using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPadScript : MonoBehaviour {

    const float FORCE_Verticale = 20f;
    const float FORCE_Horizontale = 100f;
    private Vector3 PropulsationVerticale;
    private Vector3 PropulsationHorizontale;
    private Vector3 DirectionJoueur;
    [SerializeField] AudioClip Depart;
    [SerializeField] AudioClip Launch;
    [SerializeField] Rigidbody Personnage;
    float timer = 0.0f;
    int seconds;
    int TimerDepart;
    int LaunchTime;
    bool LAUNCH_MODE = false;
    Rigidbody CibleALaunch;

    // Use this for initialization
    void Start () {
		
	}
    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;

        if (LAUNCH_MODE)
        {
            if(seconds == LaunchTime - 1)
            {
                AudioSource.PlayClipAtPoint(Depart, CibleALaunch.transform.position);
            }
            if (seconds == LaunchTime)
            {
                PropulsationVerticale = (Vector3.up * FORCE_Verticale * Physics.gravity.y * (-1 / 9.81f));

                PropulsationHorizontale = CibleALaunch.transform.forward * FORCE_Horizontale;
                AudioSource.PlayClipAtPoint(Launch, CibleALaunch.transform.position);
                CibleALaunch.velocity = (PropulsationVerticale + PropulsationHorizontale);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre");
        LAUNCH_MODE = true;
        LaunchTime = seconds + 2;
        Debug.Log("Temps Launch: "+LaunchTime);
        CibleALaunch = other.GetComponent<Rigidbody>();
        AudioSource.PlayClipAtPoint(Depart, CibleALaunch.transform.position);
    }
    private void OnTriggerExit(Collider other)
    {
        LAUNCH_MODE = false;
        Debug.Log("Sort");
    }

    // Update is called once per frame
}
