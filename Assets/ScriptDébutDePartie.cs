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
	// Use this for initialization
	void Start () {
        déjaFadeOut = false;
        tempsAuDébut = Time.time;
        txtNom = GetComponent<Text>();
        panelTexte = GameObject.Find("PnlTexteDébut");
    }
	
	// Update is called once per frame
	void Update () {
        if ((Time.time - tempsAuDébut) > 2 && !déjaFadeOut)
        {
            txtNom.CrossFadeAlpha(0.0f, 1f, false);
            déjaFadeOut = true;
            tempsAuDébut = Time.time;
        }

        if ((Time.time - tempsAuDébut) > 1 && déjaFadeOut)
        {
            Debug.Log("JAi PASSER OCIIc");
            panelTexte.SetActive(false);
        }
        

    }
}
