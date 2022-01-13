using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
  private Vector3 _target;
  [SerializeField] private float _speed;
  [SerializeField] private AnimationCurve curve;
  private bool isGO = false;

  void Update()
  {
    if (Input.GetKeyDown("g"))
    {
      GoTOPosition(new Vector3(0, 0, 0));
      this.StartCoroutine(GOOOOOOOOOOO());
    }

    // if (isGO)
    // {
    // var step = (transform.position - _target) / _speed;
    // print(step);
    // transform.position -= step;
    //
    // if (transform.position == _target)
    // {
    //   isGO = false;
    // }
    // }
  }

  private IEnumerator GOOOOOOOOOOO()
  {
    float duration = 3;
    float expiredSeconds = 0;

    float progress = 0;
    Vector3 start = transform.position;
    while (progress < 1)
    {
      expiredSeconds += Time.deltaTime;
      progress = expiredSeconds / duration;
      
      var t = curve.Evaluate(progress);

      transform.position = Vector3.LerpUnclamped(start, _target, t);
      yield return null;
    }
  }

  public void GoTOPosition(Vector3 target)
  {
    _target = target;
    isGO = true;
  }
}