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

    GameObject PnlCrosshairBleu;
    GameObject PnlCrosshairOrange;

    ModePortalGun ImageGunMode;

    // Use this for initialization
    void Start ()
    {
        Joueur = GameObject.Find("Personnage");
        PortailBleu = GameObject.Find("PortailBleu");
        PortailOrange = GameObject.Find("PortailOrange");

        ImageChargeLaser = GameObject.Find("ImageChargeLaser").GetComponent<Image>();

        PnlCrosshairBleu = GameObject.Find("PnlBleu");
        PnlCrosshairOrange = GameObject.Find("PnlOrange");

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
}
