using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScriptDébutDePartie : MonoBehaviour {
  
    CanvasRenderer opacité;
    float tempsAuDébut;
    bool déjaFadeOut;
    GameObject panelTexte;
    Text txtNom;

	//Ce script sert à afficher un petit texte à l'écran qui montre le numéro du niveau ainsi que le titre du niveau
	void Start () {
        déjaFadeOut = false;
        tempsAuDébut = Time.time;
        txtNom = GetComponent<Text>();
        panelTexte = GameObject.Find("PnlTexteDébut");
    }	
	//gère la disparition du texte
	void Update () {
        if ((Time.time - tempsAuDébut) > 2 && !déjaFadeOut)//Si le texte n'a pas déja fadeOut et que sa fait plus de 2 secondes que le scène a commencé, commence à fade out le texte avec CrossFadeAlpha
        {
            txtNom.CrossFadeAlpha(0.0f, 1f, false);
            déjaFadeOut = true;
            tempsAuDébut = Time.time;
        }
        if ((Time.time - tempsAuDébut) > 1 && déjaFadeOut) //Si le texte a fade out, désactive le panneau qui vient avec
        {
            panelTexte.SetActive(false);
        }       
    }
}
