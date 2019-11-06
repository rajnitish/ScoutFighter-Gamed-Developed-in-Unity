using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
        else if (other.gameObject.tag == "Missile")
        {
            SceneManager.LoadScene("EndScene");
        }
        Destroy(other.gameObject);

    }
}
