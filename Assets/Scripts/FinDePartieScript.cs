using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDePartieScript : MonoBehaviour {

    public static string ancienneScène;
    public static string nouvelleScène;
    float chiffreAncienneScene;
    float chiffreProchaineScene;
    public string chiffreScene;

    // Script qui gère la scène qui apparaît lorsque le joueur complète un niveau
    // À l'aide d'une variable public static, nous notons, lorsque le joueur finis un niveau, le nom de la scène qu'il vient de compléter ce qui nous permet de trouver quelle est le prochain niveau
    void Start ()
    {
        chiffreScene = ancienneScène.Substring(6); //Enlève les 6 premières lettres soit le mot "niveau" pour juste garder le chiffre du niveau
        float.TryParse(chiffreScene, out chiffreAncienneScene);
        if (chiffreAncienneScene == 12 || chiffreAncienneScene == 0)  //Si le joueur viens de compléter le dernier niveau, load une scène qui lui informe qu'il a finis le jeu
        {
            SceneManager.LoadScene("ScnFinDuJeu");
        }
        else
        {
            chiffreProchaineScene = chiffreAncienneScene + 1;
        }
    }
    public void RéessayerNiveau()
    {
        SceneManager.LoadScene(ancienneScène);
    }

    public void ProchainNiveau()
    {
        SceneManager.LoadScene("Niveau" + chiffreProchaineScene);
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
