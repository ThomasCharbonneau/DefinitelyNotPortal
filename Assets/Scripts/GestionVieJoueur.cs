using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionVieJoueur : MonoBehaviour, Personnage
{
    public const int VIE_INITIALE = 100;
    int vie;

    // Use this for initialization
    void Start ()
    {
        vie = VIE_INITIALE;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
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
