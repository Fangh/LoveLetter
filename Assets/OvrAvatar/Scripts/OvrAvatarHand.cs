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
	private byte[] hapticForceMax;
	private byte[] hapticForceLow;
    const int stdVibrationDuration = 180; // En Hertz
    bool proximityFeedback = true;
	public Transform grabPoint;

	void Start()
	{
		hapticForceMax = createByteTab (255f);
		hapticForceLow = createByteTab (2f);
	}

	public void Update()
	{		
		if (OVRInput.GetDown (OVRInput.Button.PrimaryThumbstick, controller))
		{
			//DEBUG MODE !
			GameManager.Instance.RespawnBall ();
		}
		if (currentGrabbedObject == null) 
		{
			if (currentTouchedObject == null) 
			{
				return;
			}
			if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
            {
				currentGrabbedObject = currentTouchedObject;
				currentGrabbedObject.transform.parent = grabPoint;
				currentGrabbedObject.transform.localPosition = Vector3.zero;
				currentGrabbedObject.GetComponent<Rigidbody> ().useGravity = false;
				currentGrabbedObject.GetComponent<Rigidbody> ().isKinematic = true;
				currentGrabbedObject.GetComponent<Ball> ().hand = this;

				int channel = controller == OVRInput.Controller.LTouch ? 0 : 1;
				OVRHaptics.Channels [channel].Mix(new OVRHapticsClip(hapticForceMax, stdVibrationDuration));

				//Debug.Log (currentGrabbedObject.GetComponent<Ball> ().hand.name + " viens d'attraper une balle");
				//Debug.Log ("cette balle était envoyé par " + GameManager.Instance.ballThrownBy);

				if (currentGrabbedObject.GetComponent<Ball>().hand == this 
					&& GameManager.Instance.ballThrownBy != null 
					&& GameManager.Instance.ballThrownBy != gameObject)
				{
					//Debug.Log (name + "a bien attraper la balle envoyé par " + GameManager.Instance.ballThrownBy.name);
					GameManager.Instance.AddScore ();
				}

				//Debug.Log ("ATTRAPER");
			}
		} 
		else
        {
			if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
			{
				currentGrabbedObject.transform.parent = null;
				currentGrabbedObject.GetComponent<Rigidbody> ().useGravity = true;
				currentGrabbedObject.GetComponent<Rigidbody> ().isKinematic = false;
				currentGrabbedObject.GetComponent<Ball> ().hand = null;

				currentGrabbedObject.GetComponent<Rigidbody> ().velocity = OVRInput.GetLocalControllerVelocity (controller);
				currentGrabbedObject.GetComponent<Rigidbody> ().angularVelocity = OVRInput.GetLocalControllerAngularVelocity (controller).eulerAngles;

				currentGrabbedObject = null;
				GameManager.Instance.ballThrownBy = gameObject;
				//Debug.Log ("balle envoyé par " + GameManager.Instance.ballThrownBy);

                proximityFeedback = false;
                Invoke("restoreFeedback", 0.5f);
				//Debug.Log ("LACHER");				
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ball") 
		{
			//currentTouchedObject = other.gameObject;
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (!currentTouchedObject && other.tag == "Ball")
		{
			Debug.Log ("TOUCH BALL");
			currentTouchedObject = other.gameObject;
			currentTouchedObject.transform.DOScale (new Vector3 (8, 8, 8), 0.1f).SetLoops(-1, LoopType.Yoyo);			
		}
		
		if (other.tag == "Ball" && currentGrabbedObject == null) 
		{
			int channel = controller == OVRInput.Controller.LTouch ? 0 : 1;
			OVRHaptics.Channels [channel].Mix(new OVRHapticsClip(hapticForceLow, 10));			
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.tag == "Ball") 
		{
			//Debug.Log ("UNTOUCH BALL");
			currentTouchedObject.transform.DOKill ();
			currentTouchedObject.transform.localScale = new Vector3 (6.5f, 6.5f, 6.5f);
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