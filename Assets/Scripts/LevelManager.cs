using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;
    [HideInInspector]
    public int currentLevel = 0;

    public GameObject[] levelsContainer;
    public Vector3[] P1_Offset;
    public Vector3[] P2_Offset;
    public GameObject P1, P2;
    public GameObject P1_hand, P2_hand;

    void Awake()
    {
        Instance = this;
    }

    public void SetUpNewLevel(int newLevel)
    {
		Debug.Log ("going now to level " + newLevel);
        if (levelsContainer[currentLevel] != null)
			levelsContainer[currentLevel].SetActive(false);
        if (levelsContainer[newLevel] != null)
			levelsContainer[newLevel].SetActive(true);
        currentLevel = newLevel;
        P1.transform.localPosition = P1_Offset[newLevel];
        P1_hand.transform.localPosition = P1_Offset[newLevel];
        P2.transform.localPosition = P2_Offset[newLevel];
        P2_hand.transform.localPosition = P2_Offset[newLevel];
    }
}
