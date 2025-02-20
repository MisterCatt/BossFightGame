using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    public List<Boss> BossesInLevel;

    int totalBossHealth = 100, currentBossHealth;

    [SerializeField] private Slider _BossHealthSlider;
    [SerializeField] private TMP_Text _BossHealthSliderText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void AddBossToLevel(Boss boss)
    {
        BossesInLevel.Add(boss);

        boss.OnUnitTakeDamage += UpdateBossHealthBar;

        SetBossHealthbar();
    }

    public void SetBossHealthbar()
    {
        totalBossHealth = 0;
        foreach (Boss bo in BossesInLevel)
            totalBossHealth += bo.GetCurrentHealth();

        currentBossHealth = totalBossHealth;

        _BossHealthSliderText.text = totalBossHealth.ToString()+"/"+totalBossHealth.ToString();

        _BossHealthSlider.maxValue = totalBossHealth;
        _BossHealthSlider.value = totalBossHealth;
    }

    private void UpdateBossHealthBar(int value)
    {
        Debug.Log("DAMAGE: " + value);
        _BossHealthSlider.value += value;
        currentBossHealth += value;
        _BossHealthSliderText.text = currentBossHealth + "/" + totalBossHealth.ToString();
    }

}
