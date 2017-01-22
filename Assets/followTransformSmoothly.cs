using UnityEngine;
using System.Collections;

public class followTransformSmoothly : MonoBehaviour
{
	public GameObject transformToFollow;
	public Vector3 offset = Vector3.zero;
	public float smooth = 0.5f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (null == transformToFollow)
			transformToFollow = GameObject.Find ("hand_left");
		Vector3 currentVelocity = Vector3.zero;
		Vector3 interPos = Vector3.SmoothDamp (transform.position, transformToFollow.transform.position + offset, ref currentVelocity, smooth);
		Vector3 finalPos = new Vector3 (interPos.x, transform.position.y, interPos.z);
		transform.position = finalPos;
	
	}
}
