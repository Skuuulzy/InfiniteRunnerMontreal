using System;

public static class GameEventService
{
    public static Action OnCollision;
    public static Action<int> OnPlayerLifeUpdated;
    public static Action<float> OnCountdownTick;

    public static Action<bool> OnCountdownState;
    public static Action<bool> OnGameState;
    public static Action<bool> OnGameOverState;
    
}