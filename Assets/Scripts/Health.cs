using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    public float Amount { get; private set; }
    public float PreviousValue { get; private set; }
    public float MaxValue { get; private set; }
    public event Action<float,float> AmountChanged;

    private void Start()
    {
        Amount = _maxValue;
        PreviousValue = Amount;
        MaxValue = _maxValue;
        AmountChanged?.Invoke(Amount,PreviousValue);
    }

    public void TakeDamage(float amount)
    {
        PreviousValue = Amount;
        Amount =  Mathf.Clamp(Amount - amount, 0, _maxValue); 
        AmountChanged?.Invoke(Amount, PreviousValue);
    }

    public void Heal(float amount)
    {
        PreviousValue = Amount;
        Amount = Mathf.Clamp(Amount + amount, 0, _maxValue);
        AmountChanged?.Invoke(Amount, PreviousValue);
    }
}
