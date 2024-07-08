using System.Collections.Generic;
using UnityEngine;

public static class AStarPathfinding
{
    private static List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>
        {
            new Vector2Int(position.x + 1, position.y),
            new Vector2Int(position.x - 1, position.y),
            new Vector2Int(position.x, position.y + 1),
            new Vector2Int(position.x, position.y - 1)
        };

        return neighbors;
    }

    private static int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal, ObstacleData obstacleData)
    {
        List<Vector2Int> openSet = new List<Vector2Int> { start };
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, int> gScore = new Dictionary<Vector2Int, int>
        {
            { start, 0 }
        };
        Dictionary<Vector2Int, int> fScore = new Dictionary<Vector2Int, int>
        {
            { start, Heuristic(start, goal) }
        };

        while (openSet.Count > 0)
        {
            Vector2Int current = openSet[0];
            foreach (Vector2Int position in openSet)
            {
                if (fScore.ContainsKey(position) && fScore[position] < fScore[current])
                {
                    current = position;
                }
            }

            if (current == goal)
            {
                List<Vector2Int> path = new List<Vector2Int>();
                while (cameFrom.ContainsKey(current))
                {
                    path.Insert(0, current);
                    current = cameFrom[current];
                }
                return path;
            }

            openSet.Remove(current);
            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (neighbor.x < 0 || neighbor.x >= 10 || neighbor.y < 0 || neighbor.y >= 10 || obstacleData.GetObstacle(neighbor.x, neighbor.y))
                {
                    continue;
                }

                int tentativeGScore = gScore[current] + 1;
                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }
}
