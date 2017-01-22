using UnityEngine;
using System.Collections;

public class Playground : MonoBehaviour {

    OvrAvatarHand hand = null;
    public bool isPlayerOnePlayground = false;
    bool headsetPresent = false;
    public bool validGameState = false;
    Material mat;
    Color errorColor = new Color(1f, 0.3f, 0.3f);
    Color validColor = new Color(0.3f, 1, 0.3f);

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer>().material;
		mat.color = errorColor;
		mat.SetColor ("_EmissionColor", errorColor);
    }
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (name + "valid ? " + validGameState);
		if (hand && hand.controller == OVRInput.Controller.LTouch && !isPlayerOnePlayground
			|| hand && hand.controller == OVRInput.Controller.RTouch && isPlayerOnePlayground && headsetPresent)
		{
			validGameState = true;
			mat.color = validColor;
			mat.SetColor ("_EmissionColor", validColor);
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (hand == null)
        {
			if (other.GetComponent<OvrAvatarHand>() != null 
				&& other.GetComponent<OvrAvatarHand>().controller == OVRInput.Controller.LTouch
				&& !isPlayerOnePlayground )
            {
                hand = other.GetComponent<OvrAvatarHand>();
            }
			if (other.GetComponent<OvrAvatarHand>() != null 
				&& other.GetComponent<OvrAvatarHand>().controller == OVRInput.Controller.RTouch
				&& isPlayerOnePlayground )
			{
				hand = other.GetComponent<OvrAvatarHand>();
			}
        }
        if (isPlayerOnePlayground && other.tag == "MainCamera")
        {
			headsetPresent = true;
        }
    }
}