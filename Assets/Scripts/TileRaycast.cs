using UnityEngine;
using UnityEngine.UI;

public class TileRaycast : MonoBehaviour
{
    public Text tileInfoText;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            TileInfo tileInfo = hit.transform.GetComponent<TileInfo>();
            if (tileInfo != null)
            {
                tileInfoText.text = $"Tile: {tileInfo.gridPosition.x}, {tileInfo.gridPosition.y}";
            }
        }
    }
}
