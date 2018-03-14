﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portail : MonoBehaviour
{
    [SerializeField] GameObject portailOpposé;
    [SerializeField] GameObject portail;

    [SerializeField] GameObject personnage;

    [SerializeField] AudioClip SonTeleportation;

    [SerializeField] Camera caméra;

    //const float DÉLAI_PASSAGE = 0.25f;
    float TempsDepuisDernierPassage;
    public bool passéDansFrame; //Si un objet est passé dans le portail dans le frame actuel.

    Quaternion différenceRotation;
    

    // Use this for initialization
    void Start()
    {
        TempsDepuisDernierPassage = 0;
        passéDansFrame = false;
    }

    // Update is called once per frame
    void Update()
    {
        TempsDepuisDernierPassage += Time.deltaTime;
        passéDansFrame = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        /*if (portailOpposé.activeSelf && TempsDepuisDernierPassage >= DÉLAI_PASSAGE)*/ // S'assurer que les 2 portails sont placé avant de téléporté le joueur
        if (portailOpposé.activeSelf)
        {
            if (other.transform.IsChildOf(personnage.transform) && personnage.GetComponent<GestionMouvement>().TientObjet)
            {
                personnage.GetComponent<GestionMouvement>().RelacherObjet();
            }

            AudioSource.PlayClipAtPoint(SonTeleportation, portailOpposé.transform.position); 

            /*   other.GetComponent<Rigidbody>().AddForce(Vector3.back * 500);*/ //Donner le bon vecteur
            float vitesse = other.GetComponent<Rigidbody>().velocity.magnitude;
            other.GetComponent<Rigidbody>().velocity = portailOpposé.transform.forward;
           

            Vector3 normale = portailOpposé.transform.forward;

            //vitesse = Vector3.Reflect(vitesse, normale);
            //vitesse = portailOpposé.transform.InverseTransformDirection(vitesse);
            //vitesse = portail.transform.TransformDirection(vitesse);
            //other.GetComponent<Rigidbody>().velocity = vitesse;

            //other.GetComponent<Rigidbody>().AddForce(vitesse, ForceMode.Acceleration);

            other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 10);//La chose teleportée est placée un peu devant le portail.
            //float somme = Mathf.Sqrt(normale.x * normale.x + normale.y * normale.y + normale.z * normale.z);
            //normale = new Vector3(normale.x / somme, normale.y / somme, normale.z / somme);
            other.GetComponent<Rigidbody>().AddForce(normale * vitesse * 43);
            //Debug.Log(vitesse);




            Debug.Log(other.GetComponent<Rigidbody>().transform.rotation.x + "  " + other.GetComponent<Rigidbody>().transform.rotation.y + "  " + other.transform.rotation.z);

            other.GetComponent<Rigidbody>().transform.Rotate(0,90,0);
            //other.GetComponent<Rigidbody>().velocity = vitesse;
         


            Debug.Log(other.transform.rotation.x + "  " + other.transform.rotation.y + "  " + other.transform.rotation.z);


            //Arranger la direction en sortant... Ne fonctionne pas avec :
            //other.GetComponentInChildren<Camera>().transform.LookAt(portailOpposé.transform.position + portailOpposé.transform.forward * 2000);
            //other.transform.LookAt(other.transform.position + vitesse * 100);

            //other.GetComponent<Rigidbody>().AddForce(vitesse.magnitude * Vector3.forward);


            //Debug.Break();
        }
    }
    
}
