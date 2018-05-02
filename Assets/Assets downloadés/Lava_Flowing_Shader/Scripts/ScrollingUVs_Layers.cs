using UnityEngine;
using System.Collections;

public class ScrollingUVs_Layers : MonoBehaviour 
{
	//public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
	public string textureName = "_MainTex";
	
	Vector2 uvOffset = Vector2.zero;
	
	void LateUpdate() 
	{
		uvOffset += ( uvAnimationRate * Time.deltaTime );
<<<<<<< HEAD
		//if( GetComponent<Renderer>().enabled )
		//{
		//	GetComponent<Renderer>().sharedMaterial.SetTextureOffset( textureName, uvOffset );
		//}
=======
		if( GetComponent<Renderer>().enabled )
		{
			//GetComponent<Renderer>().sharedMaterial.SetTextureOffset( textureName, uvOffset );
		}
>>>>>>> 76348c29374cf1dd3e6aaf543ec7b8a1aa76f3d7
	}
}