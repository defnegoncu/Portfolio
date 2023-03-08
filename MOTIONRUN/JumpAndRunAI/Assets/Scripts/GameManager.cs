using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public static int score;
    public static int highScore;
   public static int menulenght;
    public float DelayTime = 1.0f;

    private void Start(){
        score = 0;
        FindObjectOfType<AudioManager>().Play("rainbowrun");
    }

    public void EndGame() {
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play("hit");
        Debug.Log("verloren");
        SetHighscore();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void SetHighscore(){
        highScore = PlayerPrefs.GetInt("highscore");
        if (score > highScore){
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }

    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseScore(){
        FindObjectOfType<AudioManager>().Play("coin");
        score++;
    }
}
