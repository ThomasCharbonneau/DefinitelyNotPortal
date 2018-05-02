using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBouton : MonoBehaviour {

    [SerializeField] AudioClip SonGravité;

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
        if (other.tag == "Cube" || other.tag == ("Personnage"))
        {
            InverserGravité();
            AudioSource.PlayClipAtPoint(SonGravité, other.transform.position, 1000000f);
        }       
    }
}
