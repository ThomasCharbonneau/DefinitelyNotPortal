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
    Quaternion rotationCube1;
    Quaternion rotationCube2;
    Quaternion rotationCube3;
    Quaternion rotationPersonnage;
    public static Vector3 spawnPointPersonnage;

    GestionVieJoueur scriptGestionVieJoueur;
    GestionMouvement scriptGestionMouvement;




    // Use this for initialization
    void Start () {
        scriptGestionVieJoueur = Personnage.GetComponent<GestionVieJoueur>();
        scriptGestionMouvement = Personnage.GetComponent<GestionMouvement>();
        if (cube1 != null)
        {
            spawnPointCube1 = cube1.transform.position;
            rotationCube1 = cube1.transform.rotation;
        }
        
        if(cube2 != null)
        {
            spawnPointCube2 = cube2.transform.position;
            rotationCube2 = cube2.transform.rotation;
        }
        if(cube3 != null)
        {
            spawnPointCube3 = cube3.transform.position;
            rotationCube3 = cube3.transform.rotation;
        }
        spawnPointPersonnage = Personnage.transform.position;
        rotationPersonnage = Personnage.transform.rotation;
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
                other.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                other.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                other.transform.rotation = rotationCube1;
            }
            if (other.name == "Cube2")
            {
                other.transform.position = spawnPointCube2;
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                other.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                other.transform.rotation = rotationCube2;
            }
            if (other.name == "Cube3")
            {
                other.transform.position = spawnPointCube3;
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                other.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                other.transform.rotation = rotationCube3;
            }
        }
        if (other.tag == "Personnage")
        {
            scriptGestionVieJoueur.Vie -= 25;
            if (scriptGestionVieJoueur.Vie != 0)
            {
                if(scriptGestionMouvement.TientObjet)
                {
                    scriptGestionMouvement.RelacherObjet();
                }              
                Physics.gravity = new Vector3(0f, -9.8f, 0f);
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                other.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                other.transform.position = spawnPointPersonnage;
                other.transform.rotation = rotationPersonnage;
            }    
        }
    }
}
