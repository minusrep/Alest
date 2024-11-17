using System;
using UnityEngine;

namespace Root.Player
{
    public class PlayerStats : MonoBehaviour
    {
        public event Action OnPlayerDeathEvent;

        public event Action OnStatsChangeEvent;

        public int Money
        {
            get => _money;

            set
            {
                _money = value;

                OnStatsChangeEvent?.Invoke();
            }
        }

        public int Souls
        {
            get => _souls;

            set
            {
                _souls = value;

                OnStatsChangeEvent?.Invoke();
            }
        }

        public int Health
        {
            get => _health;

            set
            {
                if (value <= 0) value = 0;


                _health = value;

                OnStatsChangeEvent?.Invoke();

                if (_health == 0) OnPlayerDeathEvent?.Invoke();
            }
        }

        private int _souls = 0;

        [SerializeField] private int _health = 3;
    
        [SerializeField] private int _money = 0;

        private void Awake()
        {

        }
    }
}

