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
        if (other.transform.tag == "Personnage")
        {
            FinDePartieScript.ancienneScène = SceneManager.GetActiveScene().name;
            if(int.Parse(FinDePartieScript.ancienneScène.Substring(6)) >= int.Parse(SauvegardeControlleur.ListeSettings[0]))
            {
                SauvegardeControlleur.ListeSettings[0] = ((int.Parse(FinDePartieScript.ancienneScène.Substring(6)) + 1).ToString());
                SauvegardeControlleur.SaveSettings();
            }
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if(SceneManager.GetActiveScene().name == ("Niveau9"))
            {
                SceneManager.LoadScene("ScnFinDuJeu");
            }
            else
            {
                SceneManager.LoadScene("FinDePartie");
            }

            Physics.gravity = new Vector3(0, -9.81f, 0);
        }

    }
}
