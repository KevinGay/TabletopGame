using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

    //totalUnits: total number of units on the game board
    public int totalUnits = 3;
    private int unitsFound = 0;

    private bool gameOver;
    TextMesh scoreText;

    public List<GameObject> critterUI;
	public AudioClip[] soundClips;

    // Use this for initialization
    void Start () {
        scoreText = GameObject.Find("scoreDisplay").GetComponent<TextMesh>();
        setScoreText();

        critterUI.Add(GameObject.Find("scoreCritter"));
        critterUI.Add(GameObject.Find("scoreCritter (1)"));
        critterUI.Add(GameObject.Find("scoreCritter (2)"));
        critterUI.Add(GameObject.Find("scoreCritter (3)"));
        critterUI.Add(GameObject.Find("scoreCritter (4)"));
        critterUI.Add(GameObject.Find("scoreCritter (5)"));

    }

    //Call this whenever a unit is found
    //To call this from another Script:
    //      GameObject.Find("ReVIVE Scoreboard").GetComponent<GameHandler>().unitFound();
    public void unitFound()
    {
		GameObject.Find("ReVIVE Scoreboard").GetComponent<AudioSource>().clip = soundClips[unitsFound];
		GameObject.Find("ReVIVE Scoreboard").GetComponent<AudioSource>().Play();
        critterUI[unitsFound].GetComponent<MeshRenderer>().enabled = true;
        unitsFound++;
        setScoreText();
    }

    // Returns true if all critters have been found
    public bool getGameOver()
    {
        return gameOver;
    }
    //Sets the score text on the scoreboard
	void setScoreText()
    {
        scoreText.text = "Critters Found: " + unitsFound.ToString() + " / " + totalUnits.ToString();
    }

   
}
