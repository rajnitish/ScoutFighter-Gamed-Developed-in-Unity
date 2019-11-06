using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerF16RHS : MonoBehaviour
{

    float Speed;
    public Rigidbody FighterBody;
    public AudioClip impact;

    private float strt_time;
    private float threshold_time;
    public GameObject PlayerMissileGo;

    // Use this for initialization
    void Start()
    {

        Speed = Random.Range(4.0f, 12.0f);
        FighterBody = GetComponent<Rigidbody>();

        strt_time = 0;
        threshold_time = GetRandomTime(0, 2);
    }

     float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }

    void checkExpiryTime()
    {
        strt_time += Time.deltaTime;

        //Check if its the right time to spawn the object
        if (strt_time >= threshold_time && transform.position.x < -3.5f)
        {
            GameObject missile = (GameObject)Instantiate(PlayerMissileGo);
            Vector2 position = transform.position;
            position = new Vector2(position.x, position.y - 1);
            missile.transform.position = position;// transform.position;
            threshold_time = GetRandomTime(0, 2);
            strt_time = 0;
        }

    }

    void OnTriggerEnter()
    {
        Debug.Log("OnTRigger Enter4 for plane");
        AudioSource.PlayClipAtPoint(impact, new Vector3(0, 0, 0));
        Destroy(gameObject);
    }
    void Update()
    {
        checkExpiryTime();

        Vector2 position = transform.position;

        float ang = transform.eulerAngles.z;
        position = new Vector2(position.x + Speed * Time.deltaTime, position.y);

        FighterBody.MovePosition(position);

        if (transform.position.magnitude > 10f)
        {
            Destroy(gameObject);
        }

    }
}
