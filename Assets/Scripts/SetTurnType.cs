using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    public ActionBasedContinuousTurnProvider continousTurn;
    public ActionBasedSnapTurnProvider snapTurn;

    public void SetTypeFromIndex(int index)
    {
        if (index == 0)
        {
            snapTurn.enabled = false;
            continousTurn.enabled = true;
        }
        else if (index == 1)
        {
            snapTurn.enabled = true;
            continousTurn.enabled = false;
        }

    }

    // This method updates the turn speed (for continuous) and turn amount (for snap turn)
    // based on a slider value assumed to be in the range [0, 1].
    public void SetTurnValue(float sliderValue)
    {
        // Calculate the new continuous turn speed from 30 to 120.
        float newTurnSpeed = Mathf.Lerp(30f, 120f, sliderValue);
        // Calculate the new snap turn angle from 15 to 90.
        float newTurnAmount = Mathf.Lerp(15f, 90f, sliderValue);

        // Update the respective properties.
        continousTurn.turnSpeed = newTurnSpeed;
        snapTurn.turnAmount = newTurnAmount;
    }
}
