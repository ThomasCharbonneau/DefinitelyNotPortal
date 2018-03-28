using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSol : MonoBehaviour
{
    const float ÉTENDUE = 200; //L'étendue du sol en unités de Unity
    const int ÉTENDUE_CHARPENTE = 20;
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

    void Awake ()
    {
        CalculerDonnéesDeBase();
        GénérerTriangles();

        transform.gameObject.GetComponent<MeshCollider>().sharedMesh = Maillage;
    }

    private void CalculerDonnéesDeBase()
    {
        Origine = new Vector3(-ÉTENDUE / 2, 0, -ÉTENDUE / 2); //Vector3.zero;  
        NbColonnes = ÉTENDUE_CHARPENTE;
        NbRangées = ÉTENDUE_CHARPENTE;
        NbSommets = (NbColonnes + 1) * (NbRangées + 1);
        NbTriangles = NbColonnes * NB_TRIANGLES_PAR_TUILE * NbRangées;
        DeltaTexture = new Vector2(ÉTENDUE / NbColonnes, ÉTENDUE / NbRangées);
    }

    private void GénérerTriangles()
    {
        Maillage = new Mesh();
        GetComponent<MeshFilter>().mesh = Maillage;

        Maillage.name = "Surface";
        GénérerSommets();
        GénérerListeTriangles();
    }

    private void GénérerSommets()
    {
        Sommets = new Vector3[NbSommets];
        Vector2[] coordonnéesTexture = new Vector2[NbSommets]; // Le tableau qui contiendra les différentes coordonnées de textures (uv) de la primitive

        //
        //Vous devez ici produire un algorithme qui permettra de créer le tableau contenant la position des sommets, 
        //ainsi que le tableau contenant les coordonnées des textures.
        //

        int i = 0;

        for (int j = 0; j < NbRangées + 1; j++)
        {
            for (int k = 0; k < NbColonnes + 1; k++)
            {
                Sommets[i] = Origine + new Vector3(DeltaTexture.x * k, 0, DeltaTexture.y * j);

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
    void Update ()
    {
		
	}
}
