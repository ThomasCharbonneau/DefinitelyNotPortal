using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMouvement : MonoBehaviour
{

    float Vitesse = 10;
    float VitesseSaut = 6.5f; //Plus une force qu'une vitesse? (En Newtons)
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
        if (Input.GetKey(KeyCode.Space))
        {
            personnage.AddForce(Vector3.up * VitesseSaut);
        }

    }
    // Update is called once per frame
    void Update()
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

        if ((EstAuSol) && Input.GetKey(KeyCode.Space))
        {
            //personnage.AddForce(Vector3.up * VitesseSaut);
            personnage.velocity = new Vector3(0f, VitesseSaut, 0f);
            EstAuSol = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKey("w")) //déplacement vers l'avant
        {
            déplacementAvant = Vector3.forward * Time.deltaTime * Vitesse;

            if (Input.GetKey(KeyCode.LeftShift)) //Peut sprinter quand personnage dans les airs?
            {
                déplacementAvant *= CoefficientSprint;
            }

            transform.Translate(déplacementAvant);
        }
        if (Input.GetKey("a")) //déplacement de coté vers la gauche
        {
            transform.Translate(Vector3.left * Time.deltaTime * Vitesse);
        }
        if (Input.GetKey("s")) //déplacement vers l'arrière
        {
            //personnage.AddForce()
            transform.Translate(Vector3.back * Time.deltaTime * Vitesse);
        }
        if (Input.GetKey("d")) //déplacement de coté vers la droite
        {
            transform.Translate(Vector3.right * Time.deltaTime * Vitesse);
        }
        //if (Input.GetKeyDown(KeyCode.J)) { this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Plancher")) { EstAuSol = true; }
    }
}
