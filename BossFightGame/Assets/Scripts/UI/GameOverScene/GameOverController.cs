using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton, _restartButton;

    private void Start()
    {
        _mainMenuButton.onClick.AddListener(() => GameManager.LoadMainMenu());
        _restartButton.onClick.AddListener(() => GameManager.LoadPlayground());
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
    }
}
