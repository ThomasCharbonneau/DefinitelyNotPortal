using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portail : MonoBehaviour
{
    [SerializeField] GameObject portailOpposé;
    [SerializeField] GameObject portail;

    [SerializeField] GameObject personnage;

    [SerializeField] AudioClip SonTeleportation;

    [SerializeField] Camera caméra;

    const float DÉLAI_PASSAGE = 1f;
    float TempsDepuisDernierPassage;
    float angle;
    public bool passéDansFrame; //Si un objet est passé dans le portail dans le frame actuel.

    GestionCamera scriptGestionCaméra;

    Quaternion différenceRotation;
    

    // Use this for initialization
    void Start()
    {
        portail.SetActive(false);

        TempsDepuisDernierPassage = 0;
        passéDansFrame = false;
        scriptGestionCaméra = caméra.GetComponent<GestionCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        TempsDepuisDernierPassage += Time.deltaTime;
        passéDansFrame = false;
    }

    private void OnTriggerEnter(Collider other)
    {     
            if (portailOpposé.activeSelf)
            {

            if (other.transform.IsChildOf(personnage.transform) && personnage.GetComponent<GestionMouvement>().TientObjet)
            {
                personnage.GetComponent<GestionMouvement>().RelacherObjet();
            }
            AudioSource.PlayClipAtPoint(SonTeleportation, portailOpposé.transform.position);

            float vitesse = other.GetComponent<Rigidbody>().velocity.magnitude;
            Vector3 normale = portailOpposé.transform.forward;
            
            Debug.Log(other.GetComponent<Rigidbody>().velocity);

            float premièreVelocity = Mathf.Sqrt(Mathf.Pow(other.GetComponent<Rigidbody>().velocity.x, 2) + Mathf.Pow(other.GetComponent<Rigidbody>().velocity.y, 2) + Mathf.Pow(other.GetComponent<Rigidbody>().velocity.z, 2));
            other.GetComponent<Rigidbody>().velocity = (normale * premièreVelocity - (portailOpposé.transform.forward * 10));
            
            other.transform.position = portailOpposé.transform.position + (portailOpposé.transform.forward * 10);
            angle = portailOpposé.transform.eulerAngles.y - other.transform.eulerAngles.y;

            if(Physics.gravity.y > 0 )
            {
                angle *= -1;
            }

            if (other.name == "Personnage")
            {
                scriptGestionCaméra.degréRotation = angle;
                Debug.Log(angle);
                TempsDepuisDernierPassage = 0;
            }
        }
    }
    
}
