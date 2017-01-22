using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Ball : MonoBehaviour {

    [HideInInspector]
    public OvrAvatarHand hand;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision other) {
		//Debug.Log ("COLLISION !!");
		if (other.gameObject.tag == "DeadZone")
			GameManager.Instance.RespawnBall();
	}
    void OnTriggerEnter(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
    }
}
