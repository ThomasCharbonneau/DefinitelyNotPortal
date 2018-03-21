using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPorte : MonoBehaviour {

    [SerializeField] GameObject Lampe;
    [SerializeField] GameObject Porte;

    float temps = 0;
    float tempsSuperflus = 0;

    // Use this for initialization

    public Animation AnimationPorte;
    
	void Start () {
        AnimationPorte = Porte.GetComponent<Animation>();
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Cube"))
        {
            Debug.Log("J'AI TOUCHER LE COLLIDER");
            Lampe.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0.0896f));
            if ((Time.time - temps) < 2)
            {
                AnimationPorte["open"].time = (0);
                AnimationPorte["open"].time = (temps - Time.time + 2 - tempsSuperflus);
                Porte.GetComponent<Animation>().Play("open");
                tempsSuperflus = temps - Time.time + 2;

            }
            else
            {
                Porte.GetComponent<Animation>().Play("open");
                tempsSuperflus = 0;
            }
            temps = Time.time;           
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Cube"))
        {
            Debug.Log("J'AI TOUCHER LE COLLIDER");
            Lampe.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1,0,0));
            if ((Time.time - temps) < 2)
            {
                AnimationPorte["close"].time = (0);
                AnimationPorte["close"].time = (temps - Time.time + 2 - tempsSuperflus);
                Porte.GetComponent<Animation>().Play("close");
                tempsSuperflus = temps - Time.time + 2;


            }
            else
            {
                Porte.GetComponent<Animation>().Play("close");
                tempsSuperflus = 0;
            }
            temps = Time.time;           
        }
    }
}
