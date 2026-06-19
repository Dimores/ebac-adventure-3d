using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Ebac.Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    
    [SerializeField] private Vignette _vignette;

    public float duration = .1f;

    private Vignette _tmpVignette;

    private Coroutine _currentCoroutine;

    public void ChangeVignette()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine( _currentCoroutine );

            if (processVolume.profile.TryGetSettings<Vignette>(out _tmpVignette))
            {
                _vignette = _tmpVignette;
            }

            ColorParameter c = new ColorParameter();
            c.value = Color.black;

            _vignette.color.Override(c);

        } 
        _currentCoroutine = StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        if (processVolume.profile.TryGetSettings<Vignette>(out _tmpVignette))
        {
            _vignette = _tmpVignette;
        }

        ColorParameter c = new ColorParameter();

        float time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }
    }
}
