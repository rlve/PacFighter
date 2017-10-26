using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CountTiles : MonoBehaviour {

    bool[,] tileArray = new bool[34, 14];

    public Tilemap tileMap = null;

    public List<Vector3> availablePlaces;

    void Start()
    {
        tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    tileArray[n + 17, p + 7] = true;
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                    tileArray[n + 17, p + 7] = false;
                }
            }
        }
    }

    private void Update()
    {
        Debug.Log(tileArray[0,0]);


    }

}
