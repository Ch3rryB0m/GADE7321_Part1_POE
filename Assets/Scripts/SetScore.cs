using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public int playerScore = 0;

    public TextMeshProUGUI aiScoreText;
    public int aiScore = 0;

    public Transform blueBaseTransform;
    public Transform blueFlagTransform;
    public Transform redBaseTransform;
    public Transform redFlagTransform;


    void Start()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        playerScoreText.text = "Player: " + playerScore.ToString();
        aiScoreText.text = "AI: " + aiScore.ToString();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered: " + other.tag);

        if (other.CompareTag("BlueFlag") && blueFlagTransform.position == blueBaseTransform.position)
        {
            playerScore++;
            UpdateScoreText();
            Debug.Log("Player scored: " + playerScore);
        }
        else if (other.CompareTag("RedFlag") && redFlagTransform.position == redBaseTransform.position)
        {
            aiScore++;
            UpdateScoreText();
            Debug.Log("AI scored: " + aiScore);
        }
    }
}
