using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance;
	GameObject ball;

	// Use this for initialization
	void Start () 
	{
		ball = GameObject.FindWithTag ("Ball");
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void RespawnBall()
	{
		Debug.Log ("SUMMON BALL");
		//ball.transform.DOMove (transform.position, 1f).SetEase (Ease.InCubic);
		ball.transform.position = transform.position;
		ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		
	}
}
