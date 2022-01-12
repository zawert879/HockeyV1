using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class Qq : MonoBehaviour
{
  [SerializeField] private Tile tile;
  [SerializeField] private Tile tile2;
  [SerializeField] private Tile tile3;
  [SerializeField] private Tile tileStop;

  private Tilemap _tilemap;
  private Collider _collider;

  private void Awake()
  {
    _tilemap = GetComponentInChildren<Tilemap>();
    _collider = GetComponentInChildren<Collider>();
  }

  private void Start()
  {
    Debug.DrawLine(Vector3.zero, new Vector3(5, 0, 0), Color.white, 2.5f);
    var bounds = _tilemap.cellBounds;
    var allPositionsWithin = bounds.allPositionsWithin;
    foreach (var within in allPositionsWithin)
    {
      CreateCordsText(within);
    }
  }

  private Vector3Int? _tileVector = null;

  private void Update()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      KK();
    }

    if (Input.GetButtonDown("Fire2"))
    {
      var tilemap = GetComponentInChildren<Tilemap>();

      var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
      Debug.DrawRay(ray.origin, ray.direction * 15, Color.yellow);
      if (_collider.Raycast(ray, out var hit, 300.0f))
      {
        var cell = tilemap.WorldToCell(hit.point);
        var w = _tilemap.GetTile(cell);
        print(tile == w);
      }
    }

    void KK()
    {
      var tilemap = GetComponentInChildren<Tilemap>();

      var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
      Debug.DrawRay(ray.origin, ray.direction * 15, Color.yellow);
      if (_collider.Raycast(ray, out var hit, 300.0f))
      {
        var cell = tilemap.WorldToCell(hit.point);
        // BFC
        if (_tileVector is null)
        {
          _tileVector = cell;
        }
        else
        {
          Bfc((Vector3Int) _tileVector, cell);
        }

        tilemap.SetTile(new Vector3Int(cell.x, cell.y, cell.z), tile);
      }

      // var cube = GridCoordinates.UnityCellToCube(cell);
      // tilemap.SetTile(GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecDl), tile);
      // tilemap.SetTile(GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecDr), tile);
      // tilemap.SetTile(GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecR), tile);
      // tilemap.SetTile(GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecUr), tile);
      // tilemap.SetTile(GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecUl), tile);
      // tilemap.SetTile(GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecL), tile);
    }
  }

  private void SearchGoal(Vector3Int start, Vector3Int goal, Dictionary<Vector3Int, Vector3Int> visited)
  {
    var current = goal;
    while (current != start)
    {
      current = visited[current];
      _tilemap.SetTile(current, tile3);
    }
  }

  private List<Vector3Int> GetNextTiles(Vector3Int current)
  {
    var cube = GridCoordinates.UnityCellToCube(current);
    var list = new List<Vector3Int>();

    var vecUr = GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecUr);
    if (CheckTile(vecUr)) list.Add(vecUr);

    var vecR = GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecR);
    if (CheckTile(vecR)) list.Add(vecR);

    var vecDr = GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecDr);
    if (CheckTile(vecDr)) list.Add(vecDr);

    var vecDl = GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecDl);
    if (CheckTile(vecDl)) list.Add(vecDl);

    var vecL = GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecL);
    if (CheckTile(vecL)) list.Add(vecL);

    var vecUl = GridCoordinates.CubeToUnityCell(cube + GridCoordinates.VecUl);
    if (CheckTile(vecUl)) list.Add(vecUl);

    return list;
  }

  private bool CheckTile(Vector3Int vec)
  {
    if (_tilemap.HasTile(vec))
    {
      return _tilemap.GetTile(vec) != tileStop;
    }

    return false;
  }

  private void Bfc(Vector3Int start, Vector3Int goal)
  {
    var queue = new Queue<Vector3Int>();
    queue.Enqueue(start);
    var visited = new Dictionary<Vector3Int, Vector3Int>();
    visited.Add(start, start);
    var cur = start;
    while (queue.Count != 0 && cur != goal)
    {
      if (queue.Count != 0)
      {
        var current = queue.Dequeue();
        _tilemap.SetTile(current, tile2);

        foreach (var i in GetNextTiles(current))
        {
          if (!visited.ContainsKey(i))
          {
            queue.Enqueue(i);
            visited.Add(i, current);
            _tilemap.SetTile(i, tile);

            cur = i;
          }
        }
      }
    }

    Debug.Log("ok");
    SearchGoal(start, goal, visited);
  }


  private void CubeLinedraw(Vector3 a, Vector3 b)
  {
    Debug.DrawLine(a, b, Color.red, 100f);
    var N = GridCoordinates.CubeDistance(a, b);

    for (int i = 0; i < N * 2; i++)
    {
      _tilemap.SetTile(_tilemap.WorldToCell(GridCoordinates.CubeLerp(a, b, 0.5f / N * i)), tile);
    }
  }

  private void CreateCordsText(Vector3Int cord)
  {
    var text = new GameObject();
    text.transform.position = _tilemap.CellToWorld(new Vector3Int(cord.x, cord.y, 0));
    text.transform.Rotate(90f, 0, 0);
    text.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    var w = text.AddComponent<TextMesh>();
    var cube2 = GridCoordinates.UnityCellToCube(new Vector3Int(cord.x, cord.y, 0));

    w.text = "x:" + cord.x + " y:" + cord.y + $"\n x:{cube2.x} y:{cube2.y} z:{cube2.z}";
  }
}