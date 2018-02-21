using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GestionSauvegarde : MonoBehaviour
{

    const string CHEMIN = "../";
    const string nomDuFichier = "SavedSettings.txt";

    public Slider SliderSensitivité;
    public Slider SliderSon;

    public GameObject personnage;
    GestionCamera cameraControlleur;
    public List<string> ListeSettings { get; set; }
// Use this for initialization
void Start()
    {
        SliderSensitivité = GetComponent<Slider>();
        SliderSon = GetComponent<Slider>();

        personnage = GetComponent<GameObject>();
        cameraControlleur = personnage.GetComponent<GestionCamera>();

        ListeSettings = new List<string>();
        if (!File.Exists(CHEMIN + nomDuFichier))
        {
            GenerationSettingsDeBase();
        }
    }


    public void SaveSettings()
    {
        StreamWriter Sauvegarde = new StreamWriter(CHEMIN + nomDuFichier);
        Sauvegarde.WriteLine("Niveau Sauvegarder ={0}", ListeSettings[0]);
        Sauvegarde.WriteLine("Sensibilité ={0}", ListeSettings[1]);
        Sauvegarde.WriteLine("Son ={0}", ListeSettings[2]);
        Sauvegarde.Close();
        LoadSettings();
    }
    public void GenerationSettingsDeBase()
    {
        StreamWriter FichierDeBase = new StreamWriter(CHEMIN + nomDuFichier);
        FichierDeBase.WriteLine("Niveau Sauvegarder =0");
        FichierDeBase.WriteLine("Sensibilité =4");
        FichierDeBase.WriteLine("Son =5");
        FichierDeBase.Close();
    }
    public void LoadSettings()
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
        SliderSensitivité.value = float.Parse(ListeSettings[1]);
        SliderSon.value = float.Parse(ListeSettings[2]);
        cameraControlleur.Sensitivité = float.Parse(ListeSettings[1]);

        fichierLue.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
    
