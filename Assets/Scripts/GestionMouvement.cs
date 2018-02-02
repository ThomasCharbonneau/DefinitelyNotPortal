using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMouvement : MonoBehaviour
{

    float Vitesse = 10;
    float ForceSaut = 500f; //Plus une force qu'une vitesse? (En Newtons)
    float ForceDéplacement = 3000f;
    float CoefficientSprint = 2.5f;

    public bool EstAuSol;

    private Rigidbody personnage;

    private Vector3 déplacementAvant;

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

                personnage.AddRelativeForce(Vector3.up * ForceSaut);

                EstAuSol = false;
            }

            if (Input.GetKey("w")) //déplacement vers l'avant
            {
                déplacementAvant = Vector3.forward * Time.deltaTime * ForceDéplacement;

                if (Input.GetKey(KeyCode.LeftShift)) //Peut sprinter quand personnage dans les airs?
                {
                    déplacementAvant *= CoefficientSprint;
                }

                personnage.AddRelativeForce(déplacementAvant);
            }

            if (Input.GetKey("a")) //déplacement de coté vers la gauche
            {
                //transform.Translate(Vector3.left * Time.deltaTime * Vitesse);
                personnage.AddRelativeForce(Vector3.left * ForceDéplacement * Time.deltaTime);

            }

            if (Input.GetKey("s")) //déplacement vers l'arrière
            {
                personnage.AddRelativeForce(Vector3.back * ForceDéplacement * Time.deltaTime);
            }

            if (Input.GetKey("d")) //déplacement de coté vers la droite
            {
                personnage.AddRelativeForce(Vector3.right * ForceDéplacement * Time.deltaTime);
            }
        }
        
        //if (Input.GetKeyDown(KeyCode.J)) { this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f); }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sol"))
        {
            EstAuSol = true;
        }
    }
}
