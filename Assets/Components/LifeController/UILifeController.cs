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
        GameEventSystem.OnCollision += HandleCollision;
    }
    
    private void OnDestroy()
    {
        GameEventSystem.OnCollision -= HandleCollision;
    }

    private void HandleCollision()
    {
        var newLife = _life - 1;

        if (newLife <= 0)
        {
            GameEventSystem.OnGameOver?.Invoke();
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