using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GestionFinDePartie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Cube" || other.transform.tag == "Personnage")
        {
            SceneManager.LoadScene("FinDePartie");
        }

    }
}
