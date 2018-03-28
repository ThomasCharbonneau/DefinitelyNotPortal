using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GestionMenuDemarage : MonoBehaviour
{

    GameObject PnlOptions;
    GameObject PnlPrincipale;
    GameObject PnlJouer;
    GameObject PnlCredit;
    GameObject PnlChoixNiveau;
    Button BtnJouer;
    Button BtnOption;
    Button BtnCredit;
    Button BtnRetour;
    Button BtnChoixNiveau;
    string Niveau;
    Button[] TableauBoutons;

    public Canvas CanvasControlleur;
    GestionSauvegarde SauvegardeControlleur;
    [SerializeField] Slider SldSensitivité;
    [SerializeField] Slider SldSon;

    Button BtnNewGame;
    Button BtnLoadJeu;
    bool ValeurMiseAjour;
    bool CheatCode;

    string NiveauRendu;
    const int NOMBRE_NIVEAUX_MAX = 12;

    // Use this for initialization
    void Start()
    {

        TableauBoutons = new Button[NOMBRE_NIVEAUX_MAX];
        CheatCode = false;
        PnlOptions = GameObject.Find("PnlOptions");
        PnlPrincipale = GameObject.Find("PnlPrincipale");
        PnlCredit = GameObject.Find("PnlCredit");
        PnlJouer = GameObject.Find("PnlJouer");
        PnlChoixNiveau = GameObject.Find("PnlChoixNiveau");

        BtnJouer = GameObject.Find("BtnJouer").GetComponent<Button>();
        BtnOption = GameObject.Find("BtnOption").GetComponent<Button>();
        BtnCredit = GameObject.Find("BtnCredit").GetComponent<Button>();
        BtnRetour = GameObject.Find("BtnRetour").GetComponent<Button>();
        BtnChoixNiveau = GameObject.Find("BtnMenuNiveau").GetComponent<Button>();

        BtnNewGame = GameObject.Find("BtnNewGame").GetComponent<Button>();
        BtnLoadJeu = GameObject.Find("BtnLoadJeu").GetComponent<Button>();

        CanvasControlleur = GetComponent<Canvas>();
        SauvegardeControlleur = CanvasControlleur.GetComponent<GestionSauvegarde>();
        InitialiserBoutons();
        InitialisationDesParametres();

        BtnRetour.gameObject.SetActive(true);
        PnlOptions.gameObject.SetActive(false);
        PnlJouer.gameObject.SetActive(false);
        PnlCredit.gameObject.SetActive(false);
        PnlChoixNiveau.gameObject.SetActive(false);

        ValeurMiseAjour = true;
      

    }
    public void InitialisationDesParametres()
    {
        SldSensitivité.value = float.Parse(SauvegardeControlleur.ListeSettings[1]);
        SldSon.value = float.Parse(SauvegardeControlleur.ListeSettings[2]);

    }
    public void SauvegardeDesSettings()
    {
        SauvegardeControlleur.ListeSettings[1] = SldSensitivité.value.ToString();
        SauvegardeControlleur.ListeSettings[2] = SldSon.value.ToString();
        SauvegardeControlleur.SaveSettings();
        InitialisationDesParametres();
    }

    public void InitialiserBoutons()
    {
        for (int i = 1; i<= NOMBRE_NIVEAUX_MAX; i++)
        {
            TableauBoutons[i - 1] = GameObject.Find("BtnNiveau" + i).GetComponent<Button>();
        }
    }
    public void VerifierMenu(string Choix)
    {
        if ("Jouer" == Choix)
        {
            PnlJouer.gameObject.SetActive(true);
            PnlPrincipale.gameObject.SetActive(false);
            PnlCredit.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlChoixNiveau.gameObject.SetActive(false);
            ValeurMiseAjour = true;
            if(SauvegardeControlleur.ListeSettings[0] == "0")
            {
                BtnLoadJeu.interactable = false;
            }
            else { BtnLoadJeu.interactable = true; }
        }
        if ("Option" == Choix)
        {
            PnlOptions.gameObject.SetActive(true);
            PnlJouer.gameObject.SetActive(false);
            PnlPrincipale.gameObject.SetActive(false);
            PnlCredit.gameObject.SetActive(false);
            PnlChoixNiveau.gameObject.SetActive(false);
            InitialisationDesParametres();
            ValeurMiseAjour = true;
        }
        if ("Credit" == Choix)
        {
            PnlCredit.gameObject.SetActive(true);
            PnlOptions.gameObject.SetActive(false);
            PnlJouer.gameObject.SetActive(false);
            PnlPrincipale.gameObject.SetActive(false);
            PnlChoixNiveau.gameObject.SetActive(false);
            ValeurMiseAjour = true;
        }
        if ("Retour" == Choix)
        {
            PnlPrincipale.gameObject.SetActive(true);
            PnlCredit.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlJouer.gameObject.SetActive(false);
            PnlChoixNiveau.gameObject.SetActive(false);
            ValeurMiseAjour = true;
        }
        if ("ChoixNiveau" == Choix)
        {
            PnlPrincipale.gameObject.SetActive(false);
            PnlCredit.gameObject.SetActive(false);
            PnlOptions.gameObject.SetActive(false);
            PnlJouer.gameObject.SetActive(false);
            PnlChoixNiveau.gameObject.SetActive(true);
            DéterminerNiveau();
            LoaderPanelNiveaux();
            ValeurMiseAjour = true;
        }
        if ("Sauvegarder" == Choix)
        {
            SauvegardeDesSettings();
        }
        if ("New" == Choix)
        {
            SauvegardeControlleur.ListeSettings[0] = 0f.ToString();
            SauvegardeControlleur.SaveSettings();
            DéterminerNiveau();
            LoaderNiveau(NiveauRendu);

        }
        if ("Load" == Choix)
        {
            DéterminerNiveau();
            LoaderNiveau(NiveauRendu);
        }
    }
    public void DéterminerNiveau()
    {
        NiveauRendu = SauvegardeControlleur.ListeSettings[0];

        //if(NiveauRendu == "0") { Niveau = "ScnPortalGun"; }
        //if(NiveauRendu == "1") { Niveau = ""; }
        //if(NiveauRendu == "2") { Niveau = ""; }
        //if(NiveauRendu == "3") { Niveau = ""; }
        
    }

    public void LoaderNiveau(string chiffre)
    {
        if (float.Parse(chiffre) == 0) { chiffre = "1"; }
        SceneManager.LoadScene("Niveau" + chiffre);
    }

    public void LoaderPanelNiveaux()
    {
        if(CheatCode)
        {
            for (int i = 1; i <= NOMBRE_NIVEAUX_MAX; i++)
            {
               TableauBoutons[i-1].gameObject.SetActive(true);
            }          
        }
        else
        {
            for (int i = 1; i <= NOMBRE_NIVEAUX_MAX; i++)
            {
                if (i <= float.Parse(NiveauRendu) || i == 1)
                {
                    TableauBoutons[i - 1].gameObject.SetActive(true);
                }
                else
                {
                    TableauBoutons[i - 1].gameObject.SetActive(false);
                }
                
            }
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

        if (Input.GetKey("1") && Input.GetKey("2") && Input.GetKey("3"))
        {
            CheatCode = true;
            LoaderPanelNiveaux();
        }
    }
}
