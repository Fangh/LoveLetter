using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartBeat : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		transform.DOScale (new Vector3(20, 20, 20), 0.25f).SetLoops(-1,LoopType.Yoyo);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
