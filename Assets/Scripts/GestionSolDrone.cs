using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSolDrone : MonoBehaviour
{
    [SerializeField] Transform PrefabNoeud;

    const float ÉTENDUE = 200; //L'étendue du sol en unités de Unity
    const int DIMENSION_CHARPENTE = 20;
    const int NB_TRIANGLES_PAR_TUILE = 2;
    const int NB_SOMMETS_PAR_TRIANGLE = 3;

    Mesh Maillage;
    Vector3[] Sommets;  // Le tableau qui contiendra les différents sommets (vertices)

    Vector3 Origine; // L'origine est placée de manière à ce que la grille soit centrée au point (0, 0, 0).
    Vector3 DeltaTexture;

    int NbColonnes;
    int NbRangées;
    int NbSommets;
    int NbTriangles;

    public List<Transform> ListeNoeuds;

    void Start()
    {
        ListeNoeuds = new List<Transform>();

        CalculerDonnéesDeBase();
        GénérerTriangles();

        transform.gameObject.GetComponent<MeshCollider>().sharedMesh = Maillage;

        TrouverObstacles();
    }

    private void CalculerDonnéesDeBase()
    {
        Origine = new Vector3(-ÉTENDUE / 2, 0, -ÉTENDUE / 2); //Vector3.zero;  
        NbColonnes = DIMENSION_CHARPENTE;
        NbRangées = DIMENSION_CHARPENTE;
        NbSommets = (NbColonnes + 1) * (NbRangées + 1);
        NbTriangles = NbColonnes * NB_TRIANGLES_PAR_TUILE * NbRangées;
        DeltaTexture = new Vector2(ÉTENDUE / NbColonnes, ÉTENDUE / NbRangées);
    }

    private void GénérerTriangles()
    {
        Maillage = new Mesh();
        GetComponent<MeshFilter>().mesh = Maillage;

        Maillage.name = "Surface";
        GénérerSommetsEtNoeuds();
        GénérerListeTriangles();
    }

    private void GénérerSommetsEtNoeuds()
    {
        Sommets = new Vector3[NbSommets];
        Vector2[] coordonnéesTexture = new Vector2[NbSommets]; // Le tableau qui contiend les différentes coordonnées de textures (uv) de la primitive

        int i = 0;
        for (int j = 0; j < NbRangées + 1; j++)
        {
            for (int k = 0; k < NbColonnes + 1; k++)
            {
                Sommets[i] = Origine + new Vector3(DeltaTexture.x * k, 0, DeltaTexture.y * j);

                Transform noeud = Instantiate(PrefabNoeud, Sommets[i], Quaternion.identity, gameObject.transform);
                noeud.name = "neud # " + i;
                ListeNoeuds.Add(noeud);

                coordonnéesTexture[i] = new Vector3((DeltaTexture.x * k) / ÉTENDUE, (DeltaTexture.y * j) / ÉTENDUE, 0);

                i++;
            }
        }

        //

        Maillage.vertices = Sommets;
        Maillage.uv = coordonnéesTexture;
    }

    private void GénérerListeTriangles()
    {
        int[] triangles = new int[NbTriangles * NB_SOMMETS_PAR_TRIANGLE];

        //
        // Vous devez ici produire un algorithme qui permettra de créer le tableau contenant la "description" des triangles 
        // composant la surface.
        //

        int triangleActuel = 0;
        int rangéeActuelle = 0;
        int colonneActuelle = 0;

        for (int i = 0; i < triangles.Length; i++)
        {
            if (triangleActuel % 2 == 0) //Pour un premier triangle d'une tuile
            {
                if (i % NB_SOMMETS_PAR_TRIANGLE == 0) //Si premier sommet du triangle
                {
                    triangles[i] = rangéeActuelle * (NbColonnes + 1) + colonneActuelle;
                }
                if (i % NB_SOMMETS_PAR_TRIANGLE == 1) //Si deuxième sommet du triangle
                {
                    triangles[i] = (rangéeActuelle + 1) * (NbColonnes + 1) + colonneActuelle;
                }
                if (i % NB_SOMMETS_PAR_TRIANGLE == 2) //Si troisième sommet du triangle
                {
                    triangles[i] = rangéeActuelle * (NbColonnes + 1) + colonneActuelle + 1;
                }
            }
            else //Pour un deuxième triangle d'une tuile
            {
                if (i % NB_SOMMETS_PAR_TRIANGLE == 0) //Si premier sommet du triangle
                {
                    triangles[i] = (rangéeActuelle + 1) * (NbColonnes + 1) + colonneActuelle;
                }
                if (i % NB_SOMMETS_PAR_TRIANGLE == 1) //Si deuxième sommet du triangle
                {
                    triangles[i] = (rangéeActuelle + 1) * (NbColonnes + 1) + colonneActuelle + 1;
                }
                if (i % NB_SOMMETS_PAR_TRIANGLE == 2) //Si troisième sommet du triangle
                {
                    triangles[i] = rangéeActuelle * (NbColonnes + 1) + colonneActuelle + 1;
                }
            }

            if (i % NB_SOMMETS_PAR_TRIANGLE == 2) //On change de triangle
            {
                triangleActuel++;
            }

            if (i % (2 * NB_SOMMETS_PAR_TRIANGLE) == (2 * NB_SOMMETS_PAR_TRIANGLE - 1))
            {
                colonneActuelle++;

                if (colonneActuelle == NbColonnes) //Quand on arrive à la fin de la ligne
                {
                    colonneActuelle = 0;
                    rangéeActuelle++;
                }
            }
        }

        Maillage.triangles = triangles;
        Maillage.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TrouverObstacles()
    {
        //GameObject[] TableauObstacles = GameObject.FindGameObjectsWithTag("Mur");

        //for (int i = 0; i < TableauObstacles.Length; i++)
        //{
        //    Vector3[] TableauSommetsObstacle = TableauObstacles[i].GetComponent<Mesh>().vertices;
        //    //Rendre en localworldSpace ou vice versa

        //    for (int j = 0; j < TableauObstacles.Length; j++)
        //    {
        //        if (TableauSommetsObstacle[j].y == 0)
        //        {
        //            //foreach(Noeud n in ListeNoeuds.)
        //            //foreach() dans grille, dire invalide, ajouter à la liste à surveiller aussi.
        //            //TableauSommets[j]
        //        }
        //    }
        //}

        RaycastHit hit;
        foreach (Transform t in ListeNoeuds)
        {
            Ray ray = new Ray(t.position, Vector3.up);
            Physics.Raycast(ray, out hit);

            //if(hit.collider.GetComponent<GameObject>().name == "plafond")
            {
                if (hit.collider.tag == "Mur")
                {
                    t.GetComponent<Noeud>().SetDisponibilité(false);

                    //À arranger
                    //foreach (Transform nVoisin in t.GetComponent<Noeud>().GetNoeudsVoisin())
                    //{
                    //    nVoisin.GetComponent<Noeud>().SetDisponibilité(false);
                    //    Debug.Log(nVoisin.name);
                    //}
                    
                    Debug.Log(t.name + " : " + hit.collider.name);
                }
            }
        }
    }
}