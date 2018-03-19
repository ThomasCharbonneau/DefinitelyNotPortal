using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionUI : MonoBehaviour
{
    [SerializeField] GameObject Joueur;
    [SerializeField] GameObject PortailBleu;
    [SerializeField] GameObject PortailOrange;

    GestionPortalGun PortalGun;
    GameObject PnlCrossair;

    [SerializeField] GameObject PnlModePortail;
    [SerializeField] GameObject PnlModeLaser;

    [SerializeField] GameObject PnlCrosshairBleu;
    [SerializeField] GameObject PnlCrosshairOrange;

    ModePortalGun ImageGunMode;

    // Use this for initialization
    void Start ()
    {
        //ImageGunMode = ModePortalGun.PORTAIL;
        PortalGun = Joueur.GetComponentInChildren<GestionPortalGun>();

        //PnlModeLaser = GameObject.Find("PnlModeLaser");
        //PnlModePortail = GameObject.Find("PnlModePortail");


        //PnlCrossair = GameObject.Find("PnlCrossair");
        //PnlCrossair.gameObject.SetActive(true);
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
