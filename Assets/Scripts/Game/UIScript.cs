using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    private PlayerScript playerScript;
    private PlayerScript enemyScript;
    public GameObject playerHealth;
    public GameObject playerWins;
    public GameObject enemyHealth;
    public GameObject enemyWins;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        enemyScript = enemy.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.GetComponent<Text>().text = "Player Health: " + playerScript.health.ToString();
        playerWins.GetComponent<Text>().text = "Player Wins: " + playerScript.num_wins.ToString();
        enemyHealth.GetComponent<Text>().text = "Enemy Health: " + enemyScript.health.ToString();
        enemyWins.GetComponent<Text>().text = "Enemy Wins: " + enemyScript.num_wins.ToString();
    }
}
