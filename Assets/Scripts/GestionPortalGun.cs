using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Les différents modes que peuvent prendre le fusil
/// </summary>
public enum ModePortalGun { PORTAIL, LASER }

public class GestionPortalGun : MonoBehaviour
{
    [SerializeField] AudioClip SonTirPortal; //Son du tir d'un portail
    [SerializeField] AudioClip SonDésactivationPortal; //Son de la désactivation simultanée des 2 portails
    [SerializeField] AudioClip SonSurfaceInvalide; //Son d'un échec de projection de laser dû à une surface invalide
    [SerializeField] AudioClip SonChangementMode; //Click qui indique le changement de mode du fusil
    [SerializeField] AudioClip SonTirLaser; //Son du tir de laser

    [SerializeField] Camera caméraPrincipale; //La caméra principale

    [SerializeField] GameObject portailBleu;
    [SerializeField] GameObject portailOrange;

    GameObject caméraPortailBleu;
    GameObject caméraPortailOrange;

    LineRenderer lineRenderer; //Servant à la représentation du laser

    const float DÉLAI_RECHARGE_TIR_PORTAILS = 0.75f; //Le temps que le fusil prends pour tirer un nouveau portail après un tir
    float tempsDepuisDernierTirPortails; //Temps depuis le dernier essai de tir d'un portail

    Vector3 origineLaser; //Le point d'origine du laser (la position du canon du fusil)
    const float DIAMÈTRE_LASER = 0.1f; //Le diamètre du laser tiré
    const int FORCE_LASER = 300; //La force que le laser exerce sur les rigidbodies touchés
    const float DÉLAI_AVANT_RECHARGE_LASER = 0.5f; //Le temps requis pour que le laser commence à charger après un tir
    public const int CHARGE_LASER_MAX = 100; //La charge maximale du laser
    bool laserTiré; //Si le laser est présentement tiré ou non
    float tempsDepuisArrêtLaser = 0; //Le temps pour lequel le laser est arrêté
    float chargeLaser; //La charge du laser (normalement entre 0 et 100 inclusivement)

    /// <summary>
    /// Le niveau de charge du laser actuel
    /// </summary>
    public float ChargeLaser
    {
        get
        {
            return chargeLaser;
        }
        private set
        {
            if(value > CHARGE_LASER_MAX) //Ne peut pas être plus haut que la charge maximale
            {
                chargeLaser = CHARGE_LASER_MAX;
            }
            else if(value < 0) //Ne peut pas être plus bas que 0
            {
                chargeLaser = 0;
            }
            else
            {
                chargeLaser = value;
            }
        }
    }

    /// <summary>
    /// Le mode actuel du fusil
    /// </summary>
    public ModePortalGun GunMode { get; private set; }

    // Use this for initialization
    void Start()
    {
        caméraPortailBleu = GameObject.Find("cameraBleu");
        caméraPortailOrange = GameObject.Find("cameraOrange");

        caméraPortailBleu.SetActive(false); //Les caméras dans les portails sont désactivées au début
        caméraPortailOrange.SetActive(false);

        lineRenderer = GetComponent<LineRenderer>();

        tempsDepuisArrêtLaser = 0;
        tempsDepuisDernierTirPortails = 0;
        laserTiré = false;

        GunMode = ModePortalGun.PORTAIL;

        ChargeLaser = CHARGE_LASER_MAX;
    }

    // Update is called once per frame
    void Update()
    {
        if (portailBleu.activeSelf && portailOrange.activeSelf)
        {
            caméraPortailBleu.SetActive(true);
            caméraPortailOrange.SetActive(true);
        }
        else
        {
            caméraPortailBleu.SetActive(false);
            caméraPortailOrange.SetActive(false);
        }

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

            AudioSource.PlayClipAtPoint(SonChangementMode, caméraPrincipale.transform.position);
        }

        tempsDepuisDernierTirPortails += Time.deltaTime;

