using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerParachuteGo : MonoBehaviour
{

    // Use this for initialization

    public Sprite mySprite;   // // Red with parachute
    public Sprite sprite2;   // Black withoutparachute
    public Sprite sprite3;   // // REd without parachute
    public SpriteRenderer spriteRenderer;
    private bool spriteChanged = false;

    float speed;
    float ang;
    bool flag;
    public Rigidbody parachuteBody;
    public AudioClip impact;
    bool chk;
    bool landedOnabove;
    int frame;

    void Start()
    {      

        speed = 2f;
        ang = 0f;
        parachuteBody = GetComponent<Rigidbody>();
        chk = false;
        landedOnabove = false;
        frame = 0;

    }

    void setAng(float c)
    {
        ang = c;
    }
    // Update is called once per frame

    float NormalizeAngle(float ang)
    {
        if (ang > 360.0f) ang = ang - 360.0f;
        else if (ang < 0.0f) ang = ang + 360.0f;

        return ang;
    }
    void OnTriggerEnter(Collider other )
    {
        if (other.gameObject.tag != "Paratrooper")
        {
            AudioSource.PlayClipAtPoint(impact, new Vector3(0, 0, 0));

            Destroy(gameObject);
            Globals.counter++;
        }
        else
        {
            if (transform.position.y < -4.2 && (Mathf.Abs(transform.position.x - other.transform.position.x) <0.2f) )
            {
                landedOnabove = true;
            }
        }
    }

    void Update()
    {
        frame++;
       

        if (transform.position.y < -4.4 || landedOnabove)
        {
            parachuteBody.useGravity = false;
            
            if(parachuteBody.useGravity == false && chk == false)
            {
                ChangeParachute();
                chk = true;
                if (transform.position.x > 1.6)
                    Globals.RHSlandedTrooper++;
                else if (transform.position.x < -1.5)
                    Globals.LHSlandedTrooper++;

                if (Globals.LHSlandedTrooper == 4 || Globals.RHSlandedTrooper == 4)
                {
                    if (Globals.LHSlandedTrooper == 4) Globals.LHSlandedTrooper = 0;
                    if (Globals.LHSlandedTrooper == 4) Globals.LHSlandedTrooper = 0;

                    if (Globals.Life != 1)
                        Globals.Life--;
                    else if (Globals.Life == 1)
                        StartCoroutine(WAITforLoading());

                }

            }
        
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Level1Scene")
                spriteRenderer.sprite = mySprite;

        }

       
        
    }

    void ChangeParachute()
    { 
        if (!spriteChanged)
            {
            if (SceneManager.GetActiveScene().name == "Level1Scene")
            {
                spriteRenderer.sprite = sprite3;
                spriteChanged = true;
            }
            else
            {
                spriteChanged = true;
                spriteRenderer.sprite = sprite2;
            }
            }                 
    }

    IEnumerator WAITforLoading()
    {
        Time.timeScale = 0.5f; ;
        yield return new WaitForSeconds(2);
        Time.timeScale = 1;
        SceneManager.LoadScene("EndScene");        
    }


}
