using TMPro;
using UnityEngine;

public class UILifeController : MonoBehaviour
{
    [Header("Paramaters")]
    [SerializeField] private int _startLife = 3;
    
    [Header("UI References")]
    [SerializeField] private TMP_Text _lifeText;

    private int _life;
    
    private void Start()
    {
        SetLife(_startLife);
        GameEventService.OnCollision += HandleCollision;
    }
    
    private void OnDestroy()
    {
        GameEventService.OnCollision -= HandleCollision;
    }

    private void HandleCollision()
    {
        var newLife = _life - 1;

        if (newLife <= 0)
        {
            GameEventService.OnGameOver?.Invoke();
            SetLife(0);
            
            return;
        }
        
        SetLife(newLife);
    }

    private void SetLife(int life)
    {
        _life = life;
        _lifeText.text = "Life:" + life;
    }
}