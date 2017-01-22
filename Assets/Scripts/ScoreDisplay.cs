using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

    public Text scoreDisplay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!GameManager.Instance.GameIsStarted)
		{
			scoreDisplay.text = "NO SCORE";
			return;
		}
        if (scoreDisplay != null)
        {
            scoreDisplay.text = GameManager.Instance.score + " / " + GameManager.Instance.scoreThreshold;
        }
	}
}
