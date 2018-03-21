using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModePortalGun { PORTAIL, LASER }

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] AudioClip SonTirPortal;
    [SerializeField] AudioClip SonDésactivationPortal;
    [SerializeField] AudioClip SonSurfaceInvalide;
    [SerializeField] AudioClip SonChangementMode; //Click qui indique le changement de mode du fusil

    [SerializeField] Camera Caméra;

    [SerializeField] GameObject portalBleu;
    [SerializeField] GameObject portalOrange;

    LineRenderer lineRenderer;

    Vector3 CentrePortailBleu;
    Vector3 CentrePortailOrange;
    Vector3 origineLaser;
    const float DIAMÈTRE_LASER = 0.1f;
    const int FORCE_LASER = 300;
    const int TEMPS_TIR_LASER = 2;
    bool laserTiré;

    const float DÉLAI_RECHARGE = 0.75f;
    float TempsDepuisDernierTir;

    public ModePortalGun GunMode { get; private set; }

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        TempsDepuisDernierTir = 0;
        laserTiré = false;

        GunMode = ModePortalGun.PORTAIL;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (GunMode == ModePortalGun.LASER)
            {
                GunMode = ModePortalGun.PORTAIL;
            }
            else
            {
                GunMode = ModePortalGun.LASER;
            }

            //Arranger audio, joue tout le temps
            AudioSource.PlayClipAtPoint(SonChangementMode, transform.position);
        }



        TempsDepuisDernierTir += Time.deltaTime;

        if (TempsDepuisDernierTir >= DÉLAI_RECHARGE)
        {
            if (!GestionCamera.PAUSE_CAMERA)
            {
                switch (GunMode)
                {
                    case ModePortalGun.PORTAIL:

                        if (Input.GetMouseButton(1))
                        {
                            TirerPortail(portalOrange);
                        }
                        if (Input.GetMouseButton(0))
                        {
                            TirerPortail(portalBleu);
                        }

                        break;

                    case ModePortalGun.LASER:

                        if (Input.GetMouseButton(0))
                        {
                            TirerLaser();
                        }

                        if (Input.GetMouseButtonUp(0) || (TempsDepuisDernierTir >= TEMPS_TIR_LASER))
                        {
                            ArrêterLaser();
                        }

                        //if (laserTiré && TempsDepuisDernierTir > TEMPS_TIR_LASER)

                        break;
                }
            }
        }

        if ((portalBleu.activeSelf || portalOrange.activeSelf) && Input.GetKeyDown("r"))
        {
            portalBleu.SetActive(false);
            portalOrange.SetActive(false);

            AudioSource.PlayClipAtPoint(SonDésactivationPortal, transform.position);
        }
    }

    void TirerPortail(GameObject portail)
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
            //if(hit.collider.gameObject.GetComponent<Renderer>().material.name == "SurfaceBlanche")
            //{
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

    void TirerLaser()
    {
        if (!laserTiré)
        {
            TempsDepuisDernierTir = 0;
        }

        origineLaser = transform.position + 3f * transform.forward - 0.1f * transform.up - 0.1f * transform.right; //Valeurs pour bien enligner le rayon avec le fusil

        RaycastHit hit;
        Ray ray = new Ray(origineLaser, Caméra.transform.forward);
        Physics.Raycast(ray, out hit);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origineLaser);
        lineRenderer.startWidth = DIAMÈTRE_LASER;
        lineRenderer.endWidth = DIAMÈTRE_LASER;

        lineRenderer.SetPosition(1, hit.point);
        //if(hit.point != null)
        //{
        //    lineRenderer.SetPosition(1, hit.point);
        //}
        //else
        //{
        //    lineRenderer.SetPosition(1, );
        //}

        if (hit.rigidbody != null)
        {
            if (hit.rigidbody.gameObject.name == "Drone")
            {
                hit.rigidbody.gameObject.GetComponent<GestionDrone>().Vie -= 1;
            }

            if(hit.rigidbody.gameObject.name == "Cube")
            {
                hit.rigidbody.AddForce(ray.direction * FORCE_LASER);
            }
        }

        laserTiré = true;
    }

    void ArrêterLaser()
    {
        lineRenderer.positionCount = 0;
        laserTiré = false;
    }
}
