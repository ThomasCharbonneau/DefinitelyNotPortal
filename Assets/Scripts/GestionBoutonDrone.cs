﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBoutonDrone : MonoBehaviour
{
    [SerializeField] GameObject PrefabDrone;

    GameObject DroneInstancié;

	// Use this for initialization
	void Start ()
    {
        DroneInstancié = GameObject.Find("Drone");
	}
	
    public void RespawnerDrone()
    {
        //Si le drone est desactivé, en instantier un nouveau
        if(DroneInstancié == null)
        {
            Instantiate(PrefabDrone, new Vector3(0, 15, 0), Quaternion.identity);

            //if (GameObject.Find(PrefabDrone.name) == null)
            //{
            //}
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube" || other.tag == ("Personnage"))
        {
            RespawnerDrone();
        }       
    }
}
