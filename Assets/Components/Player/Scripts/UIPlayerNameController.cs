using TMPro;
using UnityEngine;

namespace Components.Player.Scripts
{
    public class UIPlayerNameController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerNameText;
        [SerializeField] private TMP_Text _playerRunCountText;
        [SerializeField] private TMP_InputField _playerNameInputField;
        
        private void Start()
        {   
            UpdatePlayerRunCount();
            UpdatePlayerName();
        }

        public void SetPlayerName()
        {
            var newPlayerName = _playerNameInputField.text;
            
            if (string.IsNullOrEmpty(newPlayerName))
            {
                Debug.LogWarning("Player name cannot be empty.");
                return;
            }

            if (!SaveService.TryLoad(out SaveData save))
            {
                save = new SaveData();
            }
            
            save.PlayerName = newPlayerName;
            SaveService.Save(save);
            
            UpdatePlayerName();
        }
        
        private void UpdatePlayerName()
        {
            if (SaveService.TryLoad(out SaveData save))
            {
                if (string.IsNullOrEmpty(save.PlayerName))
                {
                    _playerNameText.text = "No player name found.";
                }
                else
                {
                    _playerNameText.text = save.PlayerName;
                }
            }
            // No save found.
            else
            {
                _playerNameText.text = "No player name found.";
            }
        }
        
        private void UpdatePlayerRunCount()
        {
            if (SaveService.TryLoad(out SaveData save))
            {
                _playerRunCountText.text = "Run count: " + save.RunCount;
            }
            // No save found.
            else
            {
                _playerRunCountText.text = "Run count: 0";
            }
        }
    }
}