using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Test : MonoBehaviour
{
  void Start()
  {
    var test = new TestBFC();
    test.run();
  }
}