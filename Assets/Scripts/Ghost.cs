using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NesScripts.Controls.PathFind;

public class Ghost : Character {
    public GameManager gameMenager;
    public GameObject destroyEffect;
    GameObject Pac;
    TilesCounter tilesCounter;

    NesScripts.Controls.PathFind.Grid grid;
    public List<Point> path;

    public List<Vector3> pathWorldPos;
    public List<Vector3> __tempPathWorldPos;

    public Tilemap tileMap;
    int tileMapWidth;
    int tileMapHeight;

    bool[,] tileArray;

    public Vector3Int cellPositionGhost;
    public Vector3Int cellPositionTarget;

    bool currentTarget;
    bool findPac = true;
    bool findRandom = false;
    int randomCounterHowLong = 1000;
    int randomCounterBreak;
    int randomIndex;

    public bool canFindPath = true;
    public float differenceMagnitude;
    public int firstStep = 0;
    public bool canMove = false;

    public override void Start()
    {
        base.Start();
        maxHealth = 1;
        currentHealth = maxHealth;
        speed = 1.2F;

        gameMenager = GameObject.Find("Canvas").GetComponent<GameManager>();

        tilesCounter = FindObjectOfType<Tilemap>().GetComponent<TilesCounter>();
        tileMap = tilesCounter.GetComponentInParent<Tilemap>();
        tileMapWidth = -tileMap.cellBounds.xMin + tileMap.cellBounds.xMax;
        tileMapHeight = -tileMap.cellBounds.yMin + tileMap.cellBounds.yMax;

        Pac = GameObject.FindGameObjectWithTag("Player");

        currentTarget = findPac;
        randomCounterBreak = Random.Range(100, 200);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Movement();
        }
        
    }

    public override void Movement()
    {
        // after random time find path to random places on the map and stop following player
        if (canFindPath == true)
        {
            if (randomCounterBreak == 0)
            {
                randomCounterBreak = 1000;

                randomCounterHowLong = Random.Range(100, 200);
                currentTarget = findRandom;     
            } 

            if (randomCounterHowLong == 0)
            {
                randomCounterHowLong = 1000;

                randomCounterBreak = Random.Range(200, 500);
                currentTarget = findPac;
            }

            FindPath(currentTarget);
            canFindPath = false;
        }
        //find paths allows only after meeting target cell
        else
        {
            if (cellPositionGhost == cellPositionTarget)
            {
                canFindPath = true;
            }
        }

        if (pathWorldPos.Count != 0)
        {
            var difference = pathWorldPos[firstStep] - transform.position;
            differenceMagnitude = difference.magnitude;

            //check difference between ghost position and first cell in the path, then move or find new path
            if (differenceMagnitude > 0.05F)
            {
                Vector3 direction = (pathWorldPos[firstStep] - transform.position).normalized;
                GetComponent<Rigidbody2D>().MovePosition(transform.position + direction * speed * Time.deltaTime);
                SetAnimation(direction);
            }
            else
            {
                canFindPath = true;
            }
        }

        if (randomCounterBreak > 0) randomCounterBreak--;
        if (randomCounterHowLong > 0) randomCounterHowLong--;
    }

    //find the nearest path to targeted cell in the map, downloaded scripts used for that, check 2dTleBasedPathFinding folder
    void FindPath(bool findPac)
    {
        tileArray = tilesCounter.GetTileArrays();
        grid = new NesScripts.Controls.PathFind.Grid(tileMapWidth, tileMapHeight, tileArray);

        cellPositionGhost = tileMap.WorldToCell(transform.position);

        if (findPac == true)
        {
            cellPositionTarget = tileMap.WorldToCell(Pac.transform.position);
        } else
        {
            if (randomCounterBreak == 1000 )
            {
                randomIndex = Random.Range(0, tilesCounter.availablePlaces.Count);
            }
            cellPositionTarget = tileMap.WorldToCell(tilesCounter.availablePlaces[randomIndex]);
        }

        path = Pathfinding.FindPath(grid, tilesCounter.LocalGridToPathGrid(cellPositionGhost), tilesCounter.LocalGridToPathGrid(cellPositionTarget));
        __tempPathWorldPos.Clear();

        foreach (var cell in path)
        {
            var localCell = tilesCounter.PathGridToLocalGrid(cell);
            __tempPathWorldPos.Add(tileMap.GetCellCenterWorld(localCell));
        }
        pathWorldPos = __tempPathWorldPos;
    }

    void SetAnimation(Vector3 dir)
    {
        if (dir.x >0.95 && dir.x < 1.05)
        {
            anim.SetInteger(directionVariable, (int)direction.RIGHT);
        }
        else if (dir.x < -0.95 && dir.x > -1.05)
        {
            anim.SetInteger(directionVariable, (int)direction.LEFT);
        }
        else if (dir.y > 0.95 && dir.y < 1.05)
        {
            anim.SetInteger(directionVariable, (int)direction.UP);
        }
        else if (dir.y < -0.95 && dir.y > -1.05)
        {
            anim.SetInteger(directionVariable, (int)direction.DOWN);
        }
    }

    public void IncreaseSpeed()
    {
        speed += 0.1F;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        // set random target to follow after collision with Pac
        if (col.gameObject.tag == "Player")
        {
            randomCounterBreak = 0;
        }
        // collisions between ghosts disabled
        else if (col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), col.collider);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // kill ghost after meeting the sword
        if (col.gameObject.tag == "Weapon")
        {
            DecreaseHealth();
            Pac.GetComponent<Pac>().canAttack = false;
            col.GetComponent<BoxCollider2D>().enabled = false;
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
