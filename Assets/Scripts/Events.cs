using System;
using UnityEngine;
using UnityEngine.Events;

public class Events 
{
    [Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }

   // [Serializable] public class EventGameMenuState : UnityEvent<ProjectConstants.UI.Menu, GameObject> { }
}
