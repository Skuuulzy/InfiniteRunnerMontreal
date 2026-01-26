using Components.Data;
using Components.SODB;
using TMPro;
using UnityEngine;

public class UILifeController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text _lifeText;
    
    
    private void Start()
    {
        int levelIndex = 1;
        if (SaveService.TryLoad(out SaveData saveData))
        {
            levelIndex = saveData.LevelIndex;
        }
            
        var parameters = ScriptableObjectDataBase.Get<SOLevelParameters>("Level" + levelIndex);
        
        SetLife(parameters.PlayerLife);
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