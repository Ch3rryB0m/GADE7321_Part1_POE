using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBase : MonoBehaviour
{
    public int blueTeamPoints = 0; // Points for the blue team

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("flag"))
        {
            Flag flag = other.GetComponent<Flag>();
            if (flag != null && flag.holdingFlag && flag.team == Team.Red)
            {
                Debug.Log("Blue base captured Red team's flag!");
                // Award points to the blue team
                blueTeamPoints++;
                Debug.Log("Blue Team Points: " + blueTeamPoints);
                // Additional logic for capturing the flag, such as resetting the flag's position
                flag.ResetFlag(false); // Reset the flag to the opposite base
            }
        }
    }
}
