using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{

    public Slider lenght;
    

    private void Start()
    {
        
    }
    public void PlayGame(){
        GameManager.menulenght = (int)lenght.value;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Quit!!");
        Application.Quit();
    }
    
        //enght.onValueChanged.AddListener(delegate {ValueChangedCheck()};);


}
