using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZoneScript : MonoBehaviour
{
    [SerializeField] GameObject portalBleu;
    [SerializeField] GameObject portalOrange;
    [SerializeField] AudioClip SonDésactivationPortal;

    // Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        if (portalBleu.activeSelf || portalOrange.activeSelf)
        {
            portalBleu.SetActive(false);
            portalOrange.SetActive(false);

            AudioSource.PlayClipAtPoint(SonDésactivationPortal, transform.position);
        }
    }
}
