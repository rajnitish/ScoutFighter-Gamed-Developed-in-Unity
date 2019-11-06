using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerMissileGo : MonoBehaviour
{

    // Use this for initialization

    float speed;
    bool flag;
    public Rigidbody missileBody;
    public AudioClip impact;
    public AudioClip missileSound;
    bool chk;
    float move;

    void Start()
    {
        speed = 0.006f;
        missileBody = GetComponent<Rigidbody>();
        chk = false;
        move = 0.0f;

        AudioSource.PlayClipAtPoint(missileSound, new Vector3(0, 0, 0));
    }

    void OnTriggerEnter(Collider other)
    {           

        if(other.gameObject.tag != "Missile")
        {
            AudioSource.PlayClipAtPoint(impact, new Vector3(0, 0, 0));
            Destroy(gameObject);
        }
    }

    void Update()
    {        
        if (Time.timeScale == 1)
        {
            if (move < 1)
            {
                move += Time.deltaTime * speed;// whatever you want the speed to be

                if (move > 1)
                    move = 2;

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, 0, move), Mathf.Lerp(transform.position.y, -5.0f, move), Mathf.Lerp(transform.position.z, 0, move));
            }
        }

    } 
}
