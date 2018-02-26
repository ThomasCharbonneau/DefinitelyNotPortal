using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GestionHUD : MonoBehaviour
{

    GameObject PnlMenu;
    GameObject PnlOptions;
    GameObject PnlBoutons;
    GameObject PnlCrossair;

    public Canvas CanvasControlleur;
    GestionSauvegarde SauvegardeControlleur;
    Button BtnResumer;
    Button BtnOption;
    Button BtnRetour;
    bool ValeurMiseAjour;
    bool Paused;
    [SerializeField] Slider SldSensitivité;
    [SerializeField] Slider SldSon;
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
    }
    public void InitialisationDesParametres()
    {
        SldSensitivité.value = float.Parse(SauvegardeControlleur.ListeSettings[1]);
        SldSon.value = float.Parse(SauvegardeControlleur.ListeSettings[2]);
        cameraControlleur.Sensitivité = float.Parse(SauvegardeControlleur.ListeSettings[1]);
    }
    public void SauvegardeDesSettings()
    {
        SauvegardeControlleur.ListeSettings[1] = SldSensitivité.value.ToString();
        SauvegardeControlleur.ListeSettings[2] = SldSon.value.ToString();
        SauvegardeControlleur.SaveSettings();
        InitialisationDesParametres();
    }
    public void VerifierMenu(string Choix)
    {
        if ("Options" == Choix)
        {
            PnlMenu.gameObject.SetActive(true);
            BtnRetour.gameObject.SetActive(true);
            PnlOptions.gameObject.SetActive(true);
            PnlBoutons.gameObject.SetActive(false);
            InitialisationDesParametres();
        }
        if ("Resumer" == Choix)
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
        if ("Retour" == Choix)
        {
            PnlMenu.gameObject.SetActive(true);
            BtnRetour.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlBoutons.gameObject.SetActive(true);
        }
        if ("OpenMenu" == Choix)
        {
            GestionCamera.PAUSE_CAMERA = true;
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PnlCrossair.gameObject.SetActive(false);
            PnlMenu.gameObject.SetActive(true);
            BtnRetour.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlBoutons.gameObject.SetActive(true); ;
        }
        if ("Sauvegarder" == Choix)
        {
            SauvegardeDesSettings();
        }
        if ("SaveGame" == Choix)
        {
           //A FAIRE
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
