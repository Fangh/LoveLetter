using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance;
	public GameObject P1Playground;
	public GameObject P2Playground;
	public bool forceStartGame = false;
	bool GameIsStarted = false;
	GameObject ball;

	// Use this for initialization
	void Start () 
	{
		ball = GameObject.FindWithTag ("Ball");	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!GameIsStarted) 
		{
			if (P1Playground.GetComponent<Playground> ().validGameState && P2Playground.GetComponent<Playground> ().validGameState) 
			{
				StartGame ();
			}
			if (forceStartGame) 
			{
				forceStartGame = false;
				StartGame ();				
			}
		}
	
	}

	void StartGame()
	{
		GameIsStarted = true;
		RespawnBall ();		
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
