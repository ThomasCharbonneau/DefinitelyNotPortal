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

    const float DÉLAI_PASSAGE = 0.25f;
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
           

            Vector3 normale = portailOpposé.transform.forward;

            //vitesse = Vector3.Reflect(vitesse, normale);
            //vitesse = portailOpposé.transform.InverseTransformDirection(vitesse);
            //vitesse = portail.transform.TransformDirection(vitesse);
            //other.GetComponent<Rigidbody>().velocity = vitesse;

            //other.GetComponent<Rigidbody>().AddForce(vitesse, ForceMode.Acceleration);

            other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 10);//La chose teleportée est placée un peu devant le portail.
            other.GetComponent<Rigidbody>().AddForce(normale * vitesse * 42, ForceMode.Acceleration);



            //other.GetComponent<Rigidbody>().rotation = Quaternion.FromToRotation(other.transform.forward, normale);                                                                                     
            //other.GetComponent<Rigidbody>().velocity = vitesse;
            Debug.Log(other.transform.rotation.x + "  " + other.transform.rotation.y + "  " + other.transform.rotation.z);

            other.transform.Rotate(new Vector3(0, 90, 0));

            Debug.Log(other.transform.rotation.x + "  " + other.transform.rotation.y + "  " + other.transform.rotation.z);


            //Arranger la direction en sortant... Ne fonctionne pas avec :
            //other.GetComponentInChildren<Camera>().transform.LookAt(portailOpposé.transform.position + portailOpposé.transform.forward * 2000);
            //other.transform.LookAt(other.transform.position + vitesse * 100);

            //other.GetComponent<Rigidbody>().AddForce(vitesse.magnitude * Vector3.forward);

            TempsDepuisDernierPassage = 0;

            //Debug.Break();
        }
    }
    
}
