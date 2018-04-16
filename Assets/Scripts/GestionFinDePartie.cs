using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GestionFinDePartie : MonoBehaviour {

    FinDePartieScript scriptFinDePartieScript;
    GameObject HUD;
    GestionHUD ScriptHUD;

    GestionSauvegarde SauvegardeControlleur;
    // Use this for initialization
    void Start () {
        HUD = GameObject.Find("HUD");
        ScriptHUD = HUD.GetComponent<GestionHUD>();;
        SauvegardeControlleur = HUD.GetComponent<GestionSauvegarde>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Cube" || other.transform.tag == "Personnage")
        {         
            FinDePartieScript.ancienneScène = SceneManager.GetActiveScene().name;
            if(int.Parse(FinDePartieScript.ancienneScène.Substring(6)) >= int.Parse(SauvegardeControlleur.ListeSettings[0]))
            {
                SauvegardeControlleur.ListeSettings[0] = ((((int.Parse(SauvegardeControlleur.ListeSettings[0])) + 1).ToString()));
                SauvegardeControlleur.SaveSettings();
            }
            ScriptHUD.VerifierMenu("OpenMenu");
            SceneManager.LoadScene("FinDePartie");
        }

    }
}
