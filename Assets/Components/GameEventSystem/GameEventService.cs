using System;
using UnityEngine;

public static class GameEventService
{
    #region GAMEPLAY
    
    public static Action OnCollision;
    public static Action OnCollectiblePicked;
    public static Action<int> OnPlayerLifeUpdated;
    public static Action<float> OnCountdownTick;
    
    #endregion

    public static Action<bool> OnCountdownState;
    public static Action<bool> OnGameState;
    public static Action<bool> OnGameOverState;

    public static Action<Material> OnChunkColorUpdated;
}