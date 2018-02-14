using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    public GameObject portailOpposé;
    public GameObject portail;

    [SerializeField] AudioClip SonTeleportation;

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

 

    private void OnTriggerEnter(Collider personnage)
    {
        if (true) //other.tag == "personnage"
        {

            if (portailOpposé.activeSelf && TempsDepuisDernierPassage >= DÉLAI_PASSAGE) // S'assurer que les 2 portails sont placé avant de téléporté le joueur
            {
                AudioSource.PlayClipAtPoint(SonTeleportation, portailOpposé.transform.position);

                //personnage.position = (new Vector3(personnage.position.x, personnage.position.y + 20, personnage.position.z));
                //other.transform.position = (new Vector3(other.transform.position.x, other.transform.position.y + 20, other.transform.position.z));

                /*   other.GetComponent<Rigidbody>().AddForce(Vector3.back * 500);*/ //Donner le bon vecteur
                float vitesse = personnage.GetComponent<Rigidbody>().velocity.magnitude;
                personnage.GetComponent<Rigidbody>().velocity = portailOpposé.transform.forward;
                Debug.Log(vitesse);

                Vector3 normale = portailOpposé.transform.forward;

                //vitesse = Vector3.Reflect(vitesse, normale);
                //vitesse = portailOpposé.transform.InverseTransformDirection(vitesse);
                //vitesse = portail.transform.TransformDirection(vitesse);
                //other.GetComponent<Rigidbody>().velocity = vitesse;

                //other.GetComponent<Rigidbody>().AddForce(vitesse, ForceMode.Acceleration);

                personnage.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 10); //La chose teleportée est placée un peu devant le portail.
                //personnage.GetComponent<Rigidbody>().AddForce(normale * vitesse);
                //other.GetComponent<Rigidbody>().velocity = vitesse;


                //Arranger la direction en sortant... Ne fonctionne pas avec :
                //other.GetComponentInChildren<Camera>().transform.LookAt(portailOpposé.transform.position + portailOpposé.transform.forward * 2000);
                //other.transform.LookAt(other.transform.position + vitesse * 100);

                //other.GetComponent<Rigidbody>().AddForce(vitesse.magnitude * Vector3.forward);

                TempsDepuisDernierPassage = 0;

                //Debug.Break();
            }
        }

    }
}
