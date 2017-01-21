using UnityEngine;
using System.Collections;

public class Playground : MonoBehaviour {

    OvrAvatarHand hand = null;
    public bool validGameState = false;
    Material mat;
    Color errorColor = new Color(1f, 0.3f, 0.3f, 0.1f);
    Color validColor = new Color(0.3f, 1, 0.3f, 0.1f);

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        mat.color = validGameState ? validColor : errorColor;
	}

    void OnTriggerStay(Collider other)
    {
        if (hand == null)
        {
            if (other.GetComponent<OvrAvatarHand>() != null)
            {
                hand = other.GetComponent<OvrAvatarHand>();
            }
        }
        else if (other.GetComponent<OvrAvatarHand>() != null)
        {
            validGameState = (hand == other.GetComponent<OvrAvatarHand>());
        }
            
    }
}
