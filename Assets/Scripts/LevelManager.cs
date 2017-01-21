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
    public Vector3 PlayerHand_DistanceOffset;
    public Vector3 PlayerHand_UpOffset; // Use Z parameter

    void Awake()
    {
        Instance = this;
    }

    public void SetUpNewLevel(int newLevel)
    {
        levelsContainer[currentLevel].SetActive(false);
        levelsContainer[newLevel].SetActive(true);
        currentLevel = newLevel;
        P1.transform.localPosition = P1_Offset[newLevel];
        P2.transform.localPosition = P2_Offset[newLevel];
    }
}
