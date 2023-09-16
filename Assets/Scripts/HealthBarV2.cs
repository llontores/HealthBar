using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HealthBarV2 : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _duration;
    [SerializeField] private Slider _slider;

    private Coroutine _changeHPJob;

    private void OnEnable()
    {
        _health.AmountChanged += ChangeHPWork;
    }

    private void OnDisable()
    {
        _health.AmountChanged -= ChangeHPWork;
    }

    private IEnumerator ChangeHP(float target, float previousValue)
    {
        float elapsedTime = 0;
        float startValue = previousValue;
        float current = startValue;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            current = Mathf.Lerp(startValue, target, elapsedTime / _duration);
            _slider.value = current / (_health.MaxValue - 1);

            yield return null;
        }
    }

    private void ChangeHPWork(float target, float previousValue)
    {
        if (_changeHPJob != null)
        {
            StopCoroutine(_changeHPJob);
        }

        _changeHPJob = StartCoroutine(ChangeHP(target, previousValue));
    }
}
