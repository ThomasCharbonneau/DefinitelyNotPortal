using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDePartieScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RéessayerNiveau()
    {
        // SceneManager.LoadScene("Scene_Name");
    }

    public void ProchainNiveau()
    {
        // loader prochain niveau
    }

    public void LoaderMenu()
    {
        SceneManager.LoadScene("ScnMenuPrincipal");
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
