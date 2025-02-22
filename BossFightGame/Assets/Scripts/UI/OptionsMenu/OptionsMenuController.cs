using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [SerializeField] private Button _closeOptionsMenuButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _closeOptionsMenuButton.onClick.AddListener(()=>GameManager.ToggleOptionsMenu(false));
    }

    private void OnDisable()
    {
        _closeOptionsMenuButton.onClick.RemoveAllListeners();
    }
}
