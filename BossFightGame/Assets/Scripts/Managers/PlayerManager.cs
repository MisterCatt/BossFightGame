using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public List<GameObject> AllUnlockedClasses, AllLockedClasses, AllClasses;

    [SerializeField]
    private Player _player1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public Player GetPlayer()
    {
        return _player1;
    }

    public void SetPlayer(Player player)
    {
        _player1 = player;
    }
}
