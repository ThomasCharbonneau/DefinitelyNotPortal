using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GestionPathfinding : MonoBehaviour
{
    private GameObject[] noeuds;

    public List<Vector3> TrouverCheminPlusCourt(Transform noeudDépart, Transform noeudFinal)
    {
        noeuds = GameObject.FindGameObjectsWithTag("Noeud");

        List<Vector3> ListePointsRésultat = new List<Vector3>();
        Transform noeud = FaireAlgorithmeDijkstra(noeudDépart, noeudFinal);

        while (noeud != null)
        {
            ListePointsRésultat.Add(noeud.position);
            Noeud noeudActuel = noeud.GetComponent<Noeud>();
            noeud = noeudActuel.GetNoeudParent();
        }

        ListePointsRésultat.Reverse();
        return ListePointsRésultat;
    }

    private Transform FaireAlgorithmeDijkstra(Transform noeudDépart, Transform noeudFinal)
    {
        List<Transform> listeTransformInexplorés = new List<Transform>();

        foreach (GameObject g in noeuds)
        {
            Noeud n = g.GetComponent<Noeud>();
            if (n.EstDisponible())
            {
                n.ResetNoeud();
                listeTransformInexplorés.Add(g.transform);
            }
        }

        Noeud NoeudDépart = noeudDépart.GetComponent<Noeud>();
        NoeudDépart.SetCout(0);

        while (listeTransformInexplorés.Count > 0)
        {
            listeTransformInexplorés.Sort((x, y) => x.GetComponent<Noeud>().GetCout().CompareTo(y.GetComponent<Noeud>().GetCout()));

            Transform current = listeTransformInexplorés[0];

            listeTransformInexplorés.Remove(current);

            Noeud noeudActuel = current.GetComponent<Noeud>();
            List<Transform> listeNoeudsVoisin = noeudActuel.GetNoeudsVoisin();
            foreach (Transform noeudVoisin in listeNoeudsVoisin)
            {
                Noeud node = noeudVoisin.GetComponent<Noeud>();

                //Seulement les noeuds voisins et disponibles
                if (listeTransformInexplorés.Contains(noeudVoisin) && node.EstDisponible())
                {
                    float distance = Vector3.Distance(noeudVoisin.position, current.position);
                    distance = noeudActuel.GetCout() + distance;

                    if (distance < node.GetCout())
                    {
                        node.SetCout(distance);
                        node.SetNoeudParent(current);
                    }
                }
            }
        }

        return noeudFinal;
    }
}