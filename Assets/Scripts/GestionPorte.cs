using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPorte : MonoBehaviour
{
    [SerializeField] GameObject Lampe;
    [SerializeField] GameObject Porte;

    float tempsOuvrir = 0;
    float tempsFermer = 0;

    public Animation AnimationPorte;
    
    //Script qui gère l'animation de la porte
	void Start () {
        AnimationPorte = Porte.GetComponent<Animation>();       
    }
	
	// Update is called once per frame
	void Update () {}

    //S'active lorsque quelque chose rentre en contact avec le bouton
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Cube") || other.tag == ("Personnage") || other.tag == "Drone") //vérifie que c'est le joueur, drone ou le cube qui est sur le bouton
        {
            tempsFermer = AnimationPorte["close"].time; //Note nous sommes rendu dans l'animation de la porte qui se ferme
            Lampe.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0.0896f)); //Change la couleur de la lumière à vert pour montrer au joueur que la porte est ouverte/s'ouvre
            if (tempsFermer != 0) //Si la l'animation de la porte qui se ferme n'est pas finis, commence à ouvrir la porte à partir du point ou la porte est rendu
            {
                AnimationPorte["open"].time = (2 - tempsFermer);
                Porte.GetComponent<Animation>().Play("open");
            }
            else //Si la porte est déja fermée complètement, commence l'animation du début
            {
                Porte.GetComponent<Animation>().Play("open"); 
            }      
        }
        
    }

    //S'active lorsque le joueur/cube/drone quitte le bouton
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Cube") || other.tag == ("Personnage") || other.tag == "Drone") //vérifie que c'est le joueur, drone ou le cube qui est sur le bouton
        {
            tempsOuvrir = AnimationPorte["open"].time; //Note nous sommes rendu dans l'animation de la porte qui s'ouvre
            Lampe.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1,0,0)); //Change la couleur de la lumière à rouge pour informer que la porte est fermé/se ferme
            if (tempsOuvrir != 0)//Si la l'animation de la porte qui s'ouvre n'est pas finis, commence à fermer la porte à partir du point ou la porte est rendu
            {                             
                AnimationPorte["close"].time = (2 - tempsOuvrir);
                Porte.GetComponent<Animation>().Play("close");
            }
            else//Si la porte est déja ouverte complètement, commence l'animation du début
            {
                Porte.GetComponent<Animation>().Play("close");
            }       
        }
    }
}
