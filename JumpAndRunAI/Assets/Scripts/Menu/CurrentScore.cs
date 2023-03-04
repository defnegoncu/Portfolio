using UnityEngine;
using TMPro;
public class CurrentScore : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Scoretext;
    // Update is called once per frame

    void Update()
    {
        Scoretext.text = GameManager.score.ToString();
    }
}