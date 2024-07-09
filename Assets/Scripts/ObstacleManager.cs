using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (obstacleData.GetObstacle(x, y))
                {
                    GameObject obj = Instantiate(obstaclePrefab, new Vector3(x, 0.5f, y), Quaternion.identity);
                    obj.transform.SetParent(transform);
                }
            }
        }
    }
}
