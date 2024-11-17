using Root.Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;

        [SerializeField] private GameObject _loseWindow;

        [SerializeField] private float _loseDelay;

        private void Awake()
            => Init();

        public void Restart()
        {
            SceneManager.LoadScene(1);

            Time.timeScale = 1f;
        }

        private void Init()
        {
            _stats.OnPlayerDeathEvent += Lose;

            _loseWindow.SetActive(false);
        }

        private void Lose()
        {
            StartCoroutine(AppearDelay(() => Time.timeScale = 0f, _loseDelay));

            _loseWindow.SetActive(true);
        }


        private IEnumerator AppearDelay(Action callback, float delay)
        {
            yield return new WaitForSeconds(delay);

            callback?.Invoke();
        }
    }
}
