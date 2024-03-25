using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Score : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    public TextMeshProUGUI winnerText;

    public GameObject winScreen;

    private int playerScore = 0;
    private int aiScore = 0;

    public Transform blueBaseTransform;
    public Transform blueBaseFlagTransform;
    public Transform blueFlagTransform;
    public Transform redBaseTransform;
    public Transform redBaseFlagTransform;
    public Transform redFlagTransform;  
    public Transform playerTransform;
    public Transform aiTransform;

    void Start()
    {
        UpdateScoreText();
        winScreen.SetActive(false);
    }

    private void Update()
    {
        UpdateScoreText();
    }
    public void UpdateScoreText()
    {
        playerScoreText.text = "Player: " + playerScore.ToString();
        aiScoreText.text = "AI: " + aiScore.ToString();
    }

    public void IncreasePlayerScore()
    {
        playerScore++;
        UpdateScoreText();
        Debug.Log("Player scored: " + playerScore);

        if (playerScore >= 5)
        {
            EndGame("Player");
        }
        else
        {
            RespawnFlags();
            RespawnPlayer();
        }
    }

    public void IncreaseAIScore()
    {
        aiScore++;
        UpdateScoreText();
        Debug.Log("AI scored: " + aiScore);

        if (aiScore >= 5)
        {
            EndGame("AI");
        }
        else
        {
            RespawnFlags();
            RespawnAI();
        }
    }

    void EndGame(string winner)
    {
        winScreen.SetActive(true);
        winnerText.text = winner + " Wins!";
        Debug.Log(winner + " wins the game!");
        // Add your end game logic here
    }

    public void RespawnFlags()
    {
        // Reset flags to their respective bases
        if (blueBaseTransform != null && redBaseTransform != null)
        {
            blueFlagTransform.position = redBaseFlagTransform.position;
            redFlagTransform.position = blueBaseFlagTransform.position;
            Debug.Log("Flags respawned at their bases.");
        }
        else
        {
            Debug.LogWarning("Base transforms are not assigned.");
        }
    }

    public void RespawnPlayer()
    {
        // Respawn player at the blue base
        if (blueBaseTransform != null)
        {
            playerTransform.position = blueBaseTransform.position;
            Debug.Log("Player respawned at the blue base.");
        }
        else
        {
            Debug.LogWarning("Blue base transform is not assigned.");
        }
    }

    void RespawnAI()
    {
        // Respawn AI at the red base
        if (redBaseTransform != null)
        {
            aiTransform.position = redBaseTransform.position;
            Debug.Log("AI respawned at the red base.");
        }
        else
        {
            Debug.LogWarning("Red base transform is not assigned.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlueFlag") && blueFlagTransform.position == redBaseTransform.position)
        {
            IncreasePlayerScore();
        }
        else if (other.CompareTag("RedFlag") && redFlagTransform.position == blueBaseTransform.position)
        {
            IncreaseAIScore();
        }
    }
}
