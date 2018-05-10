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

    GameObject portailBleu;
    GameObject portailOrange;

    // Script qui gère la réapparition du joueur et des cubes
    void Start () {
        scriptGestionVieJoueur = Personnage.GetComponent<GestionVieJoueur>();
        scriptGestionMouvement = Personnage.GetComponent<GestionMouvement>();
        portailBleu = GameObject.Find("PortailBleu");
        portailOrange = GameObject.Find("PortailOrange");
        if (cube1 != null) //Si nous avons un cube dans la scène,note sa position et sa rotation. Répète la même chose pour le cube 2 et 3 ainsi que le personnage
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
	void Update () {}

    //s'active lorsque le joueur rentre en collision avec la lave
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube") //Replace le cube à son point de départ avec sa rotation de départ et lui met sa vitesse à 0
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
            scriptGestionVieJoueur.Vie -= 25; //Fait perdre 25 de vie au joueur
            if (scriptGestionVieJoueur.Vie != 0) //S'active si le personnage n'est pas mort
            {
                if(scriptGestionMouvement.TientObjet) //S'assure que le personnage lache l'objet avant de réaparraitre
                {
                    scriptGestionMouvement.RelacherObjet();
                }              
                Physics.gravity = new Vector3(0f, -9.8f, 0f); //s'assure de rendre la gravité négative sinon le joueur peut rester pris dans certain niveau
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //Rend la vitesse du joueur nul
                other.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

                other.transform.position = spawnPointPersonnage; //Téléporte le joueur à son point de départ
                other.transform.rotation = rotationPersonnage;

                Personnage.GetComponentInChildren<GestionPortalGun>().DésactiverPortails(); //Désactive les portails
            }    
        }
    }
}
