using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BasicTestBoss : MonoBehaviour
{
    public GameObject Laser, preLaserField;

    public Boss boss;

    [SerializeField] private float _timeToMoveToTargetPosition = 3f;
    [SerializeField] private int _amountOfDamageCirclesToPlace = 3;
    [SerializeField] private float _timeBetweenCircles = 1;

    [SerializeField] private float _minRandomXPosition = -10f;
    [SerializeField] private float _maxRandomXPosition = 10f;
    [SerializeField] private float _minRandomYPosition = -10f;
    [SerializeField] private float _maxRandomYPosition = 10f;

    private void Start()
    {
        boss.AddAttackToAttackPattern(MoveToRandomPosition);
        boss.AddAttackToAttackPattern(LaserThroughMiddle);
        boss.AddAttackToAttackPattern(PlaceDamageCirclesUnderPlayer);
    }

    public void MoveToRandomPosition()
    {
        StartCoroutine(MoveBossToRandomPosition());
    }

    private IEnumerator MoveBossToRandomPosition()
    {
        var randomPosition = new Vector3(Random.Range(_minRandomXPosition, _maxRandomXPosition), Random.Range(_minRandomYPosition, _maxRandomYPosition));
        transform.DOMove(randomPosition, _timeToMoveToTargetPosition);
        yield return new WaitForSeconds(_timeToMoveToTargetPosition);
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

    public void PlaceDamageCirclesUnderPlayer()
    {
        StartCoroutine(PlaceCircles());
    }

    private IEnumerator PlaceCircles()
    {
        if (PlayerManager.Instance.GetPlayer()){
            for (int i = 0; i < _amountOfDamageCirclesToPlace; i++)
            {
                var circle = TimedDamageCirclePool.Instance.GetObject();
                circle.gameObject.SetActive(true);

                circle.transform.position = PlayerManager.Instance.GetPlayer().transform.position;

                yield return new WaitForSeconds(_timeBetweenCircles);
            }
        }

        yield return new WaitForSeconds(_amountOfDamageCirclesToPlace);
        boss.AttackOver();
    }


}
