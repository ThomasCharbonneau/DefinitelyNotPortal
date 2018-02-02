using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] AudioClip SonTirPortal;

    [SerializeField] Camera Caméra;

<<<<<<< HEAD
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
=======
    [SerializeField] GameObject portalOrange; //Sera éventuellement le portal plat qui sera placé
    [SerializeField] GameObject portalBleu;


    Vector3 CentrePortailBleu;
    Vector3 CentrePortailOrange;
    int constanteRotation;
	// Use this for initialization
	void Start ()
    {
        constanteRotation = 0;
>>>>>>> 455af829df476cc6321b7ecfb9f62e0be84244e7
    }
	
	// Update is called once per frame
	void Update ()
    {
        TempsDepuisDernierTir += Time.deltaTime;

<<<<<<< HEAD
        if(TempsDepuisDernierTir >= DÉLAI_RECHARGE)
        {
            if (Input.GetMouseButton(0))
            {
                TirerPortail();
            }
            if (Input.GetMouseButton(1))
            {
                TirerPortail();
            }
=======
            TirerPortail(portalOrange);
>>>>>>> 455af829df476cc6321b7ecfb9f62e0be84244e7
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
<<<<<<< HEAD

        if(hit.collider == null)
        {
            portalOrange.SetActive(false);
        }
        else if (hit.collider.CompareTag("Plancher")) //hit.collider != portalOrange.GetComponent<Collider>())
        {
            AudioSource.PlayClipAtPoint(SonTirPortal, transform.position);
            portalOrange.SetActive(true);

            portalOrange.transform.position = hit.point;
            portalOrange.transform.LookAt(hit.point + hit.normal);
            TempsDepuisDernierTir = 0;
=======
        
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
>>>>>>> 455af829df476cc6321b7ecfb9f62e0be84244e7
        }

        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    }
}
