using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionGravité : MonoBehaviour {

    [SerializeField] AudioClip SonGravité;

    // Script qui permet d'inverser la garvité lorsque le joueur saute sur un bouton
    void Start () {}	
    public void InverserGravité()
    {
        Physics.gravity *= -1; //Inverse la gravité
    }
	// Update is called once per frame
	void Update () {}
    //Lorsque le joueur rentre dans le collider, on s'assure que c'est le bel et bien le joueur qui est rentré en contact avant de multiplier la gravité par -1 et de faire jouer un son qui souligne l'inversion de la gravité
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Personnage"))
        {
            InverserGravité();
            AudioSource.PlayClipAtPoint(SonGravité, other.transform.position, 10f);
        }       
    }
}
