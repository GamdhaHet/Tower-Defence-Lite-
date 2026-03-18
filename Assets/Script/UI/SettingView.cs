using TD.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class SettingView : BaseView
    {
        [SerializeField] private Button _musicButton;
        [SerializeField] private Button _sfxButton;
        [SerializeField] private Color _enabledColor = new();
        [SerializeField] private Color _disabledColor = new();

        public override void ShowView()
        {
            SetEnableEffects(_musicButton, AudioManager.Instance.IsMusicEnabled);
            SetEnableEffects(_sfxButton, AudioManager.Instance.IsSfxEnabled);
            base.ShowView();
        }

        public void OnMusicButtonClick()
        {
            AudioManager.Instance.ToggleMusic();
            SetEnableEffects(_musicButton, AudioManager.Instance.IsMusicEnabled);
        }

        public void OnSFXButtonClick()
        {
            AudioManager.Instance.ToggleSfx();
            SetEnableEffects(_sfxButton, AudioManager.Instance.IsSfxEnabled);
        }

        private void SetEnableEffects(Button button, bool isEnabled)
        {
            button.image.color = isEnabled ? _enabledColor : _disabledColor;
        }
    }
}
