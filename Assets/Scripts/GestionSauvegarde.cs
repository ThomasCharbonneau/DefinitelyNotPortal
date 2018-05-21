using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GestionSauvegarde : MonoBehaviour
{
    const string nomDuFichier = "SavedSettings.txt";

    GestionCamera cameraControlleur;
    public List<string> ListeSettings { get; set; }
// Use this for initialization
void Start()
    {
        ListeSettings = new List<string>();
        if (!File.Exists(nomDuFichier))
        {
            GenerationSettingsDeBase();
        }
        LoadSettings();
    }


    public void SaveSettings() // Écrit les données dans un fichier Txt
    {
        StreamWriter Sauvegarde = new StreamWriter(nomDuFichier);
        Sauvegarde.WriteLine("Niveau Sauvegarder ={0}", ListeSettings[0]);
        Sauvegarde.WriteLine("Sensibilité ={0}", ListeSettings[1]);
        Sauvegarde.WriteLine("Son ={0}", ListeSettings[2]);
        Sauvegarde.Close();
    }
    public void GenerationSettingsDeBase() // Genere un fichier par defaut si le joueur n'a jamais jouer au paravant
    {
        StreamWriter FichierDeBase = new StreamWriter(nomDuFichier);
        FichierDeBase.WriteLine("Niveau Sauvegarder =0");
        FichierDeBase.WriteLine("Sensibilité =4");
        FichierDeBase.WriteLine("Son =5");
        FichierDeBase.Close();
    }
    public void LoadSettings() // Lire les parametres dans le fichier et les met dans une list pour les acceder
    {
        int i = 0;
        string lignelue;
        char[] separateur = new char[] { '=' };
        string[] chaineDelaLigneLue;
        StreamReader fichierLue = new StreamReader(nomDuFichier, System.Text.Encoding.UTF7);
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
    
