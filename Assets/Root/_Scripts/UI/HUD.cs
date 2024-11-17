using Root.Player;
using TMPro;
using UnityEngine;

namespace UII
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _money;

        [SerializeField] private TextMeshProUGUI _souls;

        [SerializeField] private TextMeshProUGUI _health;

        [SerializeField] private PlayerStats _stats;

        private void Start()
            => Init();

        public void Init()
        {
            Refresh();

            _stats.OnStatsChangeEvent += Refresh;
        }

        private void Refresh()
        {
            _money.text = _stats.Money.ToString();

            _souls.text = _stats.Souls.ToString();

            _health.text = _stats.Health.ToString();
        }
    }
}
