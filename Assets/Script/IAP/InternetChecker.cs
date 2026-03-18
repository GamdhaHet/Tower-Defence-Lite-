using System;
using System.Collections;
using TD.Manager;
using TD.UI;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    public static InternetChecker Instance;

    [SerializeField] private string _url = "https://clients3.google.com/generate_204";
    [SerializeField] private float _checkInterval = 30f;
    private Coroutine _periodicCheckCoroutine;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartPeriodicInternetCheck();
    }

    public void CheckInternet(Action<bool> action)
    {
        StartCoroutine(CheckInternetCoroutine(action));
    }

    public void StartPeriodicInternetCheck()
    {
        if (_periodicCheckCoroutine == null)
        {
            _periodicCheckCoroutine = StartCoroutine(PeriodicInternetCheckCoroutine());
        }
    }

    public void StopPeriodicInternetCheck()
    {
        if (_periodicCheckCoroutine != null)
        {
            StopCoroutine(_periodicCheckCoroutine);
            _periodicCheckCoroutine = null;
        }
    }

    private IEnumerator CheckInternetCoroutine(Action<bool> action)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(_url))
        {
            www.timeout = 5;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
                action(true);
            else
                action(false);
        }
    }

    private IEnumerator PeriodicInternetCheckCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkInterval);

            CheckInternet((isConnected) =>
            {
                NoInternetPopup popup = MainUIManager.Instance.GetView<NoInternetPopup>();
                if (popup == null) return;

                if (isConnected && popup.IsActive)
                    popup.HideView();
                else if (!isConnected && !popup.IsActive)
                    popup.ShowView();
            });
        }
    }
}