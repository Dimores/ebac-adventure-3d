using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .2f;

    private Color _defaultColor;

    private Tween _currentTween;

    private void Start()
    {
        _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    public void Flash()
    {
        if(!_currentTween.IsActive())
            _currentTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }
}
