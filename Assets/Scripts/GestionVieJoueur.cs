using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionVieJoueur : MonoBehaviour, Personnage
{
    GestionUI GestionUI;

    public const int VIE_INITIALE = 100;
    static int vie;

    // Use this for initialization
    void Start ()
    {
        GestionUI = GameObject.Find("UI").GetComponent<GestionUI>();

        vie = VIE_INITIALE;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Vie <= 0)
        {
            GestionUI.AfficherÉcranMort();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public int Vie
    {
        get
        {
            return vie;
        }
        set
        {
            if (value <= 0)
            {
                vie = 0;
            }
            else
            {
                vie = value;
            }
        }
    }
}
