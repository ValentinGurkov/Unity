using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    private int score;
    private Text scoreText;

    // Start is called before the first frame update
    private void Start() {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    public void ScoreHit(int scoreIncrease) {
        score += scoreIncrease;
        scoreText.text = score.ToString();
    }
}