using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;
    enum Levels { lvl01, lvl02, lvl03, lvl04, lvl05, lvl06, lvl07, lvl08, lvl09, lvl10 };
    Levels currentLevel;

    public GameObject[] levelsContainer;
    public GameObject PlayerHeadset;
    public GameObject PlayerHand;
    public Vector3 PlayerHand_DistanceOffset;
    public Vector3 PlayerHand_UpOffset; // Use Z parameter

    void Awake()
    {
        Instance = this;
    }

    public void SetUpNewLevel()
    {
        switch (currentLevel)
        {
            case Levels.lvl01: // Standard position
                levelsContainer[0].SetActive(true);
                break;
            case Levels.lvl02: // Obstacle: Tree
                levelsContainer[0].SetActive(false);
                levelsContainer[1].SetActive(true);
                break;
            case Levels.lvl03: // Distance: 3m
                levelsContainer[1].SetActive(false);
                levelsContainer[2].SetActive(true);
                PlayerHand.transform.localPosition = PlayerHand_DistanceOffset;
                break;
            case Levels.lvl04: // Player 2 on a tree
                levelsContainer[2].SetActive(false);
                levelsContainer[3].SetActive(true);
                PlayerHand.transform.localPosition = PlayerHand_UpOffset;
                break;
            case Levels.lvl05: // Obstacle: Net
                levelsContainer[3].SetActive(false);
                levelsContainer[4].SetActive(true);
                PlayerHand.transform.localPosition = Vector3.zero;
                break;
            case Levels.lvl06:
                break;
            case Levels.lvl07:
                break;
            case Levels.lvl08:
                break;
            case Levels.lvl09:
                break;
            case Levels.lvl10:
                break;
            default:
                Debug.LogError("Unknown level");
                break;
        }
    }
}
