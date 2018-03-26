using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionCamera : MonoBehaviour
{
    Vector2 Vision;
    Vector2 AdoucirCamera;
    public float Sensitivité;
    float FacteurAdoucir = 2.5f;
    GameObject personnage;
    public static bool PAUSE_CAMERA;

    public float degréRotation;

    // Use this for initialization
    void Start ()
    {
        PAUSE_CAMERA = false;
        personnage = transform.parent.gameObject;
        degréRotation = 0;
        Cursor.lockState = CursorLockMode.Locked;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp("k")) //déplacement vers l'avant
        {
            Vision.x += 0.5f;
        }
        if (!PAUSE_CAMERA)
        {
            var VariationSouris = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vision.y = Mathf.Clamp(Vision.y, -90f, 90f);

            VariationSouris = Vector2.Scale(VariationSouris, new Vector2(Sensitivité * FacteurAdoucir, Sensitivité * FacteurAdoucir));
            AdoucirCamera.x = Mathf.Lerp(AdoucirCamera.x, VariationSouris.x, 1f / FacteurAdoucir);
            AdoucirCamera.y = Mathf.Lerp(AdoucirCamera.y, VariationSouris.y, 1f / FacteurAdoucir);
            Vision += AdoucirCamera;
           

            Vision.x += degréRotation;
        

            degréRotation = 0;

            transform.localRotation = Quaternion.AngleAxis(-Vision.y, Vector3.right);
            personnage.transform.localRotation = Quaternion.AngleAxis(Vision.x, personnage.transform.up);
            ////////////////////////////////////// CETTE LIGNE ME REND FOU

        }
    }
}
