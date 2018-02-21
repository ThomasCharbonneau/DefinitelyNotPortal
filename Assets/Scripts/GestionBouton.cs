using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBouton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void InverserGravité()
    {
        Physics.gravity *= -1;
    }
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            InverserGravité();

            //other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y, other.transform.localEulerAngles.z + 180);

   
            Debug.Log(" J'ai inversé la gravité");
            
            
        
        }
        
    }
}
