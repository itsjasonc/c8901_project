using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth()
    {
        if (player.GetComponent<PlayerScript>().health == 0)
        {
            enemy.GetComponent<PlayerScript>().num_wins += 1;

            if (enemy.GetComponent<PlayerScript>().num_wins >= 2)
            {
                PlayerPrefs.SetInt("winner", 1);
                SceneManager.LoadScene("EndGame");
            }
            else
            {
                ResetGame();
            }
        }
        if (enemy.GetComponent<PlayerScript>().health == 0)
        {
            player.GetComponent<PlayerScript>().num_wins += 1;

            if (player.GetComponent<PlayerScript>().num_wins >= 2)
            {
                PlayerPrefs.SetInt("winner", 0);
                SceneManager.LoadScene("EndGame");
            }
            else
            {
                ResetGame();
            }
        }
    }

    void ResetGame()
    {
        player.GetComponent<PlayerScript>().health = 10;
        enemy.GetComponent<PlayerScript>().health = 10;
    }
}
