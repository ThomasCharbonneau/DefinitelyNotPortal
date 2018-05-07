﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModePortalGun { PORTAIL, LASER }

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] AudioClip SonTirPortal;
    [SerializeField] AudioClip SonDésactivationPortal;
    [SerializeField] AudioClip SonSurfaceInvalide;
    [SerializeField] AudioClip SonChangementMode; //Click qui indique le changement de mode du fusil
    [SerializeField] AudioClip SonTirLaser;

    [SerializeField] Camera Caméra;

    [SerializeField] GameObject portalBleu;
    [SerializeField] GameObject portalOrange;

    LineRenderer lineRenderer;

    Vector3 CentrePortailBleu;
    Vector3 CentrePortailOrange;

    const float DÉLAI_RECHARGE_PORTAILS = 0.75f;
    float TempsDepuisDernierTirPortails;

    Vector3 origineLaser;
    const float DIAMÈTRE_LASER = 0.1f;
    const int FORCE_LASER = 300;
    const float DÉLAI_AVANT_RECHARGE_LASER = 0.5f;
    public const int CHARGE_LASER_MAX = 100;
    bool laserTiré;
    float tempsDepuisArrêtLaser = 0;
    float chargeLaser;

    public float ChargeLaser
    {
        get
        {
            return chargeLaser;
        }
        private set
        {
            if(value > CHARGE_LASER_MAX)
            {
                chargeLaser = CHARGE_LASER_MAX;
            }
            else if(value < 0)
            {
                chargeLaser = 0;
            }
            else
            {
                chargeLaser = value;
            }
        }
    }

    public ModePortalGun GunMode { get; private set; }

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        TempsDepuisDernierTirPortails = 0;
        laserTiré = false;

        GunMode = ModePortalGun.PORTAIL;

        ChargeLaser = CHARGE_LASER_MAX;
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
            AudioSource.PlayClipAtPoint(SonChangementMode, Caméra.transform.position);
        }

        TempsDepuisDernierTirPortails += Time.deltaTime;

        if (TempsDepuisDernierTirPortails >= DÉLAI_RECHARGE_PORTAILS)
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

                        if (!(ChargeLaser <= (float)CHARGE_LASER_MAX / 4) && Input.GetMouseButtonDown(0))
                        {
                            lineRenderer.enabled = true;
                            AudioSource.PlayClipAtPoint(SonTirLaser, Caméra.transform.position);
                        }

                        if (!laserTiré)
                        {
                            tempsDepuisArrêtLaser += Time.deltaTime;

                            if (tempsDepuisArrêtLaser >= DÉLAI_AVANT_RECHARGE_LASER)
                            {
                                ChargeLaser += 0.5f;
                            }
                        }

                        if (Input.GetMouseButton(0))
                        {
                            if (!(ChargeLaser <= (float)CHARGE_LASER_MAX / 4 && !laserTiré))
                            {
                                TirerLaser();
                            }
                        }

                        if (laserTiré && (Input.GetMouseButtonUp(0) || (ChargeLaser <= 0)))
                        {
                            ArrêterLaser();
                        }

                        break;
                }
            }
        }

        if ((portalBleu.activeSelf || portalOrange.activeSelf) && Input.GetKeyDown("r"))
        {
            DésactiverPortails();
        }
    }

    public void DésactiverPortails()
    {
        portalBleu.SetActive(false);
        portalOrange.SetActive(false);

        AudioSource.PlayClipAtPoint(SonDésactivationPortal, Caméra.transform.position);
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
            if (hit.collider.CompareTag("Sol") || hit.collider.CompareTag("Mur") || hit.collider.CompareTag("ObstaclePortal")) //Il faudrait trouver un moyen de simplifier avant d'avoir trop de tags
            {
                AudioSource.PlayClipAtPoint(SonTirPortal, Caméra.transform.position);
                portail.SetActive(true);

                portail.transform.position = hit.point;
                portail.transform.LookAt(hit.point + hit.normal);
            }
            else
            {
                AudioSource.PlayClipAtPoint(SonSurfaceInvalide, Caméra.transform.position);
            }

            TempsDepuisDernierTirPortails = 0;
        }

        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        //Debug.Break();
    }

    void TirerLaser()
    {
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
            if (hit.rigidbody.gameObject.tag == "Drone")
            {
                hit.rigidbody.gameObject.GetComponent<GestionDrone>().Vie -= 1;
            }

            if(hit.rigidbody.gameObject.name == "Cube")
            {
                hit.rigidbody.AddForce(ray.direction * FORCE_LASER);
            }
        }

        ChargeLaser -= 2;

        laserTiré = true;
    }

    void ArrêterLaser()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
        laserTiré = false;
        tempsDepuisArrêtLaser = 0;
    }
}
