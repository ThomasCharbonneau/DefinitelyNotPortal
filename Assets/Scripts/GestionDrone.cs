using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDrone : MonoBehaviour
{
    [SerializeField] GameObject joueur;

    Rigidbody drone;

    // Use this for initialization
    void Start ()
    {
        drone = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void FixedUpdate()
    {
        //transform.LookAt(joueur.transform.position);
        transform.forward = Vector3.RotateTowards(transform.forward, joueur.transform.position - transform.position, 0.25f, 0.25f);

        RaycastHit hitSol;
        RaycastHit hitPlafond;
        Ray raySol = new Ray(transform.position, Vector3.down);
        Ray rayPlafond = new Ray(transform.position, Vector3.up);
        Physics.Raycast(raySol, out hitSol);
        Physics.Raycast(rayPlafond, out hitPlafond);

        //Ajouter si pas d'objet = null

        if (hitSol.collider != null)
        {
            if (Vector3.Distance(transform.position, hitSol.point) < 15)
            {
                drone.AddForce(Vector3.up * 10);
            }
        }

        if (hitPlafond.collider != null)
        {
            if (Vector3.Distance(transform.position, hitPlafond.point) < 15)
            {
                drone.AddForce(Vector3.down * 10);
            }
        }
    }
}
