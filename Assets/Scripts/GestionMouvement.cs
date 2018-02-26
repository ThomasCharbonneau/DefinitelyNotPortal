using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMouvement : MonoBehaviour
{
    const float FORCE_RELACHEMENT = 1000f; //La froce avec laquelle l'objet tenu est relaché vers l'avant.
    const float FORCE_SAUT = 9f;
    const float FORCE_DÉPLACEMENT = 30f;
    const float COEFFICIENTSPRINT = 2.5f;
    const float COEFFICIENT_CHUTE = 1.5F;

    const int DISTANCE_MAX_PRISE_OBJET = 15; //La distance maximale entre le joueur et l'objet qu'il peut prendre

    const int VITESSE_MARCHE_MAX = 25;
    const int VITESSE_SPRINT_MAX = 35;
    int VitesseHorizontaleMax;
    Vector3 vitesseHorizontale;

    public bool EstAuSol;
    public bool TientObjet; //Si le personnage a un objet ou non dans ses mains

    Rigidbody ObjetTenu; //Le rigidbody de l'objet que l'on tient

    Rigidbody personnage;

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
        //RendreSautRéaliste();
        if(!GestionCamera.PAUSE_CAMERA)
        {
            if (EstAuSol)
            {
                if (Input.GetKey(KeyCode.Space))
                {

                    personnage.velocity += (Vector3.up * FORCE_SAUT * Physics.gravity.y * (-1 / 9.81f));

                    EstAuSol = false;
                }

                if (Input.GetKey("w")) //déplacement vers l'avant
                {
                    déplacementAvant = personnage.transform.forward * FORCE_DÉPLACEMENT * Time.deltaTime;

                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        VitesseHorizontaleMax = VITESSE_SPRINT_MAX;
                        déplacementAvant *= COEFFICIENTSPRINT;
                    }
                    else
                    {
                        VitesseHorizontaleMax = VITESSE_MARCHE_MAX;
                    }

                    personnage.velocity += déplacementAvant;
                }

                if (Input.GetKey("a")) //déplacement de coté vers la gauche
                {
                    personnage.velocity += -personnage.transform.right * FORCE_DÉPLACEMENT * Time.deltaTime;
                }

                if (Input.GetKey("s")) //déplacement vers l'arrière
                {
                    personnage.velocity += -personnage.transform.forward * FORCE_DÉPLACEMENT * Time.deltaTime;
                }

                if (Input.GetKey("d")) //déplacement de coté vers la droite
                {
                    personnage.velocity += personnage.transform.right * FORCE_DÉPLACEMENT * Time.deltaTime;
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
                if (TientObjet)
                {
                    RelacherObjet();
                }
                else
                {
                    PrendreObjet();
                }
            }

            if (Input.GetMouseButton(2))
            {
                if (TientObjet)
                {
                    RelacherObjet();
                    //N'était pas correct quand l'objet était relaché parcequ'il traversait un portail.
                    ObjetTenu.AddForce(Caméra.transform.forward * FORCE_RELACHEMENT);
                }
            }

            if (TientObjet)
            {
                ObjetTenu.transform.position = Vector3.MoveTowards(ObjetTenu.transform.position, Caméra.transform.position + Caméra.transform.forward * 10, 0.4f);
            }

            //Debug.Log(personnage.velocity.magnitude);
        }
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

        if (hit.rigidbody != null && hit.distance <= DISTANCE_MAX_PRISE_OBJET)
        {
            ObjetTenu = hit.rigidbody;

            ObjetTenu.transform.parent = Caméra.transform;
            ObjetTenu.useGravity = false;
            ObjetTenu.freezeRotation = true;

            TientObjet = true;
        }
    }

    public void RelacherObjet()
    {
        TientObjet = false;

        ObjetTenu.transform.parent = null;
        ObjetTenu.useGravity = true;

        ObjetTenu.freezeRotation = false;
    }

    protected void LateUpdate()
    {
        
        if (Physics.gravity.y > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 180));

            //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 180);



        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y, 0));
            //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            
        }
       // Debug.Log(Caméra.transform.position);

    }
}
