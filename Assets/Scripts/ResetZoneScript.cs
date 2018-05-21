using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZoneScript : MonoBehaviour
{
    [SerializeField] GameObject portalBleu;
    [SerializeField] GameObject portalOrange;
    [SerializeField] AudioClip SonDésactivationPortal;
    [SerializeField] GameObject SavePoint;

    // Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        // Permet d'enlever toute les portails du joueur qui étaient activé précédament dans le niveau
        // Permet de sauvegarder la progression du joueur dans le niveau
        GestionRespawnLave.spawnPointPersonnage = SavePoint.transform.position;
        if (portalBleu.activeSelf || portalOrange.activeSelf)
        {
            portalBleu.SetActive(false);
            portalOrange.SetActive(false);

            AudioSource.PlayClipAtPoint(SonDésactivationPortal, transform.position);
        }
    }
}
