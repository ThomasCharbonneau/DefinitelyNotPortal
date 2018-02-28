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
    
	
	void Start () {
        gradateur = GetComponentInChildren<Slider>();

        txtValeur = PnlText.GetComponentInChildren<Text>();
        gradateur.value = gradateur.maxValue / 2;
        Valeur = (int)gradateur.value;
        txtValeur.text = Valeur.ToString();
        ValeurMiseÀJour = true;
	}
	
	

    public void ModificationGradateur()
    {
        ValeurMiseÀJour = Valeur != (int)gradateur.value;
        Valeur = (int)gradateur.value;
        txtValeur.text = Valeur.ToString();
    }
}
