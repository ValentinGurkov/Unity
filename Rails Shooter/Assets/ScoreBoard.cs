using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    [SerializeField] private int scorePerHit = 12;

    private int score;
    private Text scoreText;

    // Start is called before the first frame update
    private void Start() {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    public void ScoreHit() {
        score += scorePerHit;
        scoreText.text = score.ToString();
    }
}