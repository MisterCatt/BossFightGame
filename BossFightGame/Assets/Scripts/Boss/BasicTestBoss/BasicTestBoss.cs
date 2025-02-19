using System.Collections;
using UnityEngine;

public class BasicTestBoss : MonoBehaviour
{
    public GameObject Laser, preLaserField;

    public Boss boss;

    private Vector3 targetMovePosition;
    [SerializeField] private float _bossWalkSpeed;

    private void Start()
    {
        targetMovePosition = transform.position;
        boss.AddAttackToAttackPattern(MoveToRandomPosition);
        boss.AddAttackToAttackPattern(LaserThroughMiddle);
    }

    private void Update()
    {
        var step = _bossWalkSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetMovePosition, step);
    }

    public void MoveToRandomPosition()
    {
        StartCoroutine(SetMovePosition());
    }

    private IEnumerator SetMovePosition()
    {
        targetMovePosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
        yield return new WaitForSeconds(2f);
        boss.AttackOver();
    }

    public void LaserThroughMiddle()
    {
        StartCoroutine(ShootLaser());
    }

    private IEnumerator ShootLaser()
    {
        preLaserField.SetActive(true);
        yield return new WaitForSeconds(3f);
        Laser.SetActive(true);
        yield return new WaitForSeconds(3f);
        preLaserField.SetActive(false);
        Laser.SetActive(false);
        boss.AttackOver();
    }


}
