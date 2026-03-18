using System.Collections.Generic;
using TD.Manager;
using TD.RewardSystem;
using TD.UI;
using UnityEngine;
using TD.Audio;

public class LevelCompleteView : BaseView
{
    [SerializeField] private List<RewardView> _rewardViews;

    public void SetReward(List<BaseRewardData> rewards)
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            RewardView rewardView = _rewardViews[i];
            rewardView.SetReward(rewards[i]);
            rewardView.gameObject.SetActive(true);
        }
        ShowView();
        MainUIManager.Instance.UnloadGamePlayView();
        AudioManager.Instance.PlayAudio(AudioType.LevelComplete);
    }

    public override void HideView()
    {
        base.HideView();
        foreach (var rewardView in _rewardViews)
        {
            rewardView.gameObject.SetActive(false);
        }
    }

    public void OnNextLevelButtonClicked()
    {
        HideView();
        GameManager.Instance.OnGameLevelExit(false);
        MainUIManager.Instance.GetView<TransitionView>().SetView(2f, () =>
        {
            MainUIManager.Instance.ShowView<HomeViewView>();
        });
    }
}