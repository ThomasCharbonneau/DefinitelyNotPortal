using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMusiqueAmbiance : MonoBehaviour
{
    [SerializeField] AudioClip Musique1;
    [SerializeField] AudioClip Musique2;
    [SerializeField] AudioClip Musique3;
    [SerializeField] AudioClip Musique4;
    [SerializeField] AudioClip Musique5;
    [SerializeField] AudioClip Musique6;

    const int NOMBRE_MAX_CHANSONS = 6;
    AudioClip[] tableauMusique;
    AudioSource SourceAudio;
    int chiffreAléatoire;
    int chiffreAléatoirePrécédent;

    void Start ()
    {
        // Nous crééons un tableau avec toutes les différentes chansons d'ambiances que nous avons
        tableauMusique = new AudioClip[6] { Musique1, Musique2, Musique3, Musique4, Musique5, Musique6 };
		SourceAudio = GetComponent<AudioSource>();
        chiffreAléatoirePrécédent = NOMBRE_MAX_CHANSONS + 1;
    }
	// Update is called once per frame
	void Update ()
    {
        //Lors de chaque update, on vérifie si une chanson joue déja. Si oui, on monte le son légerement jusqu'à un son qui est raisonnable pour créé une approche agréable à la chanson.
        // Si aucune chanson joue, on génere un chiffre aléatoire pour décider quelle sera la chanson suivante qui jouera.
        if (!(SourceAudio.isPlaying))
        {
            chiffreAléatoire = Random.Range(0, NOMBRE_MAX_CHANSONS);
            if(chiffreAléatoire != chiffreAléatoirePrécédent)
            {
                SourceAudio.clip = tableauMusique[chiffreAléatoire];
                SourceAudio.volume = 0;
                SourceAudio.Play();
                chiffreAléatoirePrécédent = chiffreAléatoire;
            }
        }
        else
        {
            if (SourceAudio.volume <= 0.15f)
            {
                SourceAudio.volume += 0.001f;             
            }
        }
    }
}
