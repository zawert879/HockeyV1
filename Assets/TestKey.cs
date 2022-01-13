using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKey : MonoBehaviour
{
  private void Awake()
  {
    var speed = 10;
    var q = new Vector3(0, 0, 0);
    var w = new Vector3(-5, 10, 10);

    print(Vector3.LerpUnclamped(q, w, 0.5f));
  }
}