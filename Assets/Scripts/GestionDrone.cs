﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModeDrone { PATROUILLE, ATTAQUE }

public class GestionDrone : MonoBehaviour, Personnage
{
    [SerializeField] GameObject joueur;
    GameObject gameObjectDrone;

    Rigidbody drone;
    Collider colliderDrone;
    LineRenderer lineRenderer;

    public ModeDrone Mode { get; private set; }

    DataPistePatrouille pistePatrouille;

    bool laserTiré; //Si un laser est présentement dans l'environnement ou non
    float tempsDepuisTirLaser;
    const float DIAMÈTRE_LASER = 0.5f;
    Vector3 origineLaser;

    float tempsDepuisVérouillageCible;
    const float DÉLAI_VÉROUILLAGE_CIBLE = 0.5f; //Le délai de temps entre le vérouillage de la la cible et le tir à cette position.
    bool cibleVérouillée;
    Vector3 positionCible;

    const float DÉLAI_TIR_LASER = 0.5f; //Le temps que le rayon laser prend pour est projeté après avoir trouvé la position de la cible
    const float DÉLAI_RECHARGE_TIR_LASER = 2.5f;
    const float TEMPS_TIR_LASER = 0.1f; //Le temps pour lequel un laser reste actif
    const int DISTANCE_LASER_MAX = 25; //La distance maximale à laquelle un drone peut être pour tirer un laser

    const int HAUTEUR_NORMALE = 10; //La hauteur par défaut à laquelle le drone flotte
    const float MAX_DISTANCE_DELTA = 0.5f;

    const int NB_DEGRÉS_FOV = 75;
    const int DISTANCE_VISION_MAX = 35; //La distance maximale à laquelle le drone peut appercevoir le joueur

    List<Vector2> PointsDePatrouilleAdaptés; //Les points de patrouille adaptés

    const int DÉPLACEMENT_X = 10;
    const int DÉPLACEMENT_Z = 18;

    int IndicePositionPiste;

    const int VIE_INITIALE = 3;
    int vie;

    // Use this for initialization
    void Start()
    {
        pistePatrouille = new DataPistePatrouille();
        AdapterPointsDePatrouille();
        IndicePositionPiste = 0;

        drone = GetComponent<Rigidbody>();
        gameObjectDrone = drone.gameObject;
        colliderDrone = GetComponent<Collider>();
        lineRenderer = GetComponent<LineRenderer>();

        Mode = ModeDrone.PATROUILLE;

        laserTiré = false;

        vie = VIE_INITIALE;

        tempsDepuisTirLaser = DÉLAI_RECHARGE_TIR_LASER;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vie <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        GérerHauteurAutomatique();

        switch (Mode)
        {
            case ModeDrone.PATROUILLE:
                Patrouiller();

                break;

            case ModeDrone.ATTAQUE:

                transform.forward = Vector3.RotateTowards(transform.forward, joueur.transform.position - transform.position, 0.25f, 0.25f);
                GérerAttaque();

                break;
        }
    }

    void GérerHauteurAutomatique()
    {
        RaycastHit hitSol;
        RaycastHit hitPlafond;
        Ray raySol = new Ray(transform.position + Vector3.down * 5, Vector3.down);
        //Ray raySol = new Ray(colliderDrone.ClosestPointOnBounds(transform.position + Vector3.down * 100), Vector3.down);
        Ray rayPlafond = new Ray(transform.position + Vector3.up * 5, Vector3.up);
        //Ray rayPlafond = new Ray(colliderDrone.ClosestPointOnBounds(transform.position + Vector3.up * 100), Vector3.up);
        Physics.Raycast(raySol, out hitSol);
        Physics.Raycast(rayPlafond, out hitPlafond);

        if (hitSol.collider != null)
        {
            if(hitPlafond.collider == null)
            {
                drone.transform.position = Vector3.MoveTowards(drone.transform.position, hitSol.point + Vector3.up * HAUTEUR_NORMALE, MAX_DISTANCE_DELTA);
            }
            else
            {
                if (Vector3.Distance(hitSol.point, hitPlafond.point) <= 2 * HAUTEUR_NORMALE)
                {
                    drone.transform.position = Vector3.MoveTowards(drone.transform.position, hitSol.point + (Vector3.up * Vector3.Distance(hitPlafond.point, hitSol.point) / 2), MAX_DISTANCE_DELTA);
                }
                else //(Vector3.Distance(transform.position, hitSol.point) < HAUTEUR_NORMALE)
                {
                    drone.transform.position = Vector3.MoveTowards(drone.transform.position, hitSol.point + Vector3.up * HAUTEUR_NORMALE, MAX_DISTANCE_DELTA);
                }
            }
        }
    }

    /// <summary>
    /// Fonction qui détermine la position latérale du drone selon un chemin de patrouille prédéterminé
    /// </summary>
    void Patrouiller()
    {
        if (VérifierJoueurVisible())
        {
            Mode = ModeDrone.ATTAQUE;
            Debug.Log("Le joueur est visible");
        }
        else
        {
            if (new Vector2(transform.position.x, transform.position.z) != PointsDePatrouilleAdaptés[IndicePositionPiste])
            {
                DéplacerVersPoint(PointsDePatrouilleAdaptés[IndicePositionPiste]);
            }
            else
            {
                IndicePositionPiste++;

                if (IndicePositionPiste == PointsDePatrouilleAdaptés.Count)
                {
                    IndicePositionPiste = 0;
                }
            }
        }
    }

