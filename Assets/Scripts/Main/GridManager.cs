using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject prefab;

    public Dictionary<Vector2, Vector2> points = new Dictionary<Vector2, Vector2>();
    
    private void Awake()
    {
        points = new Dictionary<Vector2, Vector2>();
        GetTilemapInfo();
    }

    private void GetTilemapInfo()
    {
        for(int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
        {
            for(int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
            {
                // 타일 포지션
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if(tilemap.HasTile(tilePosition))
                {   // 월드 기준으로 좌표 변경(타일 기준은 왼쪽 아래임)
                    Vector2 worldPosition = tilemap.CellToWorld(tilePosition);

                    worldPosition.x += tilemap.cellSize.x / 2;
                    worldPosition.y += tilemap.cellSize.y / 2;

                    points[worldPosition] = worldPosition;
                    //Instantiate(prefab, worldPosition, Quaternion.identity);
                }
            }
        }
    }
    /*
    public Vector2 ReturnPos(Vector2 currentPos)
    {
        Vector2 nearestTile;



        return nearestTile;
    }
    */
}
