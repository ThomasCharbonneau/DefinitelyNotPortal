using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GestionSauvegarde : MonoBehaviour
{

    const string CHEMIN = "../../";
    const string nomDuFichier = "SavedSettingsTxtPENIS";
    List<string> ListeSettings;
    // Use this for initialization
    void Start()
    {
        //ListeSettings = new List<string>();
        //GenerationSettingsDeBase("Fichier1");
        //LoadSettings("Fichier1");
        //SaveSettings("Fichier1");

    }

    public void SaveSettings(string nomDuFichier)
    {
        StreamWriter Sauvegarde = new StreamWriter(CHEMIN + nomDuFichier);
        Sauvegarde.WriteLine("Niveau Sauvegarder ={0}", ListeSettings[0]);
        Sauvegarde.WriteLine("Sensibilité ={0}", ListeSettings[1]);
        Sauvegarde.WriteLine("Son ={0}", ListeSettings[2]);
        Sauvegarde.WriteLine("ayy we made it");
        Sauvegarde.Close();
    }
    public void GenerationSettingsDeBase(string nomDuFichier)
    {
        StreamWriter FichierDeBase = new StreamWriter(CHEMIN + nomDuFichier);
        FichierDeBase.WriteLine("Niveau Sauvegarder =0");
        FichierDeBase.WriteLine("Sensibilité =50000");
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
    
