using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {

      //  other.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,0), ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
