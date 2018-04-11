using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPortal : MonoBehaviour {

    //https://www.youtube.com/watch?v=cuQao3hEKfs&t=467s
    public Transform PlayerCamera;
    public Transform Portal;
    public Transform AutrePortal;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        //Vector3 EspaceEntreJoueur_Portal = PlayerCamera.position - AutrePortal.position;
        //transform.position = Portal.position + EspaceEntreJoueur_Portal;

        //float DifferencDeAngleRotation = Quaternion.Angle(Portal.rotation, AutrePortal.rotation);

        //Quaternion DifferenceDeRotation = Quaternion.AngleAxis(DifferencDeAngleRotation, Vector3.up);
        //Vector3 NouvelleDirectionDeCamera = DifferencDeAngleRotation * PlayerCamera.forward;
        //transform.rotation = Quaternion.LookRotation(NouvelleDirectionDeCamera, Vector3.up);
    }
}
