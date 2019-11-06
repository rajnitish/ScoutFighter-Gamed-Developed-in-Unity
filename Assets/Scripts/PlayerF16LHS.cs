using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma strict

public class PlayerF16LHS : MonoBehaviour
{

    float Speed;
    public Rigidbody FighterBody;

    private float strt_time;
    private float threshold_time;
    public GameObject PlayerMissileGo;

    public AudioClip impact;

    // Use this for initialization
    void Start()
    {
        Speed = Random.Range(6.0f, 20.0f);
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

        if (strt_time >= threshold_time && transform.position.x > 3.5f)
        {
            GameObject missile = (GameObject)Instantiate(PlayerMissileGo);
            Vector2 position = transform.position;
            position = new Vector2(position.x, position.y - 1);
            missile.transform.position = position;
            threshold_time = GetRandomTime(0, 2);
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
        FighterBody.MovePosition(position);

        if (transform.position.magnitude > 10.2f)
        {
            Destroy(gameObject);
        }

    }
}
