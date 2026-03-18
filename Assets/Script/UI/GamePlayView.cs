using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class GamePlayView : BaseView
    {
        [SerializeField] private Text _currentLevelText;
        [SerializeField] private Text _nextWaveText;

        public void SetCurrentLevelText(string text)
        {
            _currentLevelText.gameObject.SetActive(true);
            _currentLevelText.text = "Level : " + text;
        }

        public void SetNextWaveText(string text)
        {
            _nextWaveText.gameObject.SetActive(true);
            _nextWaveText.text = "Next Wave Comes in " + text;
        }

        public void HideNextWaveText()
        {
            _nextWaveText.gameObject.SetActive(false);
        }
    }
}