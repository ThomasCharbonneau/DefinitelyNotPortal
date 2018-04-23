using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBoutonDrone : MonoBehaviour
{
    [SerializeField] GameObject ObjetDrone;

    //GameObject DroneInstancié;

	// Use this for initialization
	void Start ()
    {
        //DroneInstancié = GameObject.Find("Drone");
	}
	
    public void RespawnerDrone()
    {
        //Si le drone est desactivé, en instantier un nouveau

        if(!ObjetDrone.activeSelf)
        {
            ObjetDrone.GetComponent<GestionDrone>().Vie = GestionDrone.VIE_INITIALE;
            ObjetDrone.SetActive(true);
            ObjetDrone.transform.position = new Vector3(0, 100, 0);
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
