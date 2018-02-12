using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GestionHUD : MonoBehaviour
{

    GameObject PnlMenu;
    GameObject PnlOptions;
    GameObject PnlBoutons;

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

        BtnResumer = GameObject.Find("BtnResumer").GetComponent<Button>();
        BtnOption = GameObject.Find("BtnOption").GetComponent<Button>();
        BtnRetour = GameObject.Find("BtnRetour").GetComponent<Button>();

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
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PnlMenu.gameObject.SetActive(true);
            BtnRetour.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlBoutons.gameObject.SetActive(true); ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Paused = !(Paused);
            if(!Paused)
            {
                VerifierMenu("Resumer");
            }
            else
            {

                VerifierMenu("OpenMenu");
            }
        }
    }
}
