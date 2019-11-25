using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public GameObject winner;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        int win = PlayerPrefs.GetInt("winner");
        if (win == 1)
        {
            winner.GetComponent<Text>().text = "Player wins";
        }
        else
        {
            winner.GetComponent<Text>().text = "Computer wins";
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
