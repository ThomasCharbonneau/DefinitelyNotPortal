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
        ObjetDrone = GameObject.Find("Drone");
        if ((other.tag == "Cube" || other.tag == ("Personnage")) && ObjetDrone != null)
        {
            ObjetDrone.GetComponent<GestionDrone>().Mode = ModeDrone.DÉPLACEMENT_VERS_MARQUEUR;
        }
    }
}
