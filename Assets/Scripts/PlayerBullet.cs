using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerBullet : MonoBehaviour {

    // Use this for initialization

    float speed;
    float ang;
	private bool spriteChanged = false;
	private bool isBulletDead = false;
    private float time = 0.0f;
    private float timeNeeded = 0.3f;
    public Rigidbody bulletBody;
    public Sprite blastSprite;
    public Sprite blastSprite1;
    public SpriteRenderer spriteRenderer;

    private bool isPlane = false;
    private bool isFighter = false;
    private bool isParatrooper = false;



    void Start () {
        speed = 8.0f;
        ang = 0f;
        bulletBody = GetComponent<Rigidbody>();
		   
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
      
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
        {           
            Globals.counter_plane++;
            isPlane = true;
        }
        if(other.tag == "Fighter")
        {
            Globals.counter_fighter++;
            isFighter = true;
        }
        if (other.tag == "Paratrooper")
        {
            Globals.counter_paratrooper++;
            Destroy(gameObject);
            return;
        }
        if (other.tag == "Missile")
        {
            Globals.counter_missile++;
            isFighter = true;
        }
		isBulletDead = true;
                
    }

    void DoBlast()
    {
        if (!spriteChanged)
        {
            spriteChanged = true;
			speed = 0.0f;
            

        }

        if(isPlane == true)
        spriteRenderer.sprite = blastSprite;
        else if(isFighter == true )
            spriteRenderer.sprite = blastSprite1;
        spriteRenderer.size += new Vector2(0.5f, 0.5f);
        time += Time.deltaTime;
        if (time >= timeNeeded)
        { 
            Destroy(gameObject);
        }

    }

    void LateUpdate()
    {
        if (isBulletDead) { DoBlast(); }
    }
    void Update()
    {
		
		
        Vector2 position = transform.position;
        float ang = transform.eulerAngles.z;

        ang = NormalizeAngle(ang);
        ang = ang + 90.0f;
        ang = ang * Mathf.Deg2Rad;

        position = new Vector2(position.x + Mathf.Cos(ang) * speed * Time.deltaTime, position.y + Mathf.Sin(ang) * speed* Time.deltaTime);

        bulletBody.MovePosition(position);
         
        if (transform.position.magnitude > 15)
        {
            Destroy(gameObject);
            Globals.counter_bullet++;
        }
    }

    
}