        if (tempsDepuisDernierTirPortails >= DÉLAI_RECHARGE_TIR_PORTAILS)
        {
            if (!GestionCamera.PAUSE_CAMERA)
            {
                switch (GunMode)
                {
                    case ModePortalGun.PORTAIL:

                        if (Input.GetMouseButton(1))
                        {
                            TirerPortail(portailOrange);
                        }
                        if (Input.GetMouseButton(0))
                        {
                            TirerPortail(portailBleu);
                        }

                        break;

                    case ModePortalGun.LASER:

                        if (!(ChargeLaser <= (float)CHARGE_LASER_MAX / 4) && Input.GetMouseButtonDown(0))
                        {
                            lineRenderer.enabled = true;
                            AudioSource.PlayClipAtPoint(SonTirLaser, caméraPrincipale.transform.position);
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

        if ((portailBleu.activeSelf || portailOrange.activeSelf) && Input.GetKeyDown("r"))
        {
            DésactiverPortails();
        }
    }

    /// <summary>
    /// Fonction qui indisponibilise les portails
    /// </summary>
    public void DésactiverPortails()
    {
        portailBleu.SetActive(false);
        portailOrange.SetActive(false);

        AudioSource.PlayClipAtPoint(SonDésactivationPortal, caméraPrincipale.transform.position);
    }

    /// <summary>
    /// Fonction qui fait instancier un portail à la position touchée par un tir
    /// </summary>
    /// <param name="portail">Un des deux portails choisi pour être tiré</param>
    void TirerPortail(GameObject portail)
    {
        RaycastHit hit;

        Ray ray = new Ray(GetComponentInParent<Camera>().transform.position, caméraPrincipale.transform.forward);

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
            //Technique changée pour utiliser les tags...
            if (hit.collider.CompareTag("Sol") || hit.collider.CompareTag("Mur") || hit.collider.CompareTag("ObstaclePortal")) //Il aurait fallu trouver un moyen de simplifier avant d'avoir trop de tags
            {
                AudioSource.PlayClipAtPoint(SonTirPortal, caméraPrincipale.transform.position);
                portail.SetActive(true);

                portail.transform.position = hit.point;
                portail.transform.LookAt(hit.point + hit.normal);
            }
            else
            {
                AudioSource.PlayClipAtPoint(SonSurfaceInvalide, caméraPrincipale.transform.position);
            }

            tempsDepuisDernierTirPortails = 0;
        }

        //Pour faire des tests
        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        //Debug.Break();
    }

    /// <summary>
    /// Fonction qui tire le laser de son origine vers la position désirée
    /// </summary>
    void TirerLaser()
    {
        origineLaser = transform.position + 3f * transform.forward - 0.1f * transform.up - 0.1f * transform.right; //Valeurs pour bien enligner le rayon avec le fusil

        RaycastHit hit;
        Ray ray = new Ray(origineLaser, caméraPrincipale.transform.forward);
        Physics.Raycast(ray, out hit);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origineLaser);
        lineRenderer.startWidth = DIAMÈTRE_LASER;
        lineRenderer.endWidth = DIAMÈTRE_LASER;

        //if(hit.point != null)
        lineRenderer.SetPosition(1, hit.point);

        if (hit.rigidbody != null)
        {
            if (hit.rigidbody.gameObject.tag == "Drone")
            {
                hit.rigidbody.gameObject.GetComponent<GestionDrone>().Vie -= 1;
            }

            if(hit.rigidbody.gameObject.tag == "Cube")
            {
                hit.rigidbody.AddForce(ray.direction * FORCE_LASER);
            }
        }

        //La charge du laser est diminuée
        ChargeLaser -= 2;

        laserTiré = true;
    }

    /// <summary>
    /// Fonction qui arrête le tir du laser
    /// </summary>
    void ArrêterLaser()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
        laserTiré = false;
        tempsDepuisArrêtLaser = 0;
    }
}
