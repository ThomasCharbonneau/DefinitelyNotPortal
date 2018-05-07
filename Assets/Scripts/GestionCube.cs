using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCube : MonoBehaviour {
    [SerializeField] Rigidbody Personnage;
    GestionMouvement scriptGestionMouvement;

    // Use this for initialization
    void Start () {
       // scriptGestionMouvement = Personnage.GetComponent<GestionMouvement>();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("Personnage"))
    //    {
    //        scriptGestionMouvement.ValeurTenirObjet = 0.4f;
    //    }
    //    else
    //    {
    //        scriptGestionMouvement.ValeurTenirObjet = 1.0f;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    scriptGestionMouvement.ValeurTenirObjet = 1f;
    //}
    // Update is called once per frame
    void Update () {
		
	}
}
