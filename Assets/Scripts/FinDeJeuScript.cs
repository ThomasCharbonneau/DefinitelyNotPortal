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
        UnityEngine.SceneManagement.SceneManager.LoadScene("ScnMenuPrincipal");      
    }

    public void FermerApplication()
    {
        Application.Quit();
    }
        
}
