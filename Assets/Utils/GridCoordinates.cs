using UnityEngine;

namespace Utils
{
  public abstract class GridCoordinates
  {
    public static Vector3Int UnityCellToCube(Vector3Int cell)
    {
      var yCell = cell.x;
      var xCell = cell.y;
      var x = yCell - (xCell - (xCell & 1)) / 2;
      var z = xCell;
      var y = -x - z;
      return new Vector3Int(x, y, z);
    }

    public static Vector3Int CubeToUnityCell(Vector3Int cube)
    {
      var x = cube.x;
      var z = cube.z;
      var col = x + (z - (z & 1)) / 2;
      var row = z;

      var res = new Vector3Int(col, row, 0);
      return res;
    }

    private static float Lerp(float a, float b, float t)
    {
      return a + (b - a) * t;
    }


    public static Vector3 CubeLerp(Vector3 a, Vector3 b, float t)
    {
      var vector3 = new Vector3(Lerp(a.x, b.x, t), Lerp(a.y, b.y, t), Lerp(a.z, b.z, t));
      Debug.DrawLine(vector3, new Vector3(vector3.x, vector3.y + 10, vector3.z), Color.yellow, 100f);
      return vector3;
    }

    public static float CubeDistance(Vector3 a, Vector3 b)
    {
      return (a - b).magnitude;
    }

    public static readonly Vector3Int VecDr = new Vector3Int(+1, 0, -1);
    public static readonly Vector3Int VecR = new Vector3Int(+1, -1, 0);
    public static readonly Vector3Int VecUr = new Vector3Int(0, -1, +1);
    public static readonly Vector3Int VecUl = new Vector3Int(-1, 0, +1);
    public static readonly Vector3Int VecL = new Vector3Int(-1, +1, 0);
    public static readonly Vector3Int VecDl = new Vector3Int(0, +1, -1);
    public static readonly Vector3Int VecDiagDr = new Vector3Int(+2, -1, -1);
    public static readonly Vector3Int VecDiagUr = new Vector3Int(+1, -2, +1);
    public static readonly Vector3Int VecU = new Vector3Int(-1, -1, +2);
    public static readonly Vector3Int VecDiagUl = new Vector3Int(-2, +1, +1);
    public static readonly Vector3Int VecDiagDl = new Vector3Int(-1, +2, -1);
    public static readonly Vector3Int VecD = new Vector3Int(+1, +1, -2);
  }
}