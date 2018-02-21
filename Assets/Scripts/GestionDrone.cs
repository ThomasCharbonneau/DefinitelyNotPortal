using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode { PATROUILLE, ATTAQUE }

public class GestionDrone : MonoBehaviour
{
    [SerializeField] GameObject joueur;

    Rigidbody drone;
    Collider colliderDrone;
    LineRenderer lineRenderer;

    Mode mode;

    const int HAUTEUR_NORMALE = 10; //La hauteur par défaut à laquelle le drone flotte
    const float MAX_DISTANCE_DELTA = 0.5f;

    const int DISTANCE_LASER_MAX = 15; //La distance maximale à laquelle un dorne peut tirer un laser

    // Use this for initialization
    void Start ()
    {
        drone = GetComponent<Rigidbody>();
        colliderDrone = GetComponent<Collider>();
        lineRenderer = GetComponent<LineRenderer>();

        mode = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void FixedUpdate()
    {
        transform.forward = Vector3.RotateTowards(transform.forward, joueur.transform.position - transform.position, 0.25f, 0.25f);

        GérerHauteur();

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

    void GérerHauteur()
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

    }

    void GérerAttaque()
    {
        //...
        if(Vector3.Distance(transform.position, joueur.transform.position) <= DISTANCE_LASER_MAX)
        {
            TirerLaser();
        }
    }

    void TirerLaser()
    {
        Vector3[] TableauPoints = new Vector3[] { transform.position, joueur.transform.position };

        RaycastHit hit;
        Ray ray = new Ray(transform.position, (joueur.transform.position - transform.position)); //Pourrait améliorer écriture...
        Physics.Raycast(ray, out hit);

        //lineRenderer.
    }
}
