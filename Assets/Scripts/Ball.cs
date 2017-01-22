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
        if (other.tag == "CatchSphere")
        {
			transform.DOScale (new Vector3 (8, 8, 8), 0.1f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    void OnTriggerExit(Collider other)
    {
		if (other.tag == "CatchSphere")
        {
			transform.DOKill ();
			transform.localScale = new Vector3 (6.5f, 6.5f, 6.5f);
        }
    }
}
