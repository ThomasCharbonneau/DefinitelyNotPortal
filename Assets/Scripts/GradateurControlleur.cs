using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradateurControlleur : MonoBehaviour {

    public bool ValeurMiseÀJour { get; set; }
    public int Valeur { get; set; }

    public Slider gradateur { get; set; }
    Text txtValeur;

    public GameObject PnlText;
    int vitesseBase = 60;
    
	
	void Start () {
        gradateur = GetComponentInChildren<Slider>();
        gradateur.value = vitesseBase;
        Valeur = (int)gradateur.value;

        txtValeur = PnlText.GetComponentInChildren<Text>();
        txtValeur.text = Valeur.ToString();
        ValeurMiseÀJour = true;
	}
	
	

    public void ModificationGradateur()
    {
        ValeurMiseÀJour = Valeur != (int)gradateur.value; ;
        Valeur = (int)gradateur.value;
        txtValeur.text = Valeur.ToString();
    }
}
