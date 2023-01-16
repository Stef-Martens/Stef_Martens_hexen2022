using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public GameStateManager GameStateManager { get; set; }
    public abstract void EnterState(GameStateManager gameStateManager);
}
