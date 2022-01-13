using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
  [SerializeField] private GameObject _o;
  public Dictionary<Vector3Int, GameObject> _units = new Dictionary<Vector3Int, GameObject>();

  public Tilemap Tilemap
  {
    get { return _o.GetComponentInChildren<Tilemap>(); }
  }

  public Collider Collider
  {
    get { return _o.GetComponentInChildren<Collider>(); }
  }
}