using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public GameObject winner;
    private Text winnerText;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        int win = PlayerPrefs.GetInt("winner");
        winnerText = winner.GetComponent<Text>();
        if (win == 1)
        {
            winnerText.text = "Player wins";
        }
        else
        {
            winnerText.text = "Computer wins";
        }
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
