using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] AudioClip SonTirPortal;
    [SerializeField] AudioClip SonSurfaceInvalide;

    [SerializeField] Camera Caméra;

    [SerializeField] GameObject portalBleu;
    [SerializeField] GameObject portalOrange;

    Vector3 CentrePortailBleu;
    Vector3 CentrePortailOrange;

    const float DÉLAI_RECHARGE = 0.75f;
    float TempsDepuisDernierTir;

	// Use this for initialization
	void Start ()
    {
        TempsDepuisDernierTir = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        TempsDepuisDernierTir += Time.deltaTime;

        if(TempsDepuisDernierTir >= DÉLAI_RECHARGE)
        {
            if (Input.GetMouseButton(0))
            {
                TirerPortail(portalOrange);
            }
            if (Input.GetMouseButton(1))
            {
                TirerPortail(portalBleu);
            }
        }
    }

    void TirerPortail(GameObject portail) //Mettre une entrant qui dit quel portail tirer // void TirerPortail(GameObject portail)
    {
        RaycastHit hit;

        Ray ray = new Ray(GetComponentInParent<Camera>().transform.position, Caméra.transform.forward);

        //Ray ray = new Ray(Caméra.transform.position, Caméra.transform.forward);

        //if (Physics.gravity.y < 0)
        //{
        //    ray = new Ray(Caméra.transform.position, Caméra.transform.forward);

        //}
        //else
        //{
        //    Debug.Log("Origine tir : " + Caméra.transform.position);
        //    ray = new Ray(Caméra.transform.position, new Vector3(Caméra.transform.forward.x, Caméra.transform.forward.y * -1, Caméra.transform.forward.z));
        //}
   
        Physics.Raycast(ray, out hit);

        if (hit.collider == null)
        {
            portail.SetActive(false);
        }
        else
        {
            if (hit.collider.CompareTag("Sol") || hit.collider.CompareTag("Mur")) //Il faudrait trouver un moyen de simplifier avant d'avoir trop de tags
            {
                AudioSource.PlayClipAtPoint(SonTirPortal, transform.position);
                portail.SetActive(true);

                portail.transform.position = hit.point;
                portail.transform.LookAt(hit.point + hit.normal);

                TempsDepuisDernierTir = 0;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        //Debug.Break();
    }
}
