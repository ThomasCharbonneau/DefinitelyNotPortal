using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinDeJeuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoaderMenuPrincipal()
    {
        Debug.Log("Jai ouvert le menu");
        UnityEngine.SceneManagement.SceneManager.LoadScene("ScnMenuPrincipal");      
    }

    public void FermerApplication()
    {
        Debug.Log("Jai fermer le jeu");
        Application.Quit();
    }
        
}
