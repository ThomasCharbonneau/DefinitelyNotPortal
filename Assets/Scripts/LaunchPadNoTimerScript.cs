using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadNoTimerScript : MonoBehaviour {
    const float FORCE_Verticale = 30f;
    const float FORCE_Horizontale = 60f;
    private Vector3 PropulsationVerticale;
    private Vector3 PropulsationHorizontale;
    private Vector3 DirectionJoueur;
    [SerializeField] AudioClip Launch;
    [SerializeField] Rigidbody Personnage;
    GestionMouvement scriptGestionMouvement;
    Rigidbody personnage;
    bool LAUNCH_MODE = false;
    Rigidbody CibleALaunch;

    // Use this for initialization
    void Start () {
		scriptGestionMouvement = Personnage.GetComponent<GestionMouvement>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (LAUNCH_MODE)
        {
            PropulsationVerticale = (Vector3.up * FORCE_Verticale * Physics.gravity.y * (-1 / 9.81f));
            if (CibleALaunch.CompareTag("Personnage"))
            {
                scriptGestionMouvement.EstAuSol = false;
                PropulsationHorizontale = Personnage.transform.forward * FORCE_Horizontale;
            }
            else
            {
                PropulsationHorizontale = DirectionJoueur * FORCE_Horizontale;
            }
            AudioSource.PlayClipAtPoint(Launch, CibleALaunch.transform.position);
            CibleALaunch.velocity = new Vector3(0, 0, 0);
            CibleALaunch.velocity += (PropulsationVerticale + PropulsationHorizontale);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre");
        if (scriptGestionMouvement.TientObjet)
        {
            if (other.CompareTag("Personnage"))
            {
                Debug.Log("Entre");
                LAUNCH_MODE = true;
                CibleALaunch = other.GetComponent<Rigidbody>();
                DirectionJoueur = Personnage.transform.forward;
            }
            else
            {
                Debug.Log("Nest pas un personnage et Il est dans sa main");
            }
        }
        else
        {
            Debug.Log("Entre");
            LAUNCH_MODE = true;
            CibleALaunch = other.GetComponent<Rigidbody>();
            DirectionJoueur = Personnage.transform.forward;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("sort");
        LAUNCH_MODE = false;
    }
}
