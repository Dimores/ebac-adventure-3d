using TMPro;
using UnityEngine;
using DG.Tweening;

public class UITextFader : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textToFade;
    public string prefix = "Checkpoint ativado";

    [Header("Animation")]
    public float fadeTime = .3f;
    public float waitTime = 1.5f; 

    private Sequence _fadeSequence;

    private void Awake()
    {
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);
    }

    private void setText(string newText, string newPrefix = null)
    {
        if(prefix == null)
            textToFade.text = $"{prefix}: {newText}!";
        else
            textToFade.text = $"{newPrefix}: {newText}!";
    }

    public void Fade(string text, string newPrefix = null)
    {
        setText(text, newPrefix);
        TriggerFadeSequence();
    }

    private void TriggerFadeSequence()
    {
        _fadeSequence?.Kill();

        _fadeSequence = DOTween.Sequence().Append(textToFade.DOFade(1f, fadeTime))    
            .AppendInterval(waitTime)                   
            .Append(textToFade.DOFade(0f, fadeTime));   
    }
}