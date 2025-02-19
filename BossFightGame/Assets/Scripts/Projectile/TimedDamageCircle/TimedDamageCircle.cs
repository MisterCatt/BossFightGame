using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TimedDamageCircle : MonoBehaviour
{
    [SerializeField] private GameObject _maxArea;
    [SerializeField] private GameObject _fillCircle;
    [SerializeField] private GameObject _hurtCircle;

    [SerializeField] private float _fillTimeSeconds = 3f, _timeAfterFillDisableSeconds = 3f;

    private void OnEnable()
    {
        
        StartCoroutine(TimeToDisable());
    }

    private void OnDisable()
    {
        _hurtCircle.SetActive(false);
        _fillCircle.SetActive(true);
        _fillCircle.transform.localScale = Vector3.zero;


        StopAllCoroutines();
    }

    private IEnumerator TimeToDisable()
    {
        _fillCircle.transform.DOScale(_maxArea.transform.localScale.x, _fillTimeSeconds);
        yield return new WaitForSeconds(_fillTimeSeconds);
        _hurtCircle.SetActive(true);
        _fillCircle.SetActive(false);
        yield return new WaitForSeconds(_timeAfterFillDisableSeconds);
        gameObject.SetActive(false);
    }
}
