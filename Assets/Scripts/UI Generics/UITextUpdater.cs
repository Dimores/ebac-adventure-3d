using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;

    public void UpdateText(IntData intData)
    {
        uiText.text = intData.value.ToString();
    }

    public void UpdateText(int value)
    {
        uiText.text = value.ToString();
    }

    public void UpdateText(string newText)
    {
        uiText.text = newText;
    }

    public void UpdateText(int value, string text)
    {
        uiText.text = text + value.ToString();
    }

}
