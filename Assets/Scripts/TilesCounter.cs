using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NesScripts.Controls.PathFind;

public class TilesCounter : MonoBehaviour {

    bool[,] tileArray;

    public Tilemap tileMap;

    public List<Vector3> availablePlaces;

    int tileMapWidth;
    int tileMapHeight;

    void Start()
    {
        //tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();

        tileMapWidth = -tileMap.cellBounds.xMin + tileMap.cellBounds.xMax;
        tileMapHeight = -tileMap.cellBounds.yMin + tileMap.cellBounds.yMax;
        tileArray = new bool[tileMapWidth, tileMapHeight];

        PrepareTileArrays();
    }

    void PrepareTileArrays()
    {
        int arrayIndexN = 0;
        int arrayIndexP = 0;

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            arrayIndexP = 0;
            for (int p = tileMap.cellBounds.yMax - 1; p >= tileMap.cellBounds.yMin; p--)
            {

                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place" 
                    tileArray[arrayIndexN, arrayIndexP] = false;
                }
                else
                {
                    //No tile at "place"
                    tileArray[arrayIndexN, arrayIndexP] = true;
                    availablePlaces.Add(place);
                }
                arrayIndexP++;
            }
            arrayIndexN++;
        }
    }

    public bool[,] GetTileArrays()
    {
        return tileArray;
    }

    public Point LocalGridToPathGrid (Vector3Int localGrid)
    {
        return new Point(localGrid.x + (-1) * (tileMap.cellBounds.xMin), (-1) * localGrid.y + (tileMap.cellBounds.yMax - 1));
    }
    public Vector3Int PathGridToLocalGrid(Point pathGrid)
    {
        return new Vector3Int(pathGrid.x + tileMap.cellBounds.xMin, -pathGrid.y + (tileMap.cellBounds.yMax - 1),0);
    }

    public Vector3Int PointToVector(Point point)
    {
        return new Vector3Int(point.x, point.y, 0);
    }

    public Point VectorToPoint(Vector3Int vector)
    {
        return new Point(vector.x, vector.y);
    }



}
