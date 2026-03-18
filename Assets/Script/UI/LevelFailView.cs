using TD.Manager;

public class LevelFailView : BaseView
{
    public void OnRetryButtonClick()
    {
        HideView();
        GameManager.Instance.ResumeGameTimeScale();
    }
}