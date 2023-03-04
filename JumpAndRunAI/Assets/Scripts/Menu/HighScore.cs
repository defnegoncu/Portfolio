using UnityEngine;
using TMPro;
public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI HighScoretext;
    // Update is called once per frame

    void Update()
    {
        HighScoretext.text = "Highscore: " + PlayerPrefs.GetInt("highscore") + "\n" + "Score:" + GameManager.score;
    }
}