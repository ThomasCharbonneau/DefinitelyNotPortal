using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] AudioClip SonTirPortal;

    [SerializeField] Camera Caméra;

    [SerializeField] GameObject portalOrange; //Sera éventuellement le portal plat qui sera placé

    Vector3 CentrePortailBleu;
    Vector3 CentrePortailOrange;

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            //Mettre un délai pour que le fusil "recharge"

            TirerPortail();
        }
    }

    void TirerPortail() //Mettre une entrant qui dit quel portail tirer
    {
        RaycastHit hit;
        Ray ray = Caméra.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        ////Régler quand la collision est dans le vide...
        if (hit.collider.CompareTag("Plancher")) //hit.collider != portalOrange.GetComponent<Collider>())
        {
            AudioSource.PlayClipAtPoint(SonTirPortal, transform.position);
            portalOrange.transform.position = hit.point;
        }

        //Il faudra trouver un moyen d'appliquer le portal pour qu'il soit plat sur le mur / surface. Avec normales?

        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    }
}
