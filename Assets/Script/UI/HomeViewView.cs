using TD.Manager;

namespace TD.UI
{
    public class HomeViewView : BaseView
    {
        public void OnPlayButtonClick()
        {
            HideView();
            GameManager.Instance.StartGame();
        }

        public void OnSettingButtonClick()
        {
            MainUIManager.Instance.ShowView<SettingView>();
        }

        public void OnExitButtonClick()
        {
            //TODO: Exit game
        }

        public void OnShopButtonClick()
        {
            MainUIManager.Instance.ShowView<ShopView>();
        }
    }
}