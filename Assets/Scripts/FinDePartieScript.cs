using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDePartieScript : MonoBehaviour {

    public static string ancienneScène;
    float chiffreAncienneScene;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RéessayerNiveau()
    {
        SceneManager.LoadScene(ancienneScène);
    }

    public void ProchainNiveau()
    {
        float.TryParse(ancienneScène, out chiffreAncienneScene);
        if (chiffreAncienneScene > 20 || chiffreAncienneScene == 0) // remplacer 20 par le nombre max de niveau
        {
            chiffreAncienneScene = 1;
        }
        else
        {
            chiffreAncienneScene += 1;
        }

        SceneManager.LoadScene("Niveau" + chiffreAncienneScene);

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
