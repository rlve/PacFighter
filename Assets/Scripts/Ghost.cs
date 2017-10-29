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

    public List<Vector3> pathWorldPos;
    public List<Vector3> __tempPathWorldPos;
    public List<Vector3> currentPathWorldPos;
    public List<Vector3> newPathWorldPos;

    public Tilemap tileMap;
    int tileMapWidth;
    int tileMapHeight;

    bool[,] tileArray;

    public Vector3Int cellPositionGhost;
    public Vector3Int cellPositionPac;


    public Vector3 currentPathWorld;
    public Vector3 currentGhost;
    public Vector3 currentDir;
    public bool canFindPath = true;
    public float differenceMagnitude;

    public int current = 0;

    public GameObject destroyEffect;

    GameObject Pac;

    public override void Start()
    {
        base.Start();
        maxHealth = 1;
        currentHealth = maxHealth;
        speed = 0.3F;

        tilesCounter = FindObjectOfType<Tilemap>().GetComponent<TilesCounter>();
        

        //tileMap = tilesCounter.GetComponentInParent<Tilemap>();
        tileMapWidth = -tileMap.cellBounds.xMin + tileMap.cellBounds.xMax;
        tileMapHeight = -tileMap.cellBounds.yMin + tileMap.cellBounds.yMax;


        Pac = GameObject.FindGameObjectWithTag("Player");
    }


    public override void Update()
    {
        
        
    }

    List<Vector3> FindPath()
    {
        tileArray = tilesCounter.GetTileArrays();
        grid = new NesScripts.Controls.PathFind.Grid(tileMapWidth, tileMapHeight, tileArray);

        cellPositionPac = tileMap.WorldToCell(Pac.transform.position);
        cellPositionGhost = tileMap.WorldToCell(transform.position);

        path = Pathfinding.FindPath(grid, tilesCounter.LocalGridToPathGrid(cellPositionGhost), tilesCounter.LocalGridToPathGrid(cellPositionPac));

        __tempPathWorldPos.Clear();

        foreach (var cell in path)
        {
            var localCell = tilesCounter.PathGridToLocalGrid(cell);
            __tempPathWorldPos.Add(tileMap.GetCellCenterWorld(localCell));
        }

        pathWorldPos = __tempPathWorldPos;
        return pathWorldPos;
    }

    private void FixedUpdate()
    {
        
        Movement();
    }

    public override void Movement()
    {
        if (canFindPath == true)
        {
            FindPath();
            canFindPath = false;
        }

        if (pathWorldPos.Count != 0)
        {
            var difference = pathWorldPos[current] - transform.position;
            differenceMagnitude = difference.magnitude;

            if (differenceMagnitude > 0.005F)
            {
                Vector3 direction = (pathWorldPos[current] - transform.position).normalized;
                currentDir = direction;


                GetComponent<Rigidbody2D>().MovePosition(transform.position + direction * speed * Time.deltaTime);
            }
            else
            {
                current = (current + 1) % pathWorldPos.Count;
            }
        }


        if (tileMap.WorldToCell(Pac.transform.position) == tileMap.WorldToCell(transform.position))
        {
            canFindPath = true;
            current = 0;
        }

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
