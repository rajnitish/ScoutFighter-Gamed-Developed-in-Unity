using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTroopsGo2 : MonoBehaviour {

    float Speed;
    public Rigidbody PlaneBody;
    public AudioClip impact;

    private float strt_time;
    private float threshold_time;
    public GameObject PlayerParachuteGo;

    // Use this for initialization
    void Start() {
        
        Speed = Random.Range(3.0f, 8.0f);
        PlaneBody = GetComponent<Rigidbody>();

        strt_time = 0;
        threshold_time = GetRandomTime(2, 7);
    }

    float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }

    void checkExpiryTime()
    {
        strt_time += Time.deltaTime;

        //Check if its the right time to spawn the object
        if (strt_time >= threshold_time && transform.position.x < 8.5f)
        {
            GameObject paraC = (GameObject)Instantiate(PlayerParachuteGo);
            Vector2 position = transform.position;
            position = new Vector2(position.x, position.y - 1);
            paraC.transform.position = position;// transform.position;
            threshold_time = GetRandomTime(4, 7);
            strt_time = 0;
        }

    }

    void OnTriggerEnter()
    {
        AudioSource.PlayClipAtPoint(impact, new Vector3(0, 0, 0));
        Destroy(gameObject);
    }
    void Update()
    {
        checkExpiryTime();
        Vector2 position = transform.position;
        float ang = transform.eulerAngles.z;        
        position = new Vector2(position.x + Speed * Time.deltaTime, position.y);
        PlaneBody.MovePosition(position);

        if (transform.position.magnitude > 10f)
        {
            Destroy(gameObject);
        }

    }
}
