using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyAI : MonoBehaviour, IAI
{
    public PlayerMovement player;
    public float moveSpeed = 2f;
    public ObstacleData obstacleData;

    private bool isMoving = false;

    void Update()
    {
        if (!isMoving && Vector3.Distance(player.transform.position, transform.position) > 1.5f)
        {
            Move();
        }
    }


    public void Move()
    {
        Vector2Int start = new Vector2Int((int)transform.position.x, (int)transform.position.z);
        Vector2Int goal = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.z);

        List<Vector2Int> path = AStarPathfinding.FindPath(start, goal, obstacleData);
        if (path != null && path.Count > 1)
        {
            StartCoroutine(MoveAlongPath(path.GetRange(0, Mathf.Min(4, path.Count))));
        }
    }

    private IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        isMoving = true;

        for(int i = 0; i < path.Count-1; i++) 
        {
            Vector3 targetPosition = new Vector3(path[i].x, 1, path[i].y);
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPosition;
        }

        isMoving = false;
    }
}
