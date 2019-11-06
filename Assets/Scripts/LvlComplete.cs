using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class LvlComplete : MonoBehaviour {

    public double time;
    public double currentTime;
    bool chk;
    bool chk1;
    int frame;
    public AudioClip typing;
    // Use this for initialization
    void Start()
    {
        
        time = this.GetComponent<VideoPlayer>().clip.length;
        chk = false;
        chk1 = false;
        frame = 0;
    }


    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name != "StartScene")
        {
            if (chk1 == false)
            {
                AudioSource.PlayClipAtPoint(typing, new Vector3(0, 0, 0));
                chk1 = true;
            }
            currentTime = this.GetComponent<VideoPlayer>().time;
            if (currentTime >= time && chk == false)
            {
                chk = true;
                SceneManager.LoadScene("Level1Scene");
            }

            frame++;
        }
       
    }
}
