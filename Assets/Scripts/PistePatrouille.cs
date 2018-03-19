using System.Collections.Generic;
using UnityEngine;

public class PistePatrouille : MonoBehaviour
{
    [SerializeField] GameObject Drone;

    List<Vector3> PointsDePatrouilleAdaptés; //Les points de patrouille adaptés

    GestionDrone dataDrone;
    DataPistePatrouille dataPiste;

    const int DÉPLACEMENT_X = 10;
    const int DÉPLACEMENT_Z = 18;

    int IndicePositionDrone;

    void Awake()
    {
        dataPiste = new DataPistePatrouille();
        dataDrone = Drone.GetComponent<GestionDrone>();

        AdapterPointsDePatrouille();
        IndicePositionDrone = 0;
    }

    /// <summary>
    /// Fonction qui transfère les points de patrouille en points 3d selon la hauteur du terrain et qui les adapte à l'échelle (scale)
    /// </summary>
    void AdapterPointsDePatrouille()
    {
        List<Vector2> PointsDePatrouille = dataPiste.GetPointsDePatrouille();
        PointsDePatrouilleAdaptés = new List<Vector3>();

        float coordonnéeX;
        float coordonnéeZ;

        for (int i = 0; i < PointsDePatrouille.Count; i++)
        {
            coordonnéeX = PointsDePatrouille[i].x + DÉPLACEMENT_X;
            coordonnéeZ = PointsDePatrouille[i].y + DÉPLACEMENT_Z;

            //Modifier pour que hauteur automatique soit déterminée par drone.

            PointsDePatrouilleAdaptés.Add(new Vector2(coordonnéeX, coordonnéeZ));
        }
    }

    private void Update()
    {
        if (dataDrone.Mode == ModeDrone.PATROUILLE)
        {
            if (dataDrone.transform.position != PointsDePatrouilleAdaptés[IndicePositionDrone])
            {
                dataDrone.DéplacerVersPoint(PointsDePatrouilleAdaptés[IndicePositionDrone]);
            }
            else
            {
                IndicePositionDrone++;

                if (IndicePositionDrone == PointsDePatrouilleAdaptés.Count)
                {
                    IndicePositionDrone = 0;
                }
            }
        }
    }
}
