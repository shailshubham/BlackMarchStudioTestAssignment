using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    public bool[] obstacles = new bool[100];

    public bool GetObstacle(int x, int y)
    {
        return obstacles[x * 10 + y];
    }

    public void SetObstacle(int x, int y, bool value)
    {
        obstacles[x * 10 + y] = value;
    }
}
