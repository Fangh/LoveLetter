using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;
    [HideInInspector]
    public int currentLevel = 0;

    public GameObject[] levelsContainer;
    public Vector3[] P1_Offset;
    public Vector3[] P2_Offset;
    public int[] scoreTresholds;
    public GameObject P1, P2;
    public GameObject P1_heaset, P1_hand, P2_hand;
	public GameObject sceneryIsland;
	public GameObject sceneryMoon;

    void Awake()
	{
        Instance = this;
    }

    public void SetUpNewLevel(int newLevel)
    {
		Debug.Log ("going now to level " + newLevel+1);
        if (levelsContainer[currentLevel] != null)
			levelsContainer[currentLevel].SetActive(false);
        if (levelsContainer[newLevel] != null)
			levelsContainer[newLevel].SetActive(true);
        currentLevel = newLevel;
        GameManager.Instance.scoreThreshold = scoreTresholds[newLevel];
        P1.transform.localPosition = P1_Offset[newLevel];
        P1_heaset.transform.localPosition = P1_Offset[newLevel];
        P1_hand.transform.localPosition = P1_Offset[newLevel];
        P2.transform.localPosition = P2_Offset[newLevel];
        P2_hand.transform.localPosition = P2_Offset[newLevel];
        if (newLevel == 5) SetupNewScenery(sceneryIsland, sceneryMoon, -1.16f);
    }

	public void SetupNewScenery(GameObject oldScenery, GameObject newScenery, float newGravity = -9.81f)
	{
		DOTween.To (() => GameObject.Find ("CenterEyeAnchor").GetComponent<VignetteAndChromaticAberration> ().intensity,
			x => GameObject.Find ("CenterEyeAnchor").GetComponent<VignetteAndChromaticAberration> ().intensity = x,
			1, 1).SetLoops(2, LoopType.Yoyo);
		StartCoroutine (ModifyScene(1f, oldScenery, newScenery, newGravity));
	}

	private IEnumerator ModifyScene(float waitTime, GameObject oldScenery, GameObject newScenery, float newGravity)
	{
		yield return new WaitForSeconds (waitTime);
		oldScenery.SetActive (false);
		newScenery.SetActive (true);
		Physics.gravity = new Vector3 (0, newGravity, 0);
	}
}
