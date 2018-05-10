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

    float angle;

    GestionCamera scriptGestionCaméra;
    Quaternion différenceRotation;
    

    // Use this for initialization
    void Start()
    {
        portail.SetActive(false);
        scriptGestionCaméra = caméra.GetComponent<GestionCamera>();
    }

    // Update is called once per frame
    void Update()  { }
    //S'active lorsque le joueur rentre en collision avec un portail
    private void OnTriggerEnter(Collider other)
    {
        if (portailOpposé.activeSelf) //S'assure que le portail opposé soit activé
            {
            if (other.transform.IsChildOf(personnage.transform) && personnage.GetComponent<GestionMouvement>().TientObjet) //S'assure que le joueur ne tient pas d'objet lorsqu'il rentre dans le portail
            {
                personnage.GetComponent<GestionMouvement>().RelacherObjet();
            }
            AudioSource.PlayClipAtPoint(SonTeleportation, portailOpposé.transform.position);
            Vector3 normale = portailOpposé.transform.forward; //Trouve la normale du portail qui va diriger la vitesse qu'on va donner au joueur           

            float premièreVelocity = Mathf.Sqrt(Mathf.Pow(other.GetComponent<Rigidbody>().velocity.x, 2) + Mathf.Pow(other.GetComponent<Rigidbody>().velocity.y, 2) + Mathf.Pow(other.GetComponent<Rigidbody>().velocity.z, 2)); //Calcule la vitesse du joueur lorsqu'il rentre en contact avec le portail
            other.GetComponent<Rigidbody>().velocity = (normale * premièreVelocity - (portailOpposé.transform.forward * 10)); //Redonne la vitesse au joueur selon la normale créé sur le portail en enlevant une légère portion de la vitesse dut au fait qu'on téléporte le joueur pas
                                                                                                                              //directement sur le portail mais bien un petit peu devant             
            other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 10); //Téléporte le joueur légèrement devant le portail opposé
            angle = portailOpposé.transform.eulerAngles.y - other.transform.eulerAngles.y; //Puisqu'on veut toujours que le personnage regarde dans la direction du portail, nous calculons la rotation nécessaire pour que le personnage regarde dans la bonne direction

            if(Physics.gravity.y > 0 ) //Si la gravité est inversée, l'angle de rotation l'est aussi
            {
                angle *= -1;
            }
            if (other.name == "Personnage") //Note la rotation que le personnage doit faire dans le script gestionCaméra
            {
                scriptGestionCaméra.degréRotation = angle;
            }
        }
    }
    
}
