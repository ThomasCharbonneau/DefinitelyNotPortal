using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadScript : MonoBehaviour {

    const float FORCE_Verticale = 30f;
    const float FORCE_Horizontale = 10560f;
    private Vector3 PropulsationVerticale;
    private Vector3 PropulsationHorizontale;

    // Use this for initialization
    void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        PropulsationVerticale =   (Vector3.up * FORCE_Verticale * Physics.gravity.y * (-1 / 9.81f));
        PropulsationHorizontale = other.transform.forward * FORCE_Horizontale;
        other.GetComponent<Rigidbody>().velocity = (PropulsationVerticale + PropulsationHorizontale);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
