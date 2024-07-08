using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridSize = 10;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                tile.name = $"Tile_{x}_{y}";
                TileInfo tileInfo = tile.GetComponent<TileInfo>();
                tileInfo.gridPosition = new Vector2Int(x, y);
            }
        }
    }
}
