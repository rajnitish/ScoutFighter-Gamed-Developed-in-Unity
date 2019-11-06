using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma strict

public class PlayerTroopsGo1 : MonoBehaviour {

    float Speed;
    public Rigidbody PlaneBody;

    private float strt_time;
    private float threshold_time;
    public GameObject PlayerParachuteGo;

    public AudioClip impact;
    
    // Use this for initialization
    void Start() {
        Speed = Random.Range(5.0f, 9.0f);
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

        if (strt_time >= threshold_time && transform.position.x > -8.5f)
        {
            GameObject paraC = (GameObject)Instantiate(PlayerParachuteGo);
            Vector2 position = transform.position;
            position = new Vector2(position.x, position.y - 1);
            paraC.transform.position = position;// transform.position;
            threshold_time = GetRandomTime(3, 7);
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
   
        position = new Vector2(position.x - Speed * Time.deltaTime, position.y);

        PlaneBody.MovePosition(position);

        if (transform.position.magnitude > 10f)
        {
            Destroy(gameObject);
        }

    }
}
