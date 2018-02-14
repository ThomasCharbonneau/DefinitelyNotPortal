using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMouvement : MonoBehaviour
{

    const float Vitesse = 10;
    const float ForceSaut = 9f;
    const float ForceDéplacement = 30f;
    const float CoefficientSprint = 2.5f;
    const float COEFFICIENT_CHUTE = 1.5F;

    const int VITESSE_MARCHE_MAX = 25;
    const int VITESSE_SPRINT_MAX = 35;
    int VitesseHorizontaleMax;
    Vector3 vitesseHorizontale;

    public bool EstAuSol;
    public bool TientObjet; //Si le personnage a un objet ou non dans ses mains

    Rigidbody ObjetTenu; //Le rigidbody de l'objet que l'on tient

    private Rigidbody personnage;

    private Vector3 déplacementAvant;

    [SerializeField] Camera Caméra;

    // Use this for initialization
    void Start()
    {
        EstAuSol = true;
        TientObjet = false;
        personnage = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        RendreSautRéaliste();
        
        if (EstAuSol)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                personnage.velocity += (Vector3.up * ForceSaut);

                EstAuSol = false;
            }

            if (Input.GetKey("w")) //déplacement vers l'avant
            {
                déplacementAvant = personnage.transform.forward * ForceDéplacement * Time.deltaTime;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    VitesseHorizontaleMax = VITESSE_SPRINT_MAX;
                    déplacementAvant *= CoefficientSprint;
                }
                else
                {
                    VitesseHorizontaleMax = VITESSE_MARCHE_MAX;
                }

                personnage.velocity += déplacementAvant;
            }

            if (Input.GetKey("a")) //déplacement de coté vers la gauche
            {
                personnage.velocity += -personnage.transform.right * ForceDéplacement * Time.deltaTime;
            }

            if (Input.GetKey("s")) //déplacement vers l'arrière
            {
                personnage.velocity += -personnage.transform.forward * ForceDéplacement * Time.deltaTime;
            }

            if (Input.GetKey("d")) //déplacement de coté vers la droite
            {
                personnage.velocity += personnage.transform.right * ForceDéplacement * Time.deltaTime;
            }

            //

            vitesseHorizontale = (new Vector3(personnage.velocity.x, 0, personnage.velocity.z));
            if (vitesseHorizontale.magnitude > VitesseHorizontaleMax)
            {
                personnage.velocity = new Vector3(0, personnage.velocity.y, 0) + vitesseHorizontale.normalized * VitesseHorizontaleMax;
            }

            //
        }

        if (Input.GetKeyDown("e")) //prendre un objet devant soi
        {
            if(TientObjet)
            {
                TientObjet = false;

                ObjetTenu.transform.parent = null;
                ObjetTenu.useGravity = true;

                ObjetTenu.freezeRotation = false;
            }
            else
            {
                PrendreObjet();
            }
        }

        if (TientObjet) //Changer pour ne pas qu'objet clip avec les murs / autres...
        {
            //Vérifier si l'objet a une collision et déplacer accordément par rapport aux normale(s) ?

            ObjetTenu.transform.parent = Caméra.transform;
            ObjetTenu.useGravity = false;

            ObjetTenu.transform.position = Caméra.transform.position + Caméra.transform.forward * 10;

            ObjetTenu.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            ObjetTenu.transform.position = Vector3.MoveTowards(ObjetTenu.transform.position, Caméra.transform.position + Caméra.transform.forward * 10, 0.1f);
        }

        //Debug.Log(personnage.velocity.magnitude);
    }

    void RendreSautRéaliste()
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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
        {
            EstAuSol = true;
        }
    }

    private void PrendreObjet()
    {
        RaycastHit hit;
        Ray ray = Caméra.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        if (hit.rigidbody != null)
        {
            ObjetTenu = hit.rigidbody;
        }

        ObjetTenu.freezeRotation = true;

        TientObjet = true;

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
    }
}
