using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GestionMenuDemarage : MonoBehaviour
{

    GameObject PnlOptions;
    GameObject PnlPrincipale;
    GameObject PnlJouer;
    GameObject PnlCredit;
    Button BtnJouer;
    Button BtnOption;
    Button BtnCredit;
    Button BtnRetour;

    Button BtnNewGame;
    Button BtnLoadJeu;
    bool ValeurMiseAjour;
    // Use this for initialization
    void Start()
    {
        PnlOptions = GameObject.Find("PnlOptions");
        PnlPrincipale = GameObject.Find("PnlPrincipale");
        PnlCredit = GameObject.Find("PnlCredit");
        PnlJouer = GameObject.Find("PnlJouer");

        BtnJouer = GameObject.Find("BtnJouer").GetComponent<Button>();
        BtnOption = GameObject.Find("BtnOption").GetComponent<Button>();
        BtnCredit = GameObject.Find("BtnCredit").GetComponent<Button>();
        BtnRetour = GameObject.Find("BtnRetour").GetComponent<Button>();

        BtnNewGame = GameObject.Find("BtnNewGame").GetComponent<Button>();
        BtnLoadJeu = GameObject.Find("BtnLoadJeu").GetComponent<Button>();

        BtnRetour.gameObject.SetActive(true);
        PnlOptions.gameObject.SetActive(false);
        PnlJouer.gameObject.SetActive(false);
        PnlCredit.gameObject.SetActive(false);

        ValeurMiseAjour = true;

    }
    public void VerifierMenu(string Choix)
    {
        if ("Jouer" == Choix)
        {
            PnlJouer.gameObject.SetActive(true);
            PnlPrincipale.gameObject.SetActive(false);
            PnlCredit.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            ValeurMiseAjour = true;
        }
        if ("Option" == Choix)
        {
            PnlOptions.gameObject.SetActive(true);
            PnlJouer.gameObject.SetActive(false);
            PnlPrincipale.gameObject.SetActive(false);
            PnlCredit.gameObject.SetActive(false);
            ValeurMiseAjour = true;
        }
        if ("Credit" == Choix)
        {
            PnlCredit.gameObject.SetActive(true);
            PnlOptions.gameObject.SetActive(false);
            PnlJouer.gameObject.SetActive(false);
            PnlPrincipale.gameObject.SetActive(false);
            ValeurMiseAjour = true;
        }
        if ("Retour" == Choix)
        {
            PnlPrincipale.gameObject.SetActive(true);
            PnlCredit.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlJouer.gameObject.SetActive(false);
            ValeurMiseAjour = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ValeurMiseAjour)
        {
            if (PnlPrincipale.gameObject.activeSelf)
            {
                BtnRetour.interactable = false;

            }
            else { BtnRetour.interactable = true; }
            ValeurMiseAjour = false;
        }
    }
}
