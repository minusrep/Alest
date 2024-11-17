using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    [SerializeField] private Button _exitButton;

    private void Start() 
        => Init();

    private void Init()
    {
        _startButton.onClick.AddListener(OnStartButton);

        _exitButton.onClick.AddListener(OnExitButton);
    }

    private void OnStartButton()
        => SceneManager.LoadScene(1);

    private void OnExitButton()
        => Application.Quit();
}
