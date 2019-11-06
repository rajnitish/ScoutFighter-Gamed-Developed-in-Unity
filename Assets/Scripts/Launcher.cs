using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#pragma strict




public class Launcher : MonoBehaviour {


    public static int highscore;
    public Sprite sprite2;
    public SpriteRenderer spriteRenderer;
    private bool isDead = false;
    private bool spriteChanged = false;
    private float time = 0.0f;
    private float timeNeeded = 2.0f;

    private int frames;
 
    // Use this for initialization
    public Rigidbody rb1;
    public GameObject PlayerBulletGo;
    public GameObject bulletPosition;

    public GameObject PlayerTroopsGo1;
    public GameObject PlayerTroopsGo2;
    public GameObject PlainPosition01;
    public GameObject PlainPosition02;

    public GameObject PlayerFighterGo1;
    public GameObject PlayerFighterGo2;
    public GameObject FighterPosition01;
    public GameObject FighterPosition02;

    public GameObject PauseScreenGo;

    public GameObject GameOver;
    public Text str;
    private float strt_time1;
    private float strt_time2;
    private float threshold_time1;
    private float threshold_time2;

    private float strt_time_fighter1;
    private float strt_time_fighter2;
    private float threshold_time_fighter1;
    private float threshold_time_fighter2;

    private bool flag;
    private bool randomfiringflag;
    private int fpsRandom;

    public int PLANE;
    public int FIGHTER;

    public int finalScore;
    public int bulletfireCount;

    void Start()
    {

        finalScore = 0;
        bulletfireCount = 0;

        PLANE = 0;
        FIGHTER = 1;
        Time.timeScale = 1;
        fpsRandom = 4;
        randomfiringflag = false;
    
        strt_time1 = 0;
        strt_time2 = 0;

        strt_time_fighter1 = 0;
        strt_time_fighter2 = 0;

        rb1 = GetComponent<Rigidbody>();
        threshold_time1 = GetRandomTime(0,7);
        threshold_time2 = GetRandomTime(1,5);

        threshold_time_fighter1 = GetRandomTime(9, 12);
        threshold_time_fighter2 = GetRandomTime(8, 10);

        flag = true;
        frames = 1;


        if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            if (!Input.gyro.enabled)
            {
                Input.gyro.enabled = true;
            }
#endif
        }

    }

    float GetRandomTime(float min, float max)
    {        
        return Random.Range(min, max);
    }

    void checkExpiryTime()
    {
        strt_time1 += Time.deltaTime;
        strt_time2 += Time.deltaTime;


        //Check if its the right time to spawn the object
        if (strt_time1 >= threshold_time1)
        {
            InvokeOnTimerExpireRightEntrants(PLANE);
            threshold_time1 = GetRandomTime(0,7);
            strt_time1 = 0;
        }
        else if (strt_time2 >= threshold_time2)
        {
            InvokeOnTimerExpireLeftEntrants(PLANE);
            threshold_time2 = GetRandomTime(1, 5);
            strt_time2 = 0;
        }

        // Fighter Part started

        strt_time_fighter1 += Time.deltaTime;
        strt_time_fighter2 += Time.deltaTime;


       
        if (strt_time_fighter1 >= threshold_time_fighter1)
        {
            InvokeOnTimerExpireRightEntrants(FIGHTER);
            threshold_time_fighter1 = GetRandomTime(9, 12);
            strt_time_fighter1 = 0;
        }
        else if (strt_time_fighter2 >= threshold_time_fighter2)
        {
            InvokeOnTimerExpireLeftEntrants(FIGHTER);
            threshold_time_fighter2 = GetRandomTime(8, 10);
            strt_time_fighter2 = 0;
        }
    }

    void InvokeOnTimerExpireLeftEntrants(int type_of_aircraft)
    {
        if (type_of_aircraft == PLANE)
        { 
        GameObject plane02 = (GameObject)Instantiate(PlayerTroopsGo2);//, Vector3.zero, transform.rotation);
        plane02.transform.position = PlainPosition02.transform.position;
        }
        else if (type_of_aircraft == FIGHTER)
        {
            GameObject fighter02 = (GameObject)Instantiate(PlayerFighterGo2);//, Vector3.zero, transform.rotation);
            fighter02.transform.position = FighterPosition02.transform.position;
        }
    }
    void InvokeOnTimerExpireRightEntrants(int type_of_aircraft)
    {
        if (type_of_aircraft == PLANE)
        {
            GameObject plane01 = (GameObject)Instantiate(PlayerTroopsGo1);
            plane01.transform.position = PlainPosition01.transform.position;
        }
        else if (type_of_aircraft == FIGHTER)
        {
            GameObject fighter01 = (GameObject)Instantiate(PlayerFighterGo1);
            fighter01.transform.position = FighterPosition01.transform.position;
        }
    }
       
    void FireBullet()
    {          
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGo, Vector3.zero, transform.rotation);
            bullet01.transform.position = bulletPosition.transform.position;          
        
    }
    void LateUpdate()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
        {
               // if(frames%1 == 0) FireBullet();          
        }

    }
    void OnEnable()
    {
        
    }
    public bool gameStarted;
    
    void Update()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
    

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKey(KeyCode.Joystick1Button9))
        {
            if (Time.timeScale == 1)
            {
                GameObject pauseObject = GameObject.Instantiate(PauseScreenGo) as GameObject;
                Time.timeScale = 0;
                
                // showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                GameObject[] pauses;
                pauses = GameObject.FindGameObjectsWithTag("Pause");

                foreach (GameObject respawn in pauses)
                {
                    Destroy(respawn);
                }
                
                // hidePaused();
            }
        }

        if (Time.timeScale == 0) return;

        if (gameStarted) checkExpiryTime();


        UpdateScore();

        ChekGameOver();


        if (Application.platform == RuntimePlatform.Android)
        {
#if UNITY_ANDROID
            transform.localRotation = Quaternion.Euler(0, 0, Time.deltaTime * Input.gyro.attitude.z) ;
            // transform.Rotate(0, 0, Input.gyro.attitude.z);
            // transform.rotation = Input.gyro.attitude;

            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGo, Vector3.zero, transform.rotation);
                    bullet01.transform.position = bulletPosition.transform.position;
                }
            }

