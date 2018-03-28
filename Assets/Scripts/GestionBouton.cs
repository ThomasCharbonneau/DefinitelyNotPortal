using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBouton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.gravity *= 2;
		
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
        if (other.tag == "Cube" || other.tag == ("Personnage"))
        {
            InverserGravité();                            
        }       
    }
}
