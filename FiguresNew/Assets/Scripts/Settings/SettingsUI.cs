using UnityEngine;

namespace Settings
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private FlexibleColorPicker _endValueColorPicker;
        [SerializeField] private FlexibleColorPicker _startValueColorPicker;
        [SerializeField] private Player.PlayerController _playerController;
        private LineRenderer _playerLineRedenerer;
        
        private void Start()
        {
            _playerLineRedenerer = _playerController.GetComponent<LineRenderer>();
            _startValueColorPicker.onColorChange.AddListener(OnStartColorChanged);
            _endValueColorPicker.onColorChange.AddListener(OnEndColorChanged);
        }
        private void OnStartColorChanged(Color newColor)
        {
            _playerLineRedenerer.startColor = newColor;
        }
        private void OnEndColorChanged(Color newColor)
        {
            _playerLineRedenerer.endColor = newColor;
        }
    }
}
