using System;

public class InternetCheckHandle : IEquatable<InternetCheckHandle>
{
    #region PUBLIC_VARS

    public bool IsConnectionAvailable { get; private set; }
    public object SourceObject { get; private set; }
    public Action<InternetCheckHandle> OnCheckDone { get; private set; }
    public bool ShowPopup { get; private set; }
    public bool IsDone { get; private set; }

    #endregion

    #region PUBLIC_METHODS

    public InternetCheckHandle(object sourceObject, Action<InternetCheckHandle> onCheckDone, bool showPopup)
    {
        SourceObject = sourceObject;
        OnCheckDone = onCheckDone;
        ShowPopup = showPopup;
    }

    public void SetConnectionStatus(bool status)
    {
        IsConnectionAvailable = status;
        IsDone = true;
    }

    public bool Equals(InternetCheckHandle other)
    {
        return other.GetHashCode() == GetHashCode();
    }

    #endregion
}