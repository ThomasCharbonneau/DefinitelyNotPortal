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

    [SerializeField] Camera caméra;

    //const float DÉLAI_PASSAGE = 0.25f;
    float TempsDepuisDernierPassage;
    float angle;
    public bool passéDansFrame; //Si un objet est passé dans le portail dans le frame actuel.

    GestionCamera scriptGestionCaméra;

    Quaternion différenceRotation;
    

    // Use this for initialization
    void Start()
    {
        TempsDepuisDernierPassage = 0;
        passéDansFrame = false;
        scriptGestionCaméra = caméra.GetComponent<GestionCamera>();
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

            float vitesse = other.GetComponent<Rigidbody>().velocity.magnitude;
            other.GetComponent<Rigidbody>().velocity = portailOpposé.transform.forward;
           
            Vector3 normale = portailOpposé.transform.forward;

            other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 10);
            other.GetComponent<Rigidbody>().AddForce(normale * vitesse * 43);

            angle = Vector3.Angle(other.transform.forward, portailOpposé.transform.forward);

            if (other.transform.eulerAngles.y > 180)
            {
                angle *= -1;
            }
            scriptGestionCaméra.degréRotation = angle;
            Debug.Log(Vector3.Angle(other.transform.forward, portailOpposé.transform.forward));
            Debug.Log(other.transform.eulerAngles.y);
      
   
            //other.transform.Rotate(Vector3.up * 90);


            //other.GetComponent<Rigidbody>().rotation = Quaternion.FromToRotation(other.transform.forward, portailOpposé.transform.forward);



            //La chose teleportée est placée un peu devant le portail.
            //vitesse = Vector3.Reflect(vitesse, normale);                                                                                                              
            //vitesse = portailOpposé.transform.InverseTransformDirection(vitesse);
            //vitesse = portail.transform.TransformDirection(vitesse);                                                                                                         
            //other.GetComponent<Rigidbody>().velocity = vitesse;
            //other.GetComponent<Rigidbody>().AddForce(vitesse, ForceMode.Acceleration);
            //float somme = Mathf.Sqrt(normale.x * normale.x + normale.y * normale.y + normale.z * normale.z);
            //normale = new Vector3(normale.x / somme, normale.y / somme, normale.z / somme);
            //Debug.Log(vitesse);
            //other.GetComponent<Rigidbody>().velocity = vitesse;        
            //Arranger la direction en sortant... Ne fonctionne pas avec :
            //other.GetComponentInChildren<Camera>().transform.LookAt(portailOpposé.transform.position + portailOpposé.transform.forward * 2000);
            //other.transform.LookAt(other.transform.position + vitesse * 100);

            //other.GetComponent<Rigidbody>().AddForce(vitesse.magnitude * Vector3.forward);

            //Debug.Break();
        }
    }
    
}
