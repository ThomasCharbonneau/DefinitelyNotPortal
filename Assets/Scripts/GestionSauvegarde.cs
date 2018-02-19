using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GestionSauvegarde : MonoBehaviour
{

    const string CHEMIN = "../";
    const string nomDuFichier = "SavedSettings.txt";
    List<string> ListeSettings { get; set; }
// Use this for initialization
void Start()
    {
        ListeSettings = new List<string>();
        if (!File.Exists(CHEMIN + nomDuFichier))
        {
            GenerationSettingsDeBase(nomDuFichier);
        }
         LoadSettings(nomDuFichier);
         SaveSettings(nomDuFichier);
    }


    public void SaveSettings(string nomDuFichier)
    {
        StreamWriter Sauvegarde = new StreamWriter(CHEMIN + nomDuFichier);
        Sauvegarde.WriteLine("Niveau Sauvegarder ={0}", ListeSettings[0]);
        Sauvegarde.WriteLine("Sensibilité ={0}", ListeSettings[1]);
        Sauvegarde.WriteLine("Son ={0}", ListeSettings[2]);
        Sauvegarde.Close();
    }
    public void GenerationSettingsDeBase(string nomDuFichier)
    {
        StreamWriter FichierDeBase = new StreamWriter(CHEMIN + nomDuFichier);
        FichierDeBase.WriteLine("Niveau Sauvegarder =0");
        FichierDeBase.WriteLine("Sensibilité =4");
        FichierDeBase.WriteLine("Son =5");
        FichierDeBase.Close();
    }
    public void LoadSettings(string nomDuFichier)
    {
        int i = 0;
        string lignelue;
        char[] separateur = new char[] { '=' };
        string[] chaineDelaLigneLue;
        StreamReader fichierLue = new StreamReader(CHEMIN + nomDuFichier, System.Text.Encoding.UTF7);
        while (!fichierLue.EndOfStream)
        {
            lignelue = fichierLue.ReadLine();
            chaineDelaLigneLue = lignelue.Split(separateur);
            ListeSettings.Add(chaineDelaLigneLue[1]);
        }
        fichierLue.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
    
