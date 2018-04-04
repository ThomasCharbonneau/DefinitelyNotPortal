using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadNoTimerScript : MonoBehaviour {
    const float FORCE_Verticale = 15f;
    const float FORCE_Horizontale = 60f;
    private Vector3 PropulsationVerticale;
    private Vector3 PropulsationHorizontale;
    private Vector3 DirectionJoueur;
    [SerializeField] AudioClip Launch;
    [SerializeField] Rigidbody Personnage;
    Rigidbody personnage;
    bool LAUNCH_MODE = false;
    Rigidbody CibleALaunch;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        if (LAUNCH_MODE)
        {            
                    PropulsationVerticale = (Vector3.up * FORCE_Verticale * Physics.gravity.y * (-1 / 9.81f));
                    if (CibleALaunch.CompareTag("Personnage"))
                    {
                        PropulsationHorizontale = Personnage.transform.forward * FORCE_Horizontale;
                    }
                    else
                    {
                        PropulsationHorizontale = DirectionJoueur * FORCE_Horizontale;
                    }
                    AudioSource.PlayClipAtPoint(Launch, CibleALaunch.transform.position);
                    CibleALaunch.velocity += (PropulsationVerticale + PropulsationHorizontale);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre");
        LAUNCH_MODE = true;
        CibleALaunch = other.GetComponent<Rigidbody>();
        DirectionJoueur = Personnage.transform.forward;
    }
    private void OnTriggerExit(Collider other)
    {
        LAUNCH_MODE = false;
    }
}
