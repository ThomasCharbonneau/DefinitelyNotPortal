using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] Camera Caméra;

    [SerializeField] GameObject portalOrange; //Sera éventuellement le portal plat qui sera placé

    Vector3 CentrePortailBleu;

    int x;
    int y;

	// Use this for initialization
	void Start ()
    {
        x = Screen.width / 2;
        y = Screen.height / 2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            TirerPortail();
        }
    }

    void TirerPortail() //Vérifier pour ne pas lancer si le ray est déjà sur un portal. Pour ne pas relancer
    {
        RaycastHit hit;
        Ray ray = Caméra.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        portalOrange.transform.position = hit.point;

        //Il faudra trouver un moyen d'appliquer le portal pour qu'il soit plat sur le mur / surface.


        

        //Ray ray = Caméra.ScreenPointToRay(new Vector3(x, y));
        //Physics.Raycast(ray);
        //ray.
        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    }
}
