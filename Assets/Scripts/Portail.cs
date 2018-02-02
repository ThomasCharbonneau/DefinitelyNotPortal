using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour
{
    public GameObject portailOpposé;

    const float DÉLAI_PASSAGE = 0.25f;
    float TempsDepuisDernierPassage;

    // Use this for initialization
    void Start()
    {
        TempsDepuisDernierPassage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TempsDepuisDernierPassage += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (true) //other.tag == "personnage"
        {
            Debug.Log("J'AI TOUCHÉ UN PORTAIL");
            if (portailOpposé.activeSelf && TempsDepuisDernierPassage >= DÉLAI_PASSAGE) // S'assurer que les 2 portails sont placé avant de téléporté le joueur
            {
                //personnage.position = (new Vector3(personnage.position.x, personnage.position.y + 20, personnage.position.z));
                //other.transform.position = (new Vector3(other.transform.position.x, other.transform.position.y + 20, other.transform.position.z));

                other.GetComponent<Rigidbody>().AddForce(Vector3.back * 500); //Donner le bon vecteur
                other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward);
                TempsDepuisDernierPassage = 0;
            }
        }
        
    }
}
