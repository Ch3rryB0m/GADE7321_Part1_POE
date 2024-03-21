using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBase : MonoBehaviour
{
    public int redTeamPoints = 0; // Points for the red team

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("flag"))
        {
            Flag flag = other.GetComponent<Flag>();
            if (flag != null && flag.holdingFlag && flag.team == Team.Blue)
            {
                Debug.Log("Red base captured Blue team's flag!");
                // Award points to the red team
                redTeamPoints++;
                Debug.Log("Red Team Points: " + redTeamPoints);
                // Additional logic for capturing the flag, such as resetting the flag's position
                flag.ResetFlag(true); // Reset the flag to the opposite base
            }
        }
    }
}
