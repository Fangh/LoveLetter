using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour 
{
	public float time = 3f;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, time);
	
	}
}
