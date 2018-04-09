using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptNoeud : MonoBehaviour
{
    float Cout = int.MaxValue;
    GameObject NoeudParent = null;
    List<GameObject> NoeudsVoisins;
    bool Disponible = true;

    public int Rangée;
    public int Colonne;

    void Awake()
    {
        ResetNoeud();
        NoeudsVoisins = new List<GameObject>();
    }

    public void ResetNoeud()
    {
        Cout = int.MaxValue;
        NoeudParent = null;
    }

    public void SetNoeudParent(GameObject node)
    {
        NoeudParent = node;
    }

    public void SetCout(float value)
    {
        Cout = value;
    }

    /// <param name="value">boolean</param>
    public void SetDisponibilité(bool value)
    {
        Disponible = value;
    }

    public void AjouterNoeudVoisin(GameObject node)
    {
        //Problème ici...

        Debug.Log("Count NoeudsVosin : " + NoeudsVoisins.Count);

        Debug.Log("A");
        NoeudsVoisins.Add(node);
        Debug.Log("B");
    }

    public List<GameObject> GetNoeudsVoisins()
    {
        List<GameObject> result = NoeudsVoisins;
        return result;
    }

    public float GetCout()
    {
        float result = Cout;
        return result;

    }

    public GameObject GetNoeudParent()
    {
        GameObject result = NoeudParent;
        return result;
    }

    public bool EstDisponible()
    {
        bool result = Disponible;
        return result;
    }
}

