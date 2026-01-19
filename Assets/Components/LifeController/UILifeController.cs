using TMPro;
using UnityEngine;

public class UILifeController : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private TMP_Text _lifeText;
    
    
    private void Start()
    {
        GameEventService.OnPlayerLifeUpdated += SetLife;
    }
    
    private void OnDestroy()
    {
        GameEventService.OnPlayerLifeUpdated -= SetLife;
    }

    private void SetLife(int life)
    {
        _lifeText.text = "Life:" + life;
    }
}