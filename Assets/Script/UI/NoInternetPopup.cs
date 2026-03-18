using TD.Manager;

namespace TD.UI
{
    public class NoInternetPopup : BaseView
    {
        public void CheckInternet()
        {
            BufferingScreen bufferingView = MainUIManager.Instance.GetView<BufferingScreen>();
            bufferingView.ShowView();
            InternetChecker.Instance.CheckInternet((result) =>
            {
                if (result)
                {
                    bufferingView.HideView();
                }
                else
                {
                    bufferingView.HideView();
                    ShowView();
                }
            });
        }
    }
}
