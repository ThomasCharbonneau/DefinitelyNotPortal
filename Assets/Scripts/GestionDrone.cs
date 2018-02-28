using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode { PATROUILLE, ATTAQUE }

public class GestionDrone : MonoBehaviour
{
    [SerializeField] GameObject joueur;
    [SerializeField] GameObject rayonLaser;
    GameObject gameObjectDrone;

    Rigidbody drone;
    Collider colliderDrone;
    BoxCollider colliderLaser;
    LineRenderer lineRenderer;

    Mode mode;

    DataPistePatrouille pistePatrouille;

    bool laserTiré; //Si un laser est présentement dans l'environnement ou non
    float tempsDepuisTirLaser;
    float diamètreLaser = 0.5f;
    Vector3 origineLaser;

    const float DÉLAI_TIR_LASER = 0.5f; //Le temps que le rayon laser prend pour est projeté après avoir trouvé la position de la cible
    const float DÉLAI_RECHARGE_TIR_LASER = 2.5f;
    const float TEMPS_TIR_LASER = 0.5f; //Le temps pour lequel un laser reste actif
    const int DISTANCE_LASER_MAX = 25; //La distance maximale à laquelle un drone peut être pour tirer un laser

    const int HAUTEUR_NORMALE = 10; //La hauteur par défaut à laquelle le drone flotte
    const float MAX_DISTANCE_DELTA = 0.5f;

    const int NB_DEGRÉS_FOV = 75;
    const int DISTANCE_VISION_MAX = 35; //La distance maximale à laquelle le drone peut appercevoir le joueur

    // Use this for initialization
    void Start ()
    {
        //pistePatrouille = new DataPistePatrouille();

        drone = GetComponent<Rigidbody>();
        gameObjectDrone = drone.gameObject;
        colliderDrone = GetComponent<Collider>();
        lineRenderer = GetComponent<LineRenderer>();

        mode = Mode.PATROUILLE;

        laserTiré = false;
        tempsDepuisTirLaser = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void FixedUpdate()
    {
        transform.forward = Vector3.RotateTowards(transform.forward, joueur.transform.position - transform.position, 0.25f, 0.25f);

        GérerHauteurAutomatique();

        switch (mode)
        {
            case Mode.PATROUILLE:
                Patrouiller();


                break;

            case Mode.ATTAQUE:

                GérerAttaque();

                break;
        }

    }

    void GérerHauteurAutomatique()
    {

        RaycastHit hitSol;
        RaycastHit hitPlafond;
        Ray raySol = new Ray(colliderDrone.ClosestPointOnBounds(transform.position + Vector3.down * 100), Vector3.down); //Pourrait améliorer écriture...
        Ray rayPlafond = new Ray(colliderDrone.ClosestPointOnBounds(transform.position + Vector3.up * 100), Vector3.up);
        Physics.Raycast(raySol, out hitSol);
        Physics.Raycast(rayPlafond, out hitPlafond);

        //Ajouter si pas d'objet = null

        if (hitSol.collider != null)
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

    /// <summary>
    /// Fonction qui détermine la position latérale du drone selon un chemin de patrouille prédéterminé
    /// </summary>
    void Patrouiller()
    {
        if(VérifierJoueurVisible())
        {
            mode = Mode.ATTAQUE;
            Debug.Log("Le joueur est visible");
        }
        else
        {
            //Utiliser DéplacerVersPoint déjà codé
        }
    }

    bool VérifierJoueurVisible()
    {
        bool voitJoueur = false;

        //Vérification que le joueur est dans le champs de vue
        if(Vector3.Angle(transform.forward, joueur.transform.position - transform.position) <= (float)NB_DEGRÉS_FOV / 2 && Vector3.Distance(transform.position, joueur.transform.position) <= DISTANCE_VISION_MAX)
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

        //...
        if(Vector3.Distance(transform.position, joueur.transform.position) <= DISTANCE_LASER_MAX && tempsDepuisTirLaser >= DÉLAI_RECHARGE_TIR_LASER)
        {
            TirerLaser();
            tempsDepuisTirLaser = 0;
        }

        if(laserTiré && tempsDepuisTirLaser >= TEMPS_TIR_LASER)
        {
            ArrêterLaser();
            laserTiré = false;
        }

        tempsDepuisTirLaser += Time.deltaTime;
    }

    void TirerLaser()
    {
        origineLaser = transform.position + (3f) * transform.forward - (1.20f) * transform.up; //Valeurs pour bien enligner le rayon avec le fusil

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origineLaser);
        lineRenderer.startWidth = diamètreLaser;
        lineRenderer.endWidth = diamètreLaser;
        lineRenderer.SetPosition(1, joueur.transform.position);

        Instantiate(rayonLaser);

        Vector3.Distance(joueur.transform.position, transform.position + (3f) * transform.forward - (1.20f) * transform.up); //Pourrait améliorer écriture

        //colliderLaser = GetComponent<BoxCollider>();

        colliderLaser.enabled = false;
        colliderLaser.size = new Vector3(diamètreLaser, diamètreLaser, Vector3.Distance(joueur.transform.position, origineLaser));
        colliderLaser.transform.position = origineLaser + (0.5f)*(joueur.transform.position - origineLaser);
        colliderLaser.transform.LookAt(joueur.transform.position);

        laserTiré = true;

        Debug.Break();
    }

    void ArrêterLaser()
    {
        lineRenderer.positionCount = 0;
        Destroy(colliderLaser.gameObject); //Pas certain que c'est OK
    }

    void DéplacerVersPoint(Vector2 pointÀAtteindre)
    {
        transform.LookAt(new Vector3(pointÀAtteindre.x, transform.position.y, pointÀAtteindre.y));

        transform.position = Vector3.MoveTowards(transform.position, pointÀAtteindre, MAX_DISTANCE_DELTA);
    }
}
