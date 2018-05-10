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
    void Start ()
    {
        HUD = GameObject.Find("HUD");
        ScriptHUD = HUD.GetComponent<GestionHUD>();;
        SauvegardeControlleur = HUD.GetComponent<GestionSauvegarde>();
    }
	
	// Update is called once per frame
	void Update () {}

    //Lorsque le joueur saute sur le bouton quadrillé qui finit le niveau, ce trigger s'active
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Personnage") //S'assure que c'est le personnage qui saute sur le bouton
        {
            FinDePartieScript.ancienneScène = SceneManager.GetActiveScene().name; //Note dans le FinDePartieScript quel niveau le joueur vient de compléter
            if(int.Parse(FinDePartieScript.ancienneScène.Substring(6)) >= int.Parse(SauvegardeControlleur.ListeSettings[0])) //Si le niveau qu'on vient de compléter est plus grand que celui qui est sauvegardé, on sauvegarde ce niveau dans le fichier
            {
                SauvegardeControlleur.ListeSettings[0] = ((int.Parse(FinDePartieScript.ancienneScène.Substring(6)) + 1).ToString());
                SauvegardeControlleur.SaveSettings();
            }
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if(SceneManager.GetActiveScene().name == ("Niveau12")) //Si le joueur vient de compléter le dernier niveau, load une scène qui lui en informe. Sinon, load la scène qui lui permet de recommencer le niveau, faire le prochain niveau, retourner au menu ou quitter
            {
                SceneManager.LoadScene("ScnFinDuJeu");
            }
            else
            {
                SceneManager.LoadScene("FinDePartie");
            }
            Physics.gravity = new Vector3(0, -9.81f, 0); //S'assure que la gravité est négative pour ne pas causer de problème dans le prochain niveau
        }
    }
}
