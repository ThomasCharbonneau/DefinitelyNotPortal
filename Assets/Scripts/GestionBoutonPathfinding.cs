using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBoutonPathfinding : MonoBehaviour
{
    GameObject ObjetDrone;

	// Use this for initialization
	void Start ()
    {
	}

	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube" || other.tag == ("Personnage"))
        {
            ObjetDrone = GameObject.Find("Drone");
            ObjetDrone.GetComponent<GestionDrone>().Mode = ModeDrone.DÉPLACEMENT_VERS_MARQUEUR;
        }
    }
}
