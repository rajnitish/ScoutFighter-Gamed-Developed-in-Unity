using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    //public AudioClip EndMusic;
    public AudioSource m_MyAudioSource;
    public int frame_no;
	public Button myButton;
    // Use this for initialization

    private void Awake()
    {
        
    }
    void Start () {
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.Play();
		myButton = myButton.GetComponent<Button>();
		
		
    }
    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        myButton.onClick.AddListener(exitFromGame);
    }
	
	public void exitFromGame()
	{
		Application.Quit();
	}
}
