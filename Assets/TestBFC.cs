using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
  public class TestBFC
  {
    private Dictionary<int, int[]> _graph = new Dictionary<int, int[]>()
    {
      {0, new int[] {5, 1}},
      {1, new int[] {0, 2}},
      {2, new int[] {1,3}},
      {3, new int[] {2, 4}},
      {4, new int[] {3, 5}},
      {5, new int[] {4, 0}},
    };

    public void run()
    {
      Bfc(0,2);
    }

    private void Bfc(int start, int goal)
    {
      var queue = new Queue<int>();
      queue.Enqueue(start);
      var visited = new Dictionary<int, int>();
      visited.Add(start, start);
      var cur = start;
      while (queue.Count != 0 && cur != goal)
      {
        var q = queue.Dequeue();

        foreach (var i in _graph[q])
        {
          if (!visited.ContainsKey(i))
          {
            Debug.Log("add: " + i + " from: " + q);
            queue.Enqueue(i);
            visited.Add(i, q);
            cur = i;
          }
        }
      }

      Debug.Log("ok");
      search(start, goal, visited);
    }

    private void search(int start, int goal, Dictionary<int, int> visited)
    {
      var current = goal;
      var res = $"{current}";
      while (current != start)
      {
        current = visited[current];
        res += "--->" + current;
      }

      Debug.Log(res);
    }
  }
}