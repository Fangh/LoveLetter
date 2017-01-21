using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Oculus.Avatar;
using DG.Tweening;

public class OvrAvatarHand : MonoBehaviour, IAvatarPart
{
	private GameObject currentTouchedObject = null;
	private GameObject currentGrabbedObject = null;
	public OVRInput.Controller controller;
	public AudioClip vibratesound;
	private byte[] vibrateTab; 

	void Start()
	{
		vibrateTab = createByteTab ();
	}

	public void Update()
	{
		Debug.Log ("SUMMON BALL");
		if (OVRInput.GetDown (OVRInput.Button.Two, controller))
		{
			GameObject ball = GameObject.FindGameObjectWithTag ("Ball");
			//ball.transform.DOMove (transform.position, 1f).SetEase (Ease.InCubic);
			ball.transform.position = transform.position;
			ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			ball.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		}
		if (currentGrabbedObject == null) 
		{
			if (currentTouchedObject == null) 
			{
				return;
			}
			if (OVRInput.GetDown (OVRInput.Button.One, controller)
			    && OVRInput.Get (OVRInput.Button.PrimaryIndexTrigger, controller)
			    || (OVRInput.Get (OVRInput.Button.One, controller)
			    && OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger, controller))) {
				currentGrabbedObject = currentTouchedObject;
				currentGrabbedObject.transform.parent = transform;
				currentGrabbedObject.transform.localPosition = Vector3.zero;
				currentGrabbedObject.GetComponent<Rigidbody> ().useGravity = false;
				currentGrabbedObject.GetComponent<Rigidbody> ().isKinematic = true;
				int channel = controller == OVRInput.Controller.LTouch ? 0 : 1;
				OVRHaptics.Channels [channel].Mix(new OVRHapticsClip(vibrateTab, 320));

				Debug.Log ("ATTRAPER");
			}
		} 
		else 
		{
			if (!(OVRInput.Get (OVRInput.Button.One, controller)
			    && OVRInput.Get (OVRInput.Button.PrimaryIndexTrigger, controller))) 
			{
				currentGrabbedObject.transform.parent = null;
				currentGrabbedObject.GetComponent<Rigidbody> ().useGravity = true;
				currentGrabbedObject.GetComponent<Rigidbody> ().isKinematic = false;
				currentGrabbedObject.GetComponent<Rigidbody> ().velocity = OVRInput.GetLocalControllerVelocity (controller);
				currentGrabbedObject.GetComponent<Rigidbody> ().angularVelocity = OVRInput.GetLocalControllerAngularVelocity (controller).eulerAngles;
				currentGrabbedObject = null;

				Debug.Log ("LACHER");
				
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ball") 
		{
			Debug.Log ("TOUCH BALL");
			currentTouchedObject = other.gameObject;
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.tag == "Ball") 
		{
			Debug.Log ("UNTOUCH BALL");
			currentTouchedObject = null;
		}
	}

	public byte[] createByteTab()
	{
		byte[] tab = new byte[320];
		for (int i = 0; i < tab.Length; i++)
		{
			tab[i] = (byte)((i%4)*255);
		}
		return tab;
	}

    float alpha = 1.0f;

    public void UpdatePose(OvrAvatarDriver.ControllerPose pose)
    {
    }

    public void SetAlpha(float alpha)
    {
        this.alpha = alpha;
    }

    public void OnAssetsLoaded()
    {
        SetAlpha(this.alpha);
    }
}