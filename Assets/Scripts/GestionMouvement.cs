using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMouvement : MonoBehaviour
{

    const float Vitesse = 10;
    const float ForceSaut = 9f;
    const float ForceDéplacement = 300f;
    const float CoefficientSprint = 2.5f;
    const float COEFFICIENT_CHUTE = 1.5F;

    public bool EstAuSol;

    private Rigidbody personnage;

    private Vector3 déplacementAvant;

    [SerializeField] Camera Caméra;

    [SerializeField] GameObject Sol;

    // Use this for initialization
    void Start ()
    {
        EstAuSol = true;
        personnage = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Pour tester. Mauvaise écriture...
        if (EstAuSol)
        {
            Sol.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            Sol.GetComponent<Renderer>().material.color = Color.green;
        }
        //

        //Continue d'accélérer indéfiniment. Devrait mettre restriction.
        if (EstAuSol)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                //personnage.AddForce(Vector3.up * VitesseSaut);
                //personnage.velocity = new Vector3(0f, VitesseSaut, 0f);

                personnage.velocity += (Vector3.up * ForceSaut);

                EstAuSol = false;
            }

            if (Input.GetKey("w")) //déplacement vers l'avant
            {
                déplacementAvant = personnage.transform.forward * Time.deltaTime * ForceDéplacement;

                if (Input.GetKey(KeyCode.LeftShift)) //Peut sprinter quand personnage dans les airs?
                {
                    déplacementAvant *= CoefficientSprint;
                }

                personnage.velocity += déplacementAvant;
            }

            if (Input.GetKey("a")) //déplacement de coté vers la gauche
            {
                //transform.Translate(Vector3.left * Time.deltaTime * Vitesse);
                personnage.velocity += -personnage.transform.right * ForceDéplacement * Time.deltaTime;

            }

            if (Input.GetKey("s")) //déplacement vers l'arrière
            {
                personnage.velocity += -personnage.transform.forward * ForceDéplacement * Time.deltaTime;
            }

            if (Input.GetKey("d")) //déplacement de coté vers la droite
            {
                personnage.velocity += personnage.transform.right * ForceDéplacement * Time.deltaTime;
                //personnage.AddRelativeForce(Vector3.right * ForceDéplacement * Time.deltaTime);
            }

            if (Input.GetKey("e")) //prendre un objet devant soi
            {
                PrendreObjet();
            }
        }
        
        //if (Input.GetKeyDown(KeyCode.J)) { this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f); }

    }

    // Update is called once per frame
    void Update()
    {
        if (personnage.velocity.y < 0)
        {
            personnage.velocity += (Vector3.up * Physics.gravity.y * (COEFFICIENT_CHUTE) * Time.deltaTime);
        }
        else if (personnage.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            personnage.velocity += (Vector3.up * Physics.gravity.y * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sol"))
        {
            EstAuSol = true;
        }
    }

    private void PrendreObjet()
    {
        RaycastHit hit;
        Ray ray = Caméra.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        if (hit.rigidbody.isKinematic) //Cas ou il n'y a pas de rigidbody...
        {
            hit.transform.position = Caméra.transform.position + transform.forward * 10;
        }

    }
}
