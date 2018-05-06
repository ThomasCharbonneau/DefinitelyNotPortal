using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBoutonDrone : MonoBehaviour
{
    [SerializeField] GameObject ObjetDrone;

    GestionDrone ScriptGestionDrone;
    Vector3 positionInitialeDrone;

    //GameObject DroneInstancié;

    // Use this for initialization
    void Start ()
    {
        positionInitialeDrone = ObjetDrone.transform.position;
        //DroneInstancié = GameObject.Find("Drone");
    }
	
    public void RespawnerDrone()
    {
        //Si le drone est desactivé, en instantier un nouveau

        if(!ObjetDrone.activeSelf)
        {
            ScriptGestionDrone = ObjetDrone.GetComponent<GestionDrone>();

            //ScriptGestionDrone.Vie = GestionDrone.VIE_INITIALE;
            ObjetDrone.SetActive(true);
            ScriptGestionDrone.Resetter();
            ObjetDrone.transform.position = positionInitialeDrone + new Vector3(0, -20, 0);
        }

        //if(GameObject.Find("Drone") == null)
        //{
        //    ObjetDrone = Instantiate(ObjetDrone, new Vector3(0, 15, 0), Quaternion.identity);
        //    ObjetDrone.name = "Drone";
        //    ObjetDrone.GetComponent<GestionDrone>().Vie = GestionDrone.VIE_INITIALE;
        //    ObjetDrone.SetActive(true);

        //    //if (GameObject.Find(PrefabDrone.name) == null)
        //    //{
        //    //}
        //}
    }

	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube" || other.tag == ("Personnage"))
        {
            RespawnerDrone();
        }
    }
}
