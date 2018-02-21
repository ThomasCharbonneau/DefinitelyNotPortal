using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portail : MonoBehaviour
{
    [SerializeField] GameObject portailOpposé;
    [SerializeField] GameObject portail;

    [SerializeField] GameObject personnage;

    [SerializeField] AudioClip SonTeleportation;

    const float DÉLAI_PASSAGE = 0.75f;
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
        if (portailOpposé.activeSelf && TempsDepuisDernierPassage >= DÉLAI_PASSAGE) // S'assurer que les 2 portails sont placé avant de téléporté le joueur
        {
            if (other.transform.IsChildOf(personnage.transform) && personnage.GetComponent<GestionMouvement>().TientObjet)
            {
                personnage.GetComponent<GestionMouvement>().RelacherObjet();
            }

            AudioSource.PlayClipAtPoint(SonTeleportation, portailOpposé.transform.position);

            //other.position = (new Vector3(other.position.x, other.position.y + 20, other.position.z));
            //other.transform.position = (new Vector3(other.transform.position.x, other.transform.position.y + 20, other.transform.position.z));

            /*   other.GetComponent<Rigidbody>().AddForce(Vector3.back * 500);*/ //Donner le bon vecteur
            float vitesse = other.GetComponent<Rigidbody>().velocity.magnitude;
            other.GetComponent<Rigidbody>().velocity = portailOpposé.transform.forward;
            Debug.Log(vitesse);

            Vector3 normale = portailOpposé.transform.forward;

            //vitesse = Vector3.Reflect(vitesse, normale);
            //vitesse = portailOpposé.transform.InverseTransformDirection(vitesse);
            //vitesse = portail.transform.TransformDirection(vitesse);
            //other.GetComponent<Rigidbody>().velocity = vitesse;

            //other.GetComponent<Rigidbody>().AddForce(vitesse, ForceMode.Acceleration);

            other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 10);//La chose teleportée est placée un peu devant le portail.
            other.GetComponent<Rigidbody>().AddForce(normale * vitesse, ForceMode.Acceleration);
            //other.GetComponent<Rigidbody>().velocity = vitesse;


            //Arranger la direction en sortant... Ne fonctionne pas avec :
            //other.GetComponentInChildren<Camera>().transform.LookAt(portailOpposé.transform.position + portailOpposé.transform.forward * 2000);
            //other.transform.LookAt(other.transform.position + vitesse * 100);

            //other.GetComponent<Rigidbody>().AddForce(vitesse.magnitude * Vector3.forward);

            TempsDepuisDernierPassage = 0;

            //Debug.Break();
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (portailOpposé.activeSelf && TempsDepuisDernierPassage >= DÉLAI_PASSAGE && !portailOpposé.GetComponent<Portail>().passéDansFrame) // S'assurer que les 2 portails sont placé avant de téléporter le joueur
    //    {
    //        if (other.transform.IsChildOf(personnage.transform) && personnage.GetComponent<GestionMouvement>().TientObjet)
    //        {
    //            personnage.GetComponent<GestionMouvement>().RelacherObjet();
    //        }

    //        AudioSource.PlayClipAtPoint(SonTeleportation, portailOpposé.transform.position);

    //        float vitesse = other.GetComponent<Rigidbody>().velocity.magnitude;
    //        Debug.Log(vitesse);

    //        other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 5); //La chose teleportée est placée un peu devant le portail.

    //        Quaternion q = Quaternion.FromToRotation(portailOpposé.transform.forward, transform.forward);

    //        other.transform.rotation = q * portailOpposé.transform.rotation;

    //        other.GetComponent<Rigidbody>().velocity = other.transform.forward * vitesse;

    //        //if(other.gameObject.name == "personnage")
    //        //{
    //        //    other.transform.rotation = Quaternion.Euler(0, other.transform.rotation.eulerAngles.y, 0);
    //        //}

    //        TempsDepuisDernierPassage = 0;
    //        passéDansFrame = true;

    //        //Debug.Break();
    //    }
    //}
}
