using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartView : MonoBehaviour
{
    [SerializeField] private List<Image> _heartImages;

    public void SetHeartView(int lives)
    {
        for (int i = 0; i < _heartImages.Count; i++)
        {
            _heartImages[i].gameObject.SetActive(i < lives);
        }
    }
}
