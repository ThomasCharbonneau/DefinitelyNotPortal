using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDePartieScript : MonoBehaviour {

    public static string ancienneScène;
    float chiffreAncienneScene;
    public string chiffreScene;

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}

    public void RéessayerNiveau()
    {
        SceneManager.LoadScene(ancienneScène);
    }

    public void ProchainNiveau()
    {
        chiffreScene = ancienneScène.Substring(6);
        float.TryParse(chiffreScene, out chiffreAncienneScene);
        if (chiffreAncienneScene > 20 || chiffreAncienneScene == 0) // remplacer 20 par le nombre max de niveau
        {
            chiffreAncienneScene = 1;
        }
        else
        {
            chiffreAncienneScene += 1;
        }
        SceneManager.LoadScene("Niveau" + chiffreAncienneScene);
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
