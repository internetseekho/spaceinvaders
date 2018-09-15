using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;

    private Text myText;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        //scoreReset();
    }

    public void Score( int Points ) {

        score += Points;
        myText.text = score.ToString();

    }

    public static void scoreReset()
    {
        score = 0;
    }

}
