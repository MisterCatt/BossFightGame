using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public List<GameObject> AllUnlockedClasses, AllLockedClasses, AllClasses;

    [SerializeField]
    private Player _player1;

    [SerializeField] private CinemachineCamera _cineCamera;
    [SerializeField] private Slider _PlayerHealthSlider;
    [SerializeField] private TMP_Text _PlayerHealthSliderText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void OnDisable()
    {
        UnsubscribeToPlayerEvents();
    }

    public Player GetPlayer()
    {
        return _player1;
    }

    public void SetPlayer(Player player)
    {
        _player1 = player;
        _cineCamera.Follow = _player1.transform;
        _player1.OnUnitHeal += UpdatePlayerHealthbar;
        _player1.OnUnitTakeDamage += UpdatePlayerHealthbar;
        _player1.OnUnitDeath += PlayerDied;

    }

    /// <summary>
    /// Changes the player health slider to the correct ammount of health the player has.
    /// If the value is -1 the healthbar is set to 0.
    /// </summary>
    /// <param name="value">the ammount that changed (both up and down)</param>
    public void UpdatePlayerHealthbar(int value)
    {
        _PlayerHealthSlider.value = _player1.GetCurrentHealth();
        _PlayerHealthSliderText.text = _PlayerHealthSlider.value + "/100";

        if (value == -1)
        {
            _PlayerHealthSlider.value = 0;
            _PlayerHealthSliderText.text = "0/100";
        }
    }

    private void PlayerDied()
    {
        UpdatePlayerHealthbar(-1);
        UnsubscribeToPlayerEvents();
        _player1 = null;
    }

    private void UnsubscribeToPlayerEvents()
    {
        _player1.OnUnitDeath -= PlayerDied;
        _player1.OnUnitHeal -= UpdatePlayerHealthbar;
        _player1.OnUnitTakeDamage -= UpdatePlayerHealthbar;
    }

}
