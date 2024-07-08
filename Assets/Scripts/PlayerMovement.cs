using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public ObstacleData obstacleData;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                TileInfo tileInfo = hit.transform.GetComponent<TileInfo>();
                if (tileInfo != null)
                {
                    List<Vector2Int> path = AStarPathfinding.FindPath(new Vector2Int((int)transform.position.x, (int)transform.position.z), tileInfo.gridPosition, obstacleData);
                    if (path != null)
                    {
                        StartCoroutine(MoveAlongPath(path));
                    }
                }
            }
        }
    }

    private IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        isMoving = true;

        foreach (Vector2Int position in path)
        {
            targetPosition = new Vector3(position.x, 1.5f, position.y);
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
