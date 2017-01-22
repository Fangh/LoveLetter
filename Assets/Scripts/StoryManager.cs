using UnityEngine;
using System.Collections;

public class StoryManager : MonoBehaviour {

    public static StoryManager Instance;
    public GameObject[] letters;
    int nextLetter = 0;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}

    public void advanceStory()
    {
        letters[nextLetter].SetActive(true);
        nextLetter++;
    }
}
