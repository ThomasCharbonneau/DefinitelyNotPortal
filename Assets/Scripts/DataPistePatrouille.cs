using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

class DataPistePatrouille
{
    const string CHEMIN = "Assets/Resources/";
    const int MAX_DIMENSION_INITIALE = 8;
    const float VARIATION_TEMPORELLE = 0.125f;

    const string NOM_FICHIER_X = "SplineX.txt";
    const string NOM_FICHIER_Y = "SplineY.txt";

    const int DIMENSIONS_GRILLE_MAX = 128;

    float[,] TableauSplinesX; //Les tableaux qui contiennent les valeurs des coefficients des splines
    float[,] TableauSplinesY;

    public DataPistePatrouille()
    {
        GénérerListePointsPrincipaux();

        GénérerTableauxSplines();

        GénérerListePointsDePatrouille();
    }

    void GénérerListePointsPrincipaux()
    {
        PointsPrincipaux = new List<Vector2>();

        List<int> pointsBruts = new List<int>() { 7, 1, 7, 2, 6, 3, 5, 2, 4, 3, 4, 4, 4, 5, 3, 6, 2, 6, 1, 5, 2, 4, 1, 3, 1, 2, 2, 2, 2, 1, 3, 1, 4, 2, 5, 1 };
        int i = 0;
        while (i < pointsBruts.Count)
        {
            PointsPrincipaux.Add(new Vector2(pointsBruts[i] * (DIMENSIONS_GRILLE_MAX / MAX_DIMENSION_INITIALE), pointsBruts[i + 1] * (DIMENSIONS_GRILLE_MAX / MAX_DIMENSION_INITIALE)));
            i = i + 2;
        }
    }

    void GénérerTableauxSplines()
    {
        TableauSplinesX = new float[4, PointsPrincipaux.Count];
        TableauSplinesY = new float[4, PointsPrincipaux.Count];

        StreamReader fichierLectureX = new StreamReader(CHEMIN + NOM_FICHIER_X);
        StreamReader fichierLectureY = new StreamReader(CHEMIN + NOM_FICHIER_Y);

        int j = 0;

        while (!fichierLectureX.EndOfStream)
        {
            string[] tableauLigneX = fichierLectureX.ReadLine().Split('\t');
            string[] tableauLigneY = fichierLectureY.ReadLine().Split('\t');

            for (int i = 0; i < tableauLigneX.Length; i++)
            {
                TableauSplinesX[i, j] = float.Parse(tableauLigneX[i]);
                TableauSplinesY[i, j] = float.Parse(tableauLigneY[i]);
            }
            j++;
        }
        fichierLectureX.Close();
        fichierLectureY.Close();
    }

    void GénérerListePointsDePatrouille()
    {
        PointsDePatrouille = new List<Vector2>();

        int i;
        for (float t = 0; (int)Mathf.Floor(t) < PointsPrincipaux.Count; t += VARIATION_TEMPORELLE)
        {
            i = (int)Mathf.Floor(t);

            float coordonnéeX = (TableauSplinesX[0, i] + Mathf.Pow(t, 3) * TableauSplinesX[1, i] + Mathf.Pow(t, 2) * TableauSplinesX[2, i] + t * TableauSplinesX[3, i]) * (DIMENSIONS_GRILLE_MAX / MAX_DIMENSION_INITIALE);
            float coordonnéeY = (TableauSplinesY[0, i] + Mathf.Pow(t, 3) * TableauSplinesY[1, i] + Mathf.Pow(t, 2) * TableauSplinesY[2, i] + t * TableauSplinesY[3, i]) * (DIMENSIONS_GRILLE_MAX / MAX_DIMENSION_INITIALE);

            PointsDePatrouille.Add(new Vector2(coordonnéeX, coordonnéeY));
        }
    }
    
    // Liste des points principaux
    List<Vector2> PointsPrincipaux;

    // Liste des points de patrouille
    List<Vector2> PointsDePatrouille;

    public List<Vector2> GetPointsCube()
    {
        List<Vector2> nouvelleListe = new List<Vector2>();

        foreach (Vector2 v in PointsPrincipaux)
        {
            nouvelleListe.Add(v);
        }

        return nouvelleListe;
    }


    public List<Vector2> GetPointsDePatrouille()
    {
        List<Vector2> nouvelleListe = new List<Vector2>();

        foreach (Vector2 v in PointsDePatrouille)
        {
            nouvelleListe.Add(v);
        }

        return nouvelleListe;
    }
}
