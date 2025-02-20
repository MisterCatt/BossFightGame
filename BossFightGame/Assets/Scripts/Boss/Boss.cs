using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit, ITargetable
{
    public enum AttackProgress { AVALIBLE, INPROGRESS }
    [SerializeField]
    private AttackProgress _attackInProgress = AttackProgress.AVALIBLE;

    private List<Action> _attackPattern;
    private int _attackInSequence = 0;

    public bool AttackPatternShouldLoop = true;

    

    private void Awake()
    {
        _attackPattern = new List<Action>();
    }

    private void OnDisable()
    {
        _attackPattern.Clear();
    }

    private IEnumerator Start()
    {
        if(UnitManager.Instance) 
            UnitManager.Instance.BossesInLevel.Add(gameObject);

        if (BossManager.Instance)
            BossManager.Instance.BossesInLevel.Add(gameObject.GetComponent<Boss>());

        yield return new WaitForSeconds(3f);
        Debug.Log("first attack");
        DoNextAttack();
    }
    public GameObject ReturnTarget()
    {
        return gameObject;
    }

    /// <summary>
    /// Invokes the next attack in the boss attack pattern
    /// </summary>
    public void DoNextAttack()
    {
        if (_attackInProgress == AttackProgress.INPROGRESS) return;

        if (_attackInSequence >= _attackPattern.Count)
        {
            if (AttackPatternShouldLoop)
                _attackInSequence = 0;
            else
            {
                Debug.Log("Boss enrage");
                return;
            }
        }

        _attackPattern[_attackInSequence].Invoke();
        _attackInSequence++;

        _attackInProgress = AttackProgress.INPROGRESS;
    }

    public void AttackOver()
    {
        _attackInProgress = AttackProgress.AVALIBLE;
        DoNextAttack();
    }

    public void AddAttackToAttackPattern(Action attack)
    {
        _attackPattern.Add(attack);
    }
}
