using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NesScripts.Controls.PathFind;

public class TilesCounter : MonoBehaviour {

    bool[,] tileArray;

    public Tilemap tileMap = null;

    public List<Vector3> availablePlaces;

    int tileMapWidth;
    int tileMapHeight;

    //public Vector3Int testLocalToPath;
    //public Vector3Int LocalToPathResult;
    //public Vector3Int testPathToLocal;
    //public Vector3Int PathToLocalResult;

    void Start()
    {
        tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();

        tileMapWidth = -tileMap.cellBounds.xMin + tileMap.cellBounds.xMax;
        tileMapHeight = -tileMap.cellBounds.yMin + tileMap.cellBounds.yMax;
        tileArray = new bool[tileMapWidth, tileMapHeight];

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
                    tileArray[arrayIndexN, arrayIndexP] = false;
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                    tileArray[arrayIndexN, arrayIndexP] = true;
                }
                arrayIndexP++;
            }
            arrayIndexN++;
        }
    }

    private void Update()
    {
        //LocalToPathResult = PointToVector(LocalGridToPathGrid(testLocalToPath));
        //PathToLocalResult = PathGridToLocalGrid(VectorToPoint(testPathToLocal));
    }

    public Tilemap GetTilemap()
    {
        return tileMap;
    }

    public bool[,] GetTileArrays()
    {
        return tileArray;
    }

    public int[] GetMapSizeInCells()
    {
        return new int[] { tileMapWidth, tileMapHeight };
    }

    public Point LocalGridToPathGrid (Vector3Int localGrid)
    {
        return new Point(localGrid.x + (-1) * (tileMap.cellBounds.xMin), (-1) * localGrid.y + (tileMap.cellBounds.yMax - 1)); ;
    }
    public Vector3Int PathGridToLocalGrid(Point pathGrid)
    {
        return new Vector3Int(pathGrid.x + tileMap.cellBounds.xMin, -pathGrid.y + (tileMap.cellBounds.yMax - 1),0); ;
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
