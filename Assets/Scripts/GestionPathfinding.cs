using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GestionPathfinding : MonoBehaviour
{
    private GameObject[] noeuds;

    public List<Vector3> TrouverCheminPlusCourt(GameObject noeudDépart, GameObject noeudFinal)
    {
        noeuds = GameObject.FindGameObjectsWithTag("Noeud");

        List<Vector3> ListePointsRésultat = new List<Vector3>();
        GameObject noeud = FaireAlgorithmeDijkstra(noeudDépart, noeudFinal);

        while (noeud != null)
        {
            ListePointsRésultat.Add(noeud.transform.position);
            ScriptNoeud noeudActuel = noeud.GetComponent<ScriptNoeud>();
            noeud = noeudActuel.GetNoeudParent();
        }

        ListePointsRésultat.Reverse();
        return ListePointsRésultat;
    }

    private GameObject FaireAlgorithmeDijkstra(GameObject noeudDépart, GameObject noeudFinal)
    {
        List<GameObject> listeTransformInexplorés = new List<GameObject>();

        foreach (GameObject g in noeuds)
        {
            ScriptNoeud n = g.GetComponent<ScriptNoeud>();
            if (n.EstDisponible())
            {
                n.ResetNoeud();
                listeTransformInexplorés.Add(g);
            }
        }

        ScriptNoeud NoeudDépart = noeudDépart.GetComponent<ScriptNoeud>();
        NoeudDépart.SetCout(0);

        while (listeTransformInexplorés.Count > 0)
        {
            listeTransformInexplorés.Sort((x, y) => x.GetComponent<ScriptNoeud>().GetCout().CompareTo(y.GetComponent<ScriptNoeud>().GetCout()));

            GameObject current = listeTransformInexplorés[0];

            listeTransformInexplorés.Remove(current);

            ScriptNoeud noeudActuel = current.GetComponent<ScriptNoeud>();
            List<GameObject> listeNoeudsVoisin = noeudActuel.GetNoeudsVoisins();

            foreach (GameObject noeudVoisin in listeNoeudsVoisin)
            {
                ScriptNoeud node = noeudVoisin.GetComponent<ScriptNoeud>();

                //Seulement les noeuds voisins et disponibles
                if (listeTransformInexplorés.Contains(noeudVoisin) && node.EstDisponible())
                {
                    float distance = Vector3.Distance(noeudVoisin.transform.position, current.transform.position);
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