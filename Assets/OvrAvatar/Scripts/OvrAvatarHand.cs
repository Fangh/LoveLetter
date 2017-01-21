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
	private byte[] hapticForceMax;
	private byte[] hapticForceLow;
    const int stdVibrationDuration = 180; // En Hertz
    bool proximityFeedback = true;

	void Start()
	{
		hapticForceMax = createByteTab (255f);
		hapticForceLow = createByteTab (2f);
	}

	public void Update()
	{		
		if (OVRInput.GetDown (OVRInput.Button.Two, controller))
		{
			GameManager.Instance.RespawnBall ();
		}
		if (currentGrabbedObject == null) 
		{
			if (currentTouchedObject == null) 
			{
				return;
			}
            if (OVRInput.GetDown(OVRInput.Button.One, controller)
                || OVRInput.GetDown(OVRInput.Button.Two, controller)
                || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller)
                || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
            {
				currentGrabbedObject = currentTouchedObject;
				currentGrabbedObject.transform.parent = transform;
				currentGrabbedObject.transform.localPosition = Vector3.zero;
				currentGrabbedObject.GetComponent<Rigidbody> ().useGravity = false;
				currentGrabbedObject.GetComponent<Rigidbody> ().isKinematic = true;
				int channel = controller == OVRInput.Controller.LTouch ? 0 : 1;
				OVRHaptics.Channels [channel].Mix(new OVRHapticsClip(hapticForceMax, stdVibrationDuration));

				Debug.Log ("ATTRAPER");
			}
		} 
		else
        {
            if (!(OVRInput.GetDown(OVRInput.Button.One, controller)
                || OVRInput.GetDown(OVRInput.Button.Two, controller)
                || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller)
                || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller)))
			{
				currentGrabbedObject.transform.parent = null;
				currentGrabbedObject.GetComponent<Rigidbody> ().useGravity = true;
				currentGrabbedObject.GetComponent<Rigidbody> ().isKinematic = false;
				currentGrabbedObject.GetComponent<Rigidbody> ().velocity = OVRInput.GetLocalControllerVelocity (controller);
				currentGrabbedObject.GetComponent<Rigidbody> ().angularVelocity = OVRInput.GetLocalControllerAngularVelocity (controller).eulerAngles;
				currentGrabbedObject = null;

                proximityFeedback = false;
                Invoke("restoreFeedback", 0.5f);
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

	public void OnTriggerStay(Collider other)
	{
		if (proximityFeedback && other.tag == "Ball" && currentGrabbedObject == null) 
		{
			int channel = controller == OVRInput.Controller.LTouch ? 0 : 1;
			OVRHaptics.Channels [channel].Mix(new OVRHapticsClip(hapticForceLow, 10));			
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

	public byte[] createByteTab(float force)
	{
        byte[] tab = new byte[stdVibrationDuration];
		for (int i = 0; i < tab.Length; i++)
		{
			tab[i] = (byte)((Mathf.Sin(i)*force));
		}
		return tab;
	}

    float alpha = 1.0f;

    public void UpdatePose(OvrAvatarDriver.ControllerPose pose)
    {
		
    }

    void restoreFeedback()
    {
        proximityFeedback = true;
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