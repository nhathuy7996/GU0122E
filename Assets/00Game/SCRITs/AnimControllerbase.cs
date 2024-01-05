using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLAYER_STATE = PlayerController.PLAYER_STATE;

public abstract class AnimControllerbase : MonoBehaviour
{
    protected PLAYER_STATE _currentState;
    public abstract AnimControllerbase ChangeAnim(PLAYER_STATE state);

    public abstract AnimControllerbase ChangeBlend(string name, float val);
}