#endif
        }
       
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.JoystickButton6) || Input.GetKey(KeyCode.JoystickButton3))
        {
            gameStarted = true;
            float angle = transform.eulerAngles.z;
            if(angle<70.0f || angle >278.0f)
            transform.Rotate(0, 0, Time.deltaTime * 150);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKey(KeyCode.JoystickButton1))
        {
            gameStarted = true;
            float angle = transform.eulerAngles.z;
            if(angle >282.0f || angle <73.0f)
            transform.Rotate(0, 0, Time.deltaTime * -150);            
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            
                GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGo, Vector3.zero, transform.rotation);
                bullet02.transform.position = bulletPosition.transform.position;

            bulletfireCount++;



        }
        else if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Joystick1Button4))
        {
            if (randomfiringflag == false) randomfiringflag = true;
            else if (randomfiringflag == true) randomfiringflag = false;

        }
        else if(Input.GetKey(KeyCode.F4) || Input.GetKey(KeyCode.Joystick1Button6))
        {
            fpsRandom--;
            if (fpsRandom <= 0) fpsRandom = 1;
        }
        else if (Input.GetKey(KeyCode.F5) || Input.GetKey(KeyCode.Joystick1Button7))
        {
            fpsRandom++;
            if (fpsRandom >= 20) fpsRandom = 20;
        }
        else if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Joystick1Button8))
        {
            SceneManager.LoadScene("EndScene");
        }


        if (frames% fpsRandom == 0) RandomFiringByLauncher();

        frames++;
    }

    public void RandomFiringByLauncher()
    {
        if (randomfiringflag)
        {
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGo, Vector3.zero, transform.rotation);
            bullet02.transform.position = bulletPosition.transform.position;
            float angle = transform.eulerAngles.z;

            int rnd = Random.Range(0, 2);
           // Debug.Log(rnd);
            if (rnd == 0)
            {
                transform.eulerAngles = new Vector3(0, 0, GetRandomTime(0.0f, 70.0f));
                //Debug.Log("o to 70");
            }
            else if (rnd >0)
            {
                transform.eulerAngles = new Vector3(0, 0, GetRandomTime(-70.0f, 0.0f));
                //Debug.Log("-70 to 0");
            }
        }

    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    void ChekGameOver()
    {
        if (isDead)
        {

            if (!spriteChanged)
            {
                spriteChanged = true;
                spriteRenderer.sprite = sprite2;
            }

            time += Time.deltaTime;
            if (time >= timeNeeded)
            {
                SceneManager.LoadScene("EndScene");
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Paratrooper")
        {
            if (Globals.Life != 1)
            {
                Globals.Life--;
            }
            else if (Globals.Life == 1)
                SceneManager.LoadScene("EndScene");
        }
        else if(other.gameObject.tag == "Missile")
        {
            isDead = true;

           // SceneManager.LoadScene("EndScene");
        }
        Destroy(other.gameObject);

    }


    void UpdateScore()
    {
        finalScore = Globals.counter_fighter*2 + Globals.counter_plane*2 + Globals.counter_paratrooper*1 + Globals.counter_missile*2 - Globals.counter_bullet;
       
	   if (finalScore < 0){
		   finalScore = 0;
		   Globals.counter_bullet = 0;
	   }

        int maxScore = 0;
        if (SceneManager.GetActiveScene().name == "Level0Scene") maxScore = 10;
        else if (SceneManager.GetActiveScene().name == "Level1Scene") maxScore = 20;

        if (finalScore >= maxScore)
        {
            Globals.counter_fighter = 0;
            Globals.counter_plane = 0;
            Globals.counter_paratrooper = 0;
            Globals.counter_missile = 0;
            Globals.counter_bullet = 0;
            Globals.Level++;
          

            if(SceneManager.GetActiveScene().name == "Level0Scene")
            {
                //SceneManager.LoadScene("LvlCompl");
                SceneManager.LoadScene("Level1Scene");
            }
            else if(SceneManager.GetActiveScene().name == "Level1Scene")
            {
                SceneManager.LoadScene("WinningScene");
            }

        }

        if (finalScore > highscore)
        {
            highscore = finalScore;
            PlayerPrefs.SetInt("highscore", highscore);
            PlayerPrefs.Save();
        }

        if (SceneManager.GetActiveScene().name == "StartScene" || SceneManager.GetActiveScene().name == "EndScene")
        {
            str.text = "";
        }
        else
        {
            
            string outputString = string.Format("\t\t\t\t\t\t\t\t LIFE : {0}  \t\t\t\t\t\t\t\t SCORE : {1}  \t\t\t\t\t\t\t\t LEVEL : {2} \t\t\t\t\t\t\t\t BEST:{3} ", Globals.Life, finalScore, Globals.Level, highscore);
            str.text = outputString;
        }
    }

    
}

