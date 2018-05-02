using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDébutDePartie : MonoBehaviour {

    
    CanvasRenderer opacité;
    
    GameObject pnlNom;
	// Use this for initialization
	void Start () {

		pnlNom = GameObject.Find("PnlNom");
        opacité = GetComponent<CanvasRenderer>();
        Color c = opacité.GetMaterial().color;     
        c.a = 0f;
        opacité.GetMaterial().color = c;

    }
	
	// Update is called once per frame
	void Update () {

        

    }
}
