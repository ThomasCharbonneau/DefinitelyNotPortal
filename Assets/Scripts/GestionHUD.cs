﻿using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        PnlMenu = GameObject.Find("PnlMenu");
        PnlOptions = GameObject.Find("PnlOptions");
        PnlBoutons = GameObject.Find("PnlBoutons");
        PnlCrossair = GameObject.Find("PnlCrossair");

        BtnResumer = GameObject.Find("BtnResumer").GetComponent<Button>();
        BtnOption = GameObject.Find("BtnOption").GetComponent<Button>();
        BtnRetour = GameObject.Find("BtnRetour").GetComponent<Button>();
        CanvasControlleur = GetComponent<Canvas>();
        SauvegardeControlleur = CanvasControlleur.GetComponent<GestionSauvegarde>();

       // SauvegardeControlleur.LoadSettings();




        PnlCrossair.gameObject.SetActive(true);
        PnlMenu.gameObject.SetActive(false);
        BtnRetour.gameObject.SetActive(false);
        PnlOptions.gameObject.SetActive(false);
        PnlBoutons.gameObject.SetActive(false);
        Paused = false;
    }
    public void VerifierMenu(string Choix)
    {
        if ("Options" == Choix)
        {
            PnlMenu.gameObject.SetActive(true);
            BtnRetour.gameObject.SetActive(true);
            PnlOptions.gameObject.SetActive(true);
            PnlBoutons.gameObject.SetActive(false);
        }
        if ("Resumer" == Choix)
        {
            Paused = false;
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GestionCamera.PAUSE_CAMERA = true;
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
            GestionCamera.PAUSE_CAMERA = false;
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
            SauvegardeControlleur.SaveSettings();
            //ListeSettings[1] = SliderSensitivité.value.ToString();
            //ListeSettings[2] = SliderSon.value.ToString();
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
