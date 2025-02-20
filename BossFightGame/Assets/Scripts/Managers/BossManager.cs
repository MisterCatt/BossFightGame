using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    [SerializeField]
    private Boss _boss;

    [SerializeField] private Slider _BossHealthSlider;
    [SerializeField] private TMP_Text _BossHealthSliderText;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void SetBoss(Boss boss)
    {
        _boss = boss;
        _boss.OnUnitTakeDamage += UpdateHealthbar;
        _boss.OnUnitDeath += BossDied;

    }

    /// <summary>
    /// Changes the player health slider to the correct ammount of health the player has.
    /// If the value is -1 the healthbar is set to 0.
    /// </summary>
    /// <param name="value">the ammount that changed (both up and down)</param>
    public void UpdateHealthbar(int value)
    {
        _BossHealthSlider.value = _boss.GetCurrentHealth();
        _BossHealthSliderText.text = _BossHealthSlider.value + "/100";

        if (value == -1)
        {
            _BossHealthSlider.value = 0;
            _BossHealthSliderText.text = "0/100";
        }
    }

    private void BossDied()
    {
        UpdateHealthbar(-1);
        UnsubscribeToEvents();
        _boss = null;
    }

    private void UnsubscribeToEvents()
    {
        _boss.OnUnitDeath -= BossDied;
        _boss.OnUnitTakeDamage -= UpdateHealthbar;
    }
}
