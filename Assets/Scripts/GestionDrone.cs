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
    }
}
