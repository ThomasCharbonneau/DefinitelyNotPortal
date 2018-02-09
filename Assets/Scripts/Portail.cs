using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    public GameObject portailOpposé;
    public GameObject portail;

    const float DÉLAI_PASSAGE = 0.25f;
    float TempsDepuisDernierPassage;
    

    // Use this for initialization
    void Start()
    {
        
        TempsDepuisDernierPassage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TempsDepuisDernierPassage += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (true) //other.tag == "personnage"
        {
            
            if (portailOpposé.activeSelf && TempsDepuisDernierPassage >= DÉLAI_PASSAGE) // S'assurer que les 2 portails sont placé avant de téléporté le joueur
            {
                //personnage.position = (new Vector3(personnage.position.x, personnage.position.y + 20, personnage.position.z));
                //other.transform.position = (new Vector3(other.transform.position.x, other.transform.position.y + 20, other.transform.position.z));

                /*   other.GetComponent<Rigidbody>().AddForce(Vector3.back * 500);*/ //Donner le bon vecteur
                Vector3 vitesse = other.GetComponent<Rigidbody>().velocity;
                Debug.Log(vitesse.magnitude);
                vitesse = Vector3.Reflect(vitesse, portailOpposé.transform.forward);
                vitesse = portailOpposé.transform.InverseTransformDirection(vitesse);
                vitesse = portail.transform.TransformDirection(vitesse);
                other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 10);
                other.GetComponent<Rigidbody>().velocity = vitesse;
                //other.GetComponent<Rigidbody>().AddForce(vitesse.magnitude * Vector3.forward);
                TempsDepuisDernierPassage = 0;
            }
        }
        
    }
}
