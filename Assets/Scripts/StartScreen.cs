using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {


	void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            transform.localRotation = Quaternion.Euler(0, 0, Time.deltaTime * Input.gyro.attitude.z) ;
    
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    SceneManager.LoadScene("Level0Scene");
                }
            }

#endif
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Joystick1Button9))
        {
            SceneManager.LoadScene("Level0Scene");
          
        }
    }

     
 
   void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    //do stuff
}
}
