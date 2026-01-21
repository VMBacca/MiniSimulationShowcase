using TMPro;
using UnityEngine;

public class ObjectCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;

    public void UpdateCounter(int current, int max)
    {
        counterText.text = $"Objects: {current} / {max}";
    }
}
