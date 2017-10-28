using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NesScripts.Controls.PathFind;



public class Ghost : Character {
    TilesCounter tilesCounter;
    Pathfinding pathFinder;
    NesScripts.Controls.PathFind.Grid grid;
    public List<Point> path;

    Tilemap tileMap;
    int tileMapWidth;
    int tileMapHeight;

    bool[,] tileArray;

    public Vector3Int cellPositionGhost;
    public Vector3Int cellPositionPac;

    Vector3 curentPosition;
    int current;

    public GameObject destroyEffect;
    int currentDir;
    int frameCounter = 0;
    bool changeDirection = false;

    GameObject Pac;

    public override void Start()
    {
        base.Start();
        maxHealth = 1;
        currentHealth = maxHealth;

        tilesCounter = FindObjectOfType<Tilemap>().GetComponent<TilesCounter>();
        tileArray = tilesCounter.GetTileArrays();

        tileMap = tilesCounter.GetTilemap();
        tileMapWidth = tilesCounter.GetMapSizeInCells()[0];
        tileMapHeight = tilesCounter.GetMapSizeInCells()[1];

        grid = new NesScripts.Controls.PathFind.Grid(tileMapWidth, tileMapHeight, tileArray);

        Pac = GameObject.FindGameObjectWithTag("Player");
    }


    public override void Update()
    {
        //    if (transform.position != junctions[current].transform.position)
        //    {
        //        Vector3 pos = Vector3.MoveTowards(transform.position, junctions[current].transform.position, speed * Time.deltaTime);
        //        GetComponent<Rigidbody2D>().MovePosition(pos);
        //    }
        //    else
        //    {
        //        current = (current + 1) % junctions.Length;
        //    }

        Movement();
        
    }

    public override void Movement()
    {
        cellPositionPac = tileMap.WorldToCell(Pac.transform.position);
        cellPositionGhost = tileMap.WorldToCell(transform.position);

        path = Pathfinding.FindPath(grid, tilesCounter.LocalGridToPathGrid(cellPositionGhost), tilesCounter.LocalGridToPathGrid(cellPositionPac));
        //Debug.Log("");
    }

    public void ChangeDirection()
    {
        frameCounter = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Pac>().DecreaseHealth();
        }
        //else if (col.gameObject.tag == "Wall")
        //{
        //    ChangeDirection();
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Weapon")
        {
            DecreaseHealth();
        }
    }

    public override void DecreaseHealth()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            DestroyEffect();
            Destroy(gameObject);
        }
    }

    public void DestroyEffect()
    {
        var main = destroyEffect.GetComponent<ParticleSystem>().main;

        if (gameObject.name == "gRed")
        {
            main.startColor = new Color(1, 0, 0, 0.7F);
        }
        else if (gameObject.name == "gCyan")
        {
            main.startColor = new Color(0, 1, 1, 0.7F);
        }
        else if (gameObject.name == "gPink")
        {
            main.startColor = new Color(1, 0.4F, 1, 0.7F);
        }
        else if (gameObject.name == "gOrange")
        {
            main.startColor = new Color(1, 0.6F, 0, 0.7F);
        }
        else if (gameObject.name == "gViolet")
        {
            main.startColor = new Color(0.5F, 0, 1, 0.7F);
        }


        Instantiate(destroyEffect, transform.position, transform.rotation);
    }


}
