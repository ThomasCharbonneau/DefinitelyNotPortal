using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionRespawnLave : MonoBehaviour {

    [SerializeField] GameObject cube1;
    [SerializeField] GameObject cube2;
    [SerializeField] GameObject cube3;
    [SerializeField] GameObject Personnage;

    Vector3 spawnPointCube1;
    Vector3 spawnPointCube2;
    Vector3 spawnPointCube3;
    Vector3 spawnPointPersonnage;

  
    

	// Use this for initialization
	void Start () {
        if (cube1 != null)
        {
            spawnPointCube1 = cube1.transform.position;
        }
        
        if(cube2 != null)
        {
            spawnPointCube2 = cube2.transform.position;
        }
        if(cube3 != null)
        {
            spawnPointCube3 = cube3.transform.position; 
        }
        spawnPointPersonnage = Personnage.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            if(other.name == "Cube1")
            {
                other.transform.position = spawnPointCube1;
            }
            if (other.name == "Cube2")
            {
                other.transform.position = spawnPointCube2;
            }
            if (other.name == "Cube3")
            {
                other.transform.position = spawnPointCube3;
            }
        }
        if (other.tag == "Personnage")
        {
            other.transform.position = spawnPointPersonnage;
        }
    }
}
