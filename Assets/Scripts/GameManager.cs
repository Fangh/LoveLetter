﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance;
	public GameObject P1Playground;
	public GameObject P2Playground;
	public bool forceStartGame = false;
	bool GameIsStarted = false;
	GameObject ball;

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		ball = GameObject.FindWithTag ("Ball");	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!GameIsStarted) {
			if (P1Playground.GetComponent<Playground> ().validGameState && P2Playground.GetComponent<Playground> ().validGameState) 
			{
				StartGame ();
			}
			if (forceStartGame) {
				forceStartGame = false;
				StartGame ();				
			}
		} 
		else 
		{
			if (!(P1Playground.GetComponent<Playground> ().validGameState && P2Playground.GetComponent<Playground> ().validGameState)) 
			{
				GameIsStarted = false;
				ball.transform.position = new Vector3 (100, 100, 100);
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
		ball.transform.DOMove (transform.position, 0.5f).SetEase (Ease.OutQuint);
		//ball.transform.position = transform.position;
		ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}
}
