using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] AudioClip SonTirPortal;

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
                TirerPortail();
            }
            if (Input.GetMouseButton(1))
            {
                TirerPortail();
            }
        }
    }

    void TirerPortail() //Mettre une entrant qui dit quel portail tirer
    {
        RaycastHit hit;
        Ray ray = Caméra.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

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
        }

        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    }
}
