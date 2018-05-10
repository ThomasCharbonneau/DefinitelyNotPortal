using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinDeJeuScript : MonoBehaviour {
    // Script qui gère la scène qui apparaît lorsqu'on finit les 12 niveaux
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
    public void LoaderMenuPrincipal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ScnMenuPrincipal");      
    }
    public void FermerApplication()
    {
        Application.Quit();
    }
        
}
