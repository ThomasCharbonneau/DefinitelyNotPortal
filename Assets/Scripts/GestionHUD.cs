using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GestionHUD : MonoBehaviour
{

    GameObject PnlMenu;
    GameObject PnlOptions;
    GameObject PnlBoutons;
    GameObject PnlCrossair;
    GameObject PnlTexte;
    
    public Canvas CanvasControlleur;
    GestionSauvegarde SauvegardeControlleur;
    Button BtnResumer;
    Button BtnOption;
    Button BtnRetour;
    bool ValeurMiseAjour;
    bool Paused;
    [SerializeField] Slider SldSensitivité;
    [SerializeField] Slider SldSon;
    [SerializeField] GameObject PnlMort;
    [SerializeField] AudioClip SonClick;
    Camera Caméra;
    GestionCamera cameraControlleur;


    // Use this for initialization
    void Start()
    {
        Caméra = GameObject.Find("Caméra").GetComponent<Camera>();
        cameraControlleur = Caméra.GetComponent<GestionCamera>();
        PnlMenu = GameObject.Find("PnlMenu");
        PnlOptions = GameObject.Find("PnlOptions");
        PnlBoutons = GameObject.Find("PnlBoutons");
        PnlCrossair = GameObject.Find("PnlCrossair");
        PnlTexte = GameObject.Find("PnlTexteDébut");


        BtnResumer = GameObject.Find("BtnResumer").GetComponent<Button>();
        BtnOption = GameObject.Find("BtnOption").GetComponent<Button>();
        BtnRetour = GameObject.Find("BtnRetour").GetComponent<Button>();

        CanvasControlleur = GetComponent<Canvas>();
        SauvegardeControlleur = CanvasControlleur.GetComponent<GestionSauvegarde>();

        InitialisationDesParametres();

        PnlCrossair.gameObject.SetActive(true);
        PnlMenu.gameObject.SetActive(false);
        BtnRetour.gameObject.SetActive(false);
        PnlOptions.gameObject.SetActive(false);
        PnlBoutons.gameObject.SetActive(false);
        Paused = false;
        VerifierMenu("Resumer");
    }
    public void InitialisationDesParametres() // Permet de Initialiser chacun des parametres en fonction des données dans le fichier
    {
        SldSensitivité.value = float.Parse(SauvegardeControlleur.ListeSettings[1]);
        SldSon.value = float.Parse(SauvegardeControlleur.ListeSettings[2]);
        cameraControlleur.Sensitivité = float.Parse(SauvegardeControlleur.ListeSettings[1]);
        AudioListener.volume = (float.Parse(SauvegardeControlleur.ListeSettings[2]))/10f;
    }
    public void SauvegardeDesSettings() // Permet de sauvegarder les nouveaux changements et les écrient dans le fichier
    {
        SauvegardeControlleur.ListeSettings[1] = SldSensitivité.value.ToString();
        SauvegardeControlleur.ListeSettings[2] = SldSon.value.ToString();
        SauvegardeControlleur.SaveSettings();
        InitialisationDesParametres();
    }
    public void VerifierMenu(string Choix) // Permet d'interagir avec le menu
    {
        if ("Options" == Choix) // Ouvre le menu option
        {
            PnlMenu.gameObject.SetActive(true);
            BtnRetour.gameObject.SetActive(true);
            PnlOptions.gameObject.SetActive(true);
            PnlBoutons.gameObject.SetActive(false);
            InitialisationDesParametres();
        }
        if ("Reloader" == Choix) // Recommence la scene
        {
            VerifierMenu("Resumer");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
        if ("Resumer" == Choix) // Continuer le niveau en cours
        {
            Paused = false;
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GestionCamera.PAUSE_CAMERA = false;
            PnlCrossair.gameObject.SetActive(true);
            PnlMenu.gameObject.SetActive(false);
            BtnRetour.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlBoutons.gameObject.SetActive(false);
        }
        if ("Retour" == Choix) // Revenir au menu précédent
        {
            PnlMenu.gameObject.SetActive(true);
            BtnRetour.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlBoutons.gameObject.SetActive(true);
        }
        if ("OpenMenu" == Choix) // Ouvre le menu
        {
            GestionCamera.PAUSE_CAMERA = true;
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PnlTexte.SetActive(false);
            PnlCrossair.gameObject.SetActive(false);
            PnlMenu.gameObject.SetActive(true);
            BtnRetour.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlBoutons.gameObject.SetActive(true); ;
        }
        if ("Sauvegarder" == Choix) //Sauvegarder les parametres
        {
            SauvegardeDesSettings();
        }
        if ("SaveQuit" == Choix) // Quit et revien au menu principale
        {
            Paused = false;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("ScnMenuPrincipal");
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
        if ("ReloaderMort" == Choix) // Mort du joueur
        {
            PnlMort.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!Paused)
            {
                Paused = true;
                VerifierMenu("OpenMenu");
            }
        }
    }
}
