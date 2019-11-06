using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
       // SceneManager.UnloadScene("LvlComplete");

    }
	
	// Update is called once per frame
	void Update () {

        if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            transform.localRotation = Quaternion.Euler(0, 0, Time.deltaTime * Input.gyro.attitude.z);

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
            PlayGame();
        }
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Joystick1Button8))
        {
            QuitGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level0Scene");
    }

    public void HelpLook()
    {
        //SceneManager.UnloadScene("StartScene");

        SceneManager.LoadScene("RuleScene");
    }
    public void Back()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
