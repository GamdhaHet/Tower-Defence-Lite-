using Sirenix.OdinInspector;
using TD.Audio;

public class BaseView : SerializedMonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;

    public virtual void ShowView()
    {
        AudioManager.Instance.PlayAudio(AudioType.PopupOpen);
        gameObject.SetActive(true);
    }

    public virtual void HideView()
    {
        gameObject.SetActive(false);
    }
}
