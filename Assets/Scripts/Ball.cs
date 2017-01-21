using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    [HideInInspector]
    public OvrAvatarHand hand;
    Material mat;
    Color stdColor = Color.red;
    Color heldColor = new Color (0.25f, 0.9f, 0.25f);

	// Use this for initialization
	void Start () {
        if (GetComponent<Collider>() == null) this.enabled = false;
        mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision other) {
		Debug.Log ("COLLISION !!");
		if (other.gameObject.tag == "DeadZone")
			GameManager.Instance.RespawnBall();
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OvrAvatarHand>() != null)
        {
            hand = other.GetComponent<OvrAvatarHand>();
            mat.color = heldColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<OvrAvatarHand>() == hand)
        {
            mat.color = stdColor;
            GameManager.Instance.ballThrown = true;
        }
    }
}
