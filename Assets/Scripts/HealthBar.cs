using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Health _health;

    private Coroutine _changeHPJob;

    private void OnEnable()
    {
        _health.AmountChanged += ChangeHPWork;
    }

    private void OnDisable()
    {
        _health.AmountChanged -= ChangeHPWork;
    }

    private IEnumerator ChangeHP(float targetHealth)
    {
        float elapsedTime = 0;
        float start = Convert.ToSingle(_text.text);
        float current = start;

        while (current != targetHealth)
        {
            elapsedTime += Time.deltaTime;
            current = Mathf.MoveTowards(start, targetHealth, (elapsedTime / _duration) * Mathf.Abs(targetHealth - start));
            _text.text = current.ToString();
            yield return null;
        }
    }

    public void ChangeHPWork(float target, float previousValue)
    {
        if (_changeHPJob != null)
        {
            StopCoroutine(_changeHPJob);
        }

        _changeHPJob = StartCoroutine(ChangeHP(target));
    }
}
