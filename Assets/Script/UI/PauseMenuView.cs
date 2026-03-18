using TD.EnemyHandler;
using TD.Manager;

namespace TD.UI
{
    public class PauseMenuView : BaseView
    {
        public override void ShowView()
        {
            GameManager.Instance.PauseGameTimeScale();
            base.ShowView();
        }

        public override void HideView()
        {
            GameManager.Instance.ResumeGameTimeScale();
            base.HideView();
        }

        public void OnResumeButtonClick()
        {
            HideView();
            GameManager.Instance.ResumeGameTimeScale();
        }

        public void OnRestartButtonClick()
        {
            HideView();
            GameManager.Instance.ResumeGameTimeScale();
            GameManager.Instance.OnGameLevelExit();
            MainUIManager.Instance.GetView<TransitionView>().SetView(2f, () =>
            {
                EnemyWaveManager.Instance.SetupEnemyWaveManager();
            });
        }

        public void OnExitButtonClick()
        {
            HideView();
            GameManager.Instance.OnGameLevelExit();
            MainUIManager.Instance.UnloadGamePlayView();
            MainUIManager.Instance.GetView<TransitionView>().SetView(2f, () =>
            {
                MainUIManager.Instance.ShowView<HomeViewView>();
            });
        }
    }
}