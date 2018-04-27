using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

class PistePatrouille : MonoBehaviour
{
    [SerializeField] TextAsset TxtPointsBruts;
    [SerializeField] string NomFichierSplineX;
    [SerializeField] string NomFichierSplineY;

    const string CHEMIN = "Assets/Ressources/";
    const float VARIATION_TEMPORELLE = 0.05f;

    float[,] TableauSplinesX; //Les tableaux qui contiennent les valeurs des coefficients des splines
    float[,] TableauSplinesY;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        GénérerListePointsPrincipaux();

        GénérerTableauxSplines();

        GénérerListePointsDePatrouille();

        DessinerPiste();

        //Pour test :
        ColorerPiste();
    }

    void GénérerListePointsPrincipaux()
    {

        List<string> pointsBruts = TxtPointsBruts.text.Split(',').ToList();

        PointsPrincipaux = new List<Vector2>();

        int i = 0;
        while (i < pointsBruts.Count)
        {
            PointsPrincipaux.Add(new Vector2(int.Parse(pointsBruts[i]), int.Parse(pointsBruts[i + 1])));
            i = i + 2;
        }
    }

    void GénérerTableauxSplines()
    {
        TableauSplinesX = new float[4, PointsPrincipaux.Count];
        TableauSplinesY = new float[4, PointsPrincipaux.Count];

        StreamReader fichierLectureX = new StreamReader(CHEMIN + NomFichierSplineX);
        StreamReader fichierLectureY = new StreamReader(CHEMIN + NomFichierSplineY);

        int j = 0;

        while (!fichierLectureX.EndOfStream)
        {
            Debug.Log("j" + j);
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

            float coordonnéeX = (Mathf.Pow(t, 3) * TableauSplinesX[0, i] + Mathf.Pow(t, 2) * TableauSplinesX[1, i] + t * TableauSplinesX[2, i] + TableauSplinesX[3, i]);
            float coordonnéeY = (Mathf.Pow(t, 3) * TableauSplinesY[0, i] + Mathf.Pow(t, 2) * TableauSplinesY[1, i] + t * TableauSplinesY[2, i] + TableauSplinesY[3, i]);
            //float coordonnéeY = (TableauSplinesY[0, i] + Mathf.Pow(t, 3) * TableauSplinesY[1, i] + Mathf.Pow(t, 2) * TableauSplinesY[2, i] + t * TableauSplinesY[3, i]);

            PointsDePatrouille.Add(new Vector2(coordonnéeX, coordonnéeY));
        }
    }
    
    // Liste des points principaux
    List<Vector2> PointsPrincipaux;

    // Liste des points de patrouille
    List<Vector2> PointsDePatrouille;

    public List<Vector2> GetPointsDePatrouille()
    {
        List<Vector2> nouvelleListe = new List<Vector2>();

        foreach (Vector2 v in PointsDePatrouille)
        {
            nouvelleListe.Add(v);
        }

        return nouvelleListe;
    }

    void DessinerPiste()
    {
        lineRenderer.positionCount = PointsDePatrouille.Count;

        for(int i = 0; i < PointsDePatrouille.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(PointsDePatrouille[i].x, 0.5f, PointsDePatrouille[i].y));
        }
    }

    public void ColorerPiste()
    {
        GetComponent<Renderer>().material.color = new Color(0,255, 223);
    }

    public void DécolorerPiste()
    {
        GetComponent<Material>().color = Color.grey;
    }
}
