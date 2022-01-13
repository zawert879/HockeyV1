using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQQEE : MonoBehaviour
{
  public GameObject _q;
  private bool isCreate = false;
  private GameObject _instanse;

  public void CreateUnit()
  {
    isCreate = !isCreate;
    if (isCreate)
    {
      _instanse = Instantiate(_q);
      _instanse.SetActive(false);
    }
    else
    {
      Destroy(_instanse);
    }
  }

  private void Update()
  {
    if (!isCreate) return;

    var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
    Debug.DrawRay(ray.origin, ray.direction * 15, Color.yellow);
    if (GameManager.Instance.Collider.Raycast(ray, out var hit, 300.0f))
    {
      _instanse.SetActive(true);
      var material = _instanse.GetComponent<Renderer>().material;
      Color color = material.color;
      color.a = .6f;
      material.SetColor("_Color", color);
      var hitPoint = hit.point;
      var cell = GameManager.Instance.Tilemap.WorldToCell(hitPoint);
      var center = GameManager.Instance.Tilemap.CellToWorld(cell);
      _instanse.transform.position = center;
      if (Input.GetButtonDown("Fire1"))
      {
        isCreate = !isCreate;
        color.a = 1f;

        material.SetColor("_Color", color);
        GameManager.Instance._units.Add(cell, _instanse);
      }
    }
    else
    {
      _instanse.SetActive(false);
    }
  }
}