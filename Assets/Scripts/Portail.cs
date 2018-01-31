using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour {

    public GameObject portailOpposé;
    

    public Rigidbody personnage;
   

    // Use this for initialization
    void Start() {
        personnage = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update() {

    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (true) //other.tag == "personnage"
        {
            Debug.Log("J'AI TOUCHER UN PORTAIL");
            if (portailOpposé.activeSelf) // S'assurer que les 2 portails sont placé avant de téléporté le joueur
            {
                //personnage.position = (new Vector3(personnage.position.x, personnage.position.y + 20, personnage.position.z));
                //other.transform.position = (new Vector3(other.transform.position.x, other.transform.position.y + 20, other.transform.position.z));
                
                other.transform.position = (new Vector3(portailOpposé.transform.position.x, portailOpposé.transform.position.y, 
                    portailOpposé.transform.position.z) + portailOpposé.transform.forward * -5);
                
            }
        }
        
    }
}
