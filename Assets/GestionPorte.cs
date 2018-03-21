using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPorte : MonoBehaviour {

    [SerializeField] GameObject Lampe;
    [SerializeField] GameObject Porte;

    float temps = 0;
    float tempsOuvrir = 0;
    float tempsFermer = 0;

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
            tempsFermer = AnimationPorte["close"].time;
            Lampe.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0.0896f));
            if (tempsFermer != 0)
            {
                AnimationPorte["open"].time = (2 - tempsFermer);
                Porte.GetComponent<Animation>().Play("open");
            }
            else
            {
                Porte.GetComponent<Animation>().Play("open");
            }
            temps = Time.time;           
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Cube"))
        {
            tempsOuvrir = AnimationPorte["open"].time;
            Lampe.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1,0,0));
            if (tempsOuvrir != 0)
            {
                               
                AnimationPorte["close"].time = (2 - tempsOuvrir);
                Porte.GetComponent<Animation>().Play("close");
            }
            else
            {
                Porte.GetComponent<Animation>().Play("close");
            }
            temps = Time.time;           
        }
    }
}