    public void DéplacerVersPoint(Vector2 pointÀAtteindre)
    {
        transform.LookAt(new Vector3(pointÀAtteindre.x, transform.position.y, pointÀAtteindre.y));

        //Changer hauteur normale trouver un moyen de déplacer sans hauteur.
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(pointÀAtteindre.x, HAUTEUR_NORMALE, pointÀAtteindre.y), MAX_DISTANCE_DELTA);
        //transform.position = Vector2.MoveTowards(transform.position, new Vector3(pointÀAtteindre.x, 0, pointÀAtteindre.y), MAX_DISTANCE_DELTA);
    }

    /// <summary>
    /// Fonction qui transfère les points de patrouille en points 3d selon la hauteur du terrain et qui les adapte à l'échelle (scale)
    /// </summary>
    void AdapterPointsDePatrouille()
    {
        List<Vector2> PointsDePatrouille = pistePatrouille.GetPointsDePatrouille();
        PointsDePatrouilleAdaptés = new List<Vector2>();

        float coordonnéeX;
        float coordonnéeZ;

        for (int i = 0; i < PointsDePatrouille.Count; i++)
        {
            coordonnéeX = PointsDePatrouille[i].x + DÉPLACEMENT_X;
            coordonnéeZ = PointsDePatrouille[i].y + DÉPLACEMENT_Z;

            //Modifier pour que hauteur automatique soit déterminée par drone.

            PointsDePatrouilleAdaptés.Add(new Vector2(coordonnéeX, coordonnéeZ));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Vrai si le joueur est visible. Faux sinon</returns>
    bool VérifierJoueurVisible()
    {
        bool voitJoueur = false;

        //Vérification que le joueur est dans le champs de vue
        if (Vector3.Angle(transform.forward, joueur.transform.position - transform.position) <= (float)NB_DEGRÉS_FOV / 2 && Vector3.Distance(transform.position, joueur.transform.position) <= DISTANCE_VISION_MAX)
        {
            //Vérification que le joueur n'est pas caché derrière un objet
            RaycastHit hit;
            Ray ray = new Ray(transform.position, joueur.transform.position - transform.position);
            Physics.Raycast(ray, out hit);

            if (hit.rigidbody.gameObject == joueur)
            {
                voitJoueur = true;
            }
        }

        return voitJoueur;
    }

    void GérerAttaque()
    {
        if (Vector3.Distance(transform.position, joueur.transform.position) <= DISTANCE_LASER_MAX)
        {
            if (tempsDepuisTirLaser >= DÉLAI_RECHARGE_TIR_LASER)
            {
                if (!cibleVérouillée)
                {
                    positionCible = ViserCible();
                    cibleVérouillée = true;
                    tempsDepuisVérouillageCible = 0;
                }

                if (tempsDepuisVérouillageCible >= DÉLAI_VÉROUILLAGE_CIBLE)
                {
                    TirerLaser(positionCible);
                    tempsDepuisTirLaser = 0;
                    cibleVérouillée = false;
                    laserTiré = true;
                }
            }
        }

        if (laserTiré && tempsDepuisTirLaser >= TEMPS_TIR_LASER)
        {
            ArrêterLaser();
            laserTiré = false;
        }

        tempsDepuisTirLaser += Time.deltaTime;
        tempsDepuisVérouillageCible += Time.deltaTime;
    }

    Vector3 ViserCible()
    {
        return joueur.transform.position;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">La position vers laquelle le laser est tiré</param>
    void TirerLaser(Vector3 positionCible)
    {
        origineLaser = transform.position + (3f) * transform.forward - (1.20f) * transform.up; //Valeurs pour bien enligner le rayon avec le fusil

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origineLaser);
        lineRenderer.startWidth = DIAMÈTRE_LASER;
        lineRenderer.endWidth = DIAMÈTRE_LASER;
        lineRenderer.SetPosition(1, positionCible);

        RaycastHit hit;
        Ray ray = new Ray(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0));
        Physics.Raycast(ray, out hit);

        if(hit.rigidbody != null)
        {
            if (hit.rigidbody.gameObject.name == "Drone")
            {
                hit.rigidbody.gameObject.GetComponent<GestionDrone>().Vie -= 1;
            }
            //if (hit.rigidbody.gameObject.name == "Personnage")
            //{
            //    hit.rigidbody.gameObject.GetComponent<GestionJoueur>().Vie -= 1;
            //}
        }

        //colliderLaser = GetComponent<BoxCollider>();
        //colliderLaser.enabled = false;
        //colliderLaser.size = new Vector3(diamètreLaser, diamètreLaser, Vector3.Distance(joueur.transform.position, origineLaser));
        //colliderLaser.transform.position = origineLaser + (0.5f) * (joueur.transform.position - origineLaser);
        //colliderLaser.transform.LookAt(joueur.transform.position);
    }

    void ArrêterLaser()
    {
        lineRenderer.positionCount = 0;
    }

    public int Vie
    {
        get
        {
            return vie;
        }
        set
        {
            if(value <= 0)
            {
                vie = 0;
            }
            else
            {
                vie = value;
            }
        }
    }
}
