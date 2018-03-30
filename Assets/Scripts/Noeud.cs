using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noeud : MonoBehaviour
{

    [SerializeField] private float weight = int.MaxValue;
    [SerializeField] private Transform NoeudParent = null;
    [SerializeField] private List<Transform> neighbourNode;
    [SerializeField] private bool walkable = true;

    // Use this for initialization
    void Start()
    {
        ResetNoeud();
    }

    /// <summary>
    /// Reset all the values in the nodes.
    /// </summary>
    public void ResetNoeud()
    {
        weight = int.MaxValue;
        NoeudParent = null;
    }

    public void SetNoeudParent(Transform node)
    {
        NoeudParent = node;
    }

    public void SetCout(float value)
    {
        weight = value;
    }

    /// <param name="value">boolean</param>
    public void SetDisponibilité(bool value)
    {
        walkable = value;
    }

    //Inutile. À enlever...?
    public void AddNeighbourNode(Transform node)
    {
        neighbourNode.Add(node);
    }

    public List<Transform> GetNoeudsVoisin()
    {
        List<Transform> result = neighbourNode;
        return result;
    }

    /// <summary>
    /// Get weight
    /// </summary>
    /// <returns>get weight in float.</returns>
    public float GetCout()
    {
        float result = weight;
        return result;

    }

    public Transform GetNoeudParent()
    {
        Transform result = NoeudParent;
        return result;
    }

    public bool EstDisponible()
    {
        bool result = walkable;
        return result;
    }
}

