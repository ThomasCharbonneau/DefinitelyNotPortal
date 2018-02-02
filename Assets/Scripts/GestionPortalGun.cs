using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] AudioClip SonTirPortal;

    [SerializeField] Camera Caméra;

    [SerializeField] GameObject portalOrange; //Sera éventuellement le portal plat qui sera placé
    [SerializeField] GameObject portalBleu;


    Vector3 CentrePortailBleu;
    Vector3 CentrePortailOrange;
    int constanteRotation;
	// Use this for initialization
	void Start ()
    {
        constanteRotation = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            //Mettre un délai pour que le fusil "recharge"

            TirerPortail(portalOrange);
        }
        if(Input.GetMouseButton(1))
        {
            TirerPortail(portalBleu);
        }

    }

    void TirerPortail(GameObject portail) //Mettre une entrant qui dit quel portail tirer // void TirerPortail(GameObject portail)
    {
        RaycastHit hit;
        Ray ray = Caméra.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        
        ////Régler quand la collision est dans le vide...
        if (hit.collider.CompareTag("Mur")) //hit.collider != portalOrange.GetComponent<Collider>())
        {
            AudioSource.PlayClipAtPoint(SonTirPortal, transform.position);         
            portail.transform.position = hit.point;          
            portail.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (hit.collider.CompareTag("Sol")) 
        {
            AudioSource.PlayClipAtPoint(SonTirPortal, transform.position);           
            portail.transform.position = hit.point;            
            portail.transform.eulerAngles = new Vector3(90, 0, 0);
            
        }
        if (hit.collider.CompareTag("Plafond")) 
        {
            AudioSource.PlayClipAtPoint(SonTirPortal, transform.position);            
            portail.transform.position = hit.point;
            portail.transform.eulerAngles = new Vector3(270, 0, 0);
        }

        //Il faudra trouver un moyen d'appliquer le portal pour qu'il soit plat sur le mur / surface. Avec normales?

        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    }
}
