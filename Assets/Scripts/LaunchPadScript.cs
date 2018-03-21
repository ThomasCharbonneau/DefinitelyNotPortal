using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadScript : MonoBehaviour {

    const float FORCE_Verticale = 20f;
    const float FORCE_Horizontale = 60f;
    private Vector3 PropulsationVerticale;
    private Vector3 PropulsationHorizontale;
    private Vector3 DirectionJoueur;
    [SerializeField] AudioClip Depart;
    [SerializeField] AudioClip Launch;
    [SerializeField] Rigidbody Personnage;
    Rigidbody personnage;
    float timer = 0.0f;
    int seconds;
    int TimerDepart;
    int LaunchTime;
    bool LAUNCH_MODE = false;
    Rigidbody CibleALaunch;
     int i = 0;

    // Use this for initialization
    void Start () {

	}
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;

        if (LAUNCH_MODE)
        {

            if (seconds == LaunchTime - 2)
            {

                if (i == 0)
                {
                    AudioSource.PlayClipAtPoint(Depart, CibleALaunch.transform.position);
                    i++;
                }
            }
            if (seconds == LaunchTime - 1)
            {

                if (i == 1)
                {
                    AudioSource.PlayClipAtPoint(Depart, CibleALaunch.transform.position);
                    i++;
                }
            }
            if (seconds == LaunchTime)
            {
                if(i == 2)
                {
                    PropulsationVerticale = (Vector3.up * FORCE_Verticale * Physics.gravity.y * (-1 / 9.81f));
                    if(CibleALaunch.CompareTag("Personnage"))
                    {
                        PropulsationHorizontale = Personnage.transform.forward * FORCE_Horizontale;
                    }
                    else
                    {
                        PropulsationHorizontale = DirectionJoueur * FORCE_Horizontale;
                    }
                    AudioSource.PlayClipAtPoint(Launch, CibleALaunch.transform.position);
                    CibleALaunch.velocity = (PropulsationVerticale + PropulsationHorizontale);
                    i++;
                }

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre");
        LAUNCH_MODE = true;
        LaunchTime = seconds + 3;
        CibleALaunch = other.GetComponent<Rigidbody>();
        DirectionJoueur = Personnage.transform.forward;
        i = 0;
    }
    private void OnTriggerExit(Collider other)
    {
        LAUNCH_MODE = false;
        Debug.Log("Sort");
    }

}
