using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noeud : MonoBehaviour
{

    private float Cout = int.MaxValue;
    private Transform NoeudParent = null;
    private List<Transform> NoeudsVosins;
    private bool Disponible = true;

    // Use this for initialization
    void Start()
    {
        ResetNoeud();
    }

    public void ResetNoeud()
    {
        Cout = int.MaxValue;
        NoeudParent = null;
    }

    public void SetNoeudParent(Transform node)
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

    //Inutile. À enlever...?
    public void AddNeighbourNode(Transform node)
    {
        NoeudsVosins.Add(node);
    }

    public List<Transform> GetNoeudsVoisin()
    {
        List<Transform> result = NoeudsVosins;
        return result;
    }

    public float GetCout()
    {
        float result = Cout;
        return result;

    }

    public Transform GetNoeudParent()
    {
        Transform result = NoeudParent;
        return result;
    }

    public bool EstDisponible()
    {
        bool result = Disponible;
        return result;
    }
}

