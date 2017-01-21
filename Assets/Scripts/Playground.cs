using UnityEngine;
using System.Collections;

public class Playground : MonoBehaviour {

    OvrAvatarHand hand = null;
    public bool needHeadset = false;
    bool headsetPresent = false;
    public bool validGameState = false;
    Material mat;
    Color errorColor = new Color(1f, 0.3f, 0.3f);
    Color validColor = new Color(0.3f, 1, 0.3f);

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
		Debug.Log (name + "valid ? " + validGameState);
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
        else
        {
            if (needHeadset && other.tag == "MainCamera")
            {
				headsetPresent = true;
            }
            if (other.GetComponent<OvrAvatarHand>() != null)
            {
				validGameState = needHeadset ? (headsetPresent && checkHand(other.GetComponent<OvrAvatarHand>()) ) : checkHand(other.GetComponent<OvrAvatarHand>());
            }
        } 
    }

    bool checkHand(OvrAvatarHand unknownHand)
    {
        return (hand == unknownHand);
    }

    void OnTriggerExit(Collider other)
    {
        if (hand != null) 
		{
			hand = checkHand (other.GetComponent<OvrAvatarHand> ()) ? other.GetComponent<OvrAvatarHand> () : null;
            validGameState = checkHand(other.GetComponent<OvrAvatarHand>());
        }
    }
}