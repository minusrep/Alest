using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public event Action OnStateChangeEvent;

    public int Health
    {
        get => _health;

        set
        {
            if (value < 0) value = 0;

            _health = value;

            OnStateChangeEvent?.Invoke();
        }
    }

    [SerializeField] private int _health;

}
