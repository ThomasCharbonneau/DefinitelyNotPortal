using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GestionUI : MonoBehaviour
{
    GameObject Joueur;
    GameObject PortailBleu;
    GameObject PortailOrange;
    
    GestionPortalGun PortalGun;
    GameObject PnlCrossair;

    GameObject PnlModePortail;
    GameObject PnlModeLaser;
    Image ImageChargeLaser;
    Image ImageVie;

    GameObject PnlCrosshairBleu;
    GameObject PnlCrosshairOrange;

    GameObject ÉcranMort;

    ModePortalGun ImageGunMode;

    GestionVieJoueur GestionVie;

    float différencePourcentageVie;

    // Use this for initialization
    void Start ()
    {
        Joueur = GameObject.Find("Personnage");
        GestionVie = Joueur.GetComponent<GestionVieJoueur>();
        différencePourcentageVie = 0;

        PortailBleu = GameObject.Find("PortailBleu");
        PortailOrange = GameObject.Find("PortailOrange");

        ImageChargeLaser = GameObject.Find("ImageChargeLaser").GetComponent<Image>();
        ImageVie = GameObject.Find("ImageVie").GetComponent<Image>();

        PnlCrosshairBleu = GameObject.Find("PnlBleu");
        PnlCrosshairOrange = GameObject.Find("PnlOrange");

        ÉcranMort = GameObject.Find("ÉcranMort");
        ÉcranMort.SetActive(false);

        //ImageChargeLaser = GetComponentInChildren<Image>();
        //ImageGunMode = ModePortalGun.PORTAIL;
        PortalGun = Joueur.GetComponentInChildren<GestionPortalGun>();

        PnlModeLaser = GameObject.Find("PnlModeLaser");
        PnlModePortail = GameObject.Find("PnlModePortail");

        //PnlCrossair = GameObject.Find("PnlCrossair");
    }
	
	// Update is called once per frame
	void Update ()
    {
        PnlModePortail.SetActive(PortalGun.GunMode == ModePortalGun.PORTAIL);
        PnlModeLaser.SetActive(PortalGun.GunMode == ModePortalGun.LASER);

        PnlCrosshairBleu.SetActive(PortailBleu.activeSelf);
        PnlCrosshairOrange.SetActive(PortailOrange.activeSelf);

        //if (ImageGunMode != PortalGun.GunMode)
        //      {
        //          ImageGunMode = PortalGun.GunMode;
        //          ChangerImageGunMode();
        //      }

        ImageChargeLaser.fillAmount = PortalGun.ChargeLaser / GestionPortalGun.CHARGE_LASER_MAX;

        if (Mathf.Round(ImageVie.fillAmount * GestionVieJoueur.VIE_INITIALE) != ((float)GestionVie.Vie))
        {
            //if (différencePourcentageVie == 0)
            //{
            //    différencePourcentageVie = ImageVie.fillAmount - (((float)GestionVie.Vie) / GestionVieJoueur.VIE_INITIALE);
            //    Debug.Log(différencePourcentageVie);
            //}

            ImageVie.fillAmount -= 0.01f;
        }
        //else
        //{
        //    différencePourcentageVie = 0;
        //}
    }

    void ChangerImageGunMode()
    {
        //if(ImageGunMode == ModePortalGun.PORTAIL)
        //{
        //    PnlModePortail.SetActive(true);
        //    PnlModeLaser.SetActive(false);
        //}
        //else
        //{
        //    PnlModePortail.SetActive(false);
        //    PnlModeLaser.SetActive(true);
        //}
    }

    public void AfficherÉcranMort()
    {
        ÉcranMort.SetActive(true);
    }
}
