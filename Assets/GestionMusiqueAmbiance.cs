using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMusiqueAmbiance : MonoBehaviour {
    const int NOMBRE_MAX_CHANSONS = 5;
    AudioSource[] tableauMusique;
    int chiffreAléatoire;

	void Start () {
		tableauMusique = GetComponents<AudioSource>();
        for (int i = 0; i < NOMBRE_MAX_CHANSONS; ++i)
        {
            tableauMusique[i].volume = 0;
        }
        
    }
	// Update is called once per frame
	void Update () {
        if (!(tableauMusique[0].isPlaying || tableauMusique[1].isPlaying || tableauMusique[2].isPlaying || tableauMusique[3].isPlaying || tableauMusique[4].isPlaying || tableauMusique[5].isPlaying))
        {
            chiffreAléatoire = Random.Range(0, NOMBRE_MAX_CHANSONS + 1);
            tableauMusique[chiffreAléatoire].Play();
            Debug.Log("Xd");
        }
        if (tableauMusique[0].volume < 0.1f)
        {
            for (int i = 0; i < NOMBRE_MAX_CHANSONS; ++i)
            {
                tableauMusique[i].volume += 0.0005f;
            }
        }
    }
}
