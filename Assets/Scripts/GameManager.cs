using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance;
	public GameObject P1Playground;
	public GameObject P2Playground;
	public bool GameIsStarted = false;
	GameObject ball;
	public GameObject explosionFX;
    public int scoreThreshold = 3;
	public GameObject ballThrownBy = null;
    public int score = 0;
	public GameObject instructions;
	public ParticleSystem fireworks;
	public List<AudioClip> SFX_fail = new List<AudioClip>();
	public List<AudioClip> SFX_win = new List<AudioClip>();
	public AudioClip SFX_nextLevel;

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
		if (!GameIsStarted) 
		{
			if (P1Playground.GetComponent<Playground> ().validGameState && P2Playground.GetComponent<Playground> ().validGameState) 
			{
				StartGame ();
			}
		} 
		else 
		{
            if (score >= scoreThreshold)
            {
				GetComponent<AudioSource> ().PlayOneShot (SFX_nextLevel);
                fireworks.Play();
                score = 0;
                LevelManager.Instance.SetUpNewLevel(LevelManager.Instance.currentLevel+1);
				Debug.Log("Game complete");
            }
		}
	}

	void StartGame()
	{
		GameIsStarted = true;
		instructions.SetActive(false);
		RespawnBall ();
	}

	public void RespawnBall()
	{
		//Debug.Log ("SUMMON BALL");
		//ball.transform.DOMove (transform.position, 0.5f).SetEase (Ease.OutQuint);
        GameManager.Instance.score = 0;
		GameObject.Instantiate (explosionFX, ball.transform.position, Quaternion.identity);
		ball.transform.position = transform.position;
		GameObject.Instantiate (explosionFX, ball.transform.position, Quaternion.identity);
		ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		GetComponent<AudioSource> ().PlayOneShot (SFX_fail[ Random.Range(0, SFX_fail.Count) ]);
	}

	public void AddScore()
	{
		score++;
		GetComponent<AudioSource> ().PlayOneShot (SFX_win[ Random.Range(0, SFX_win.Count) ]);
	}

	public void OnAvatarLoad(GameObject leftHand)
	{
		if( leftHand.GetComponentInChildren<SkinnedMeshRenderer> () )
			leftHand.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;		
	}
}
