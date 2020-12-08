using System;
using System.Collections.Generic;
using UnityEngine;

/* Last Edited 12/7/2020
 * Edited by Amp
 * 
 * Note - uppercase are properties while lowercase are fields
 */


public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// VARIABLES AND WHOT NOT
    /// </summary>
    /// 
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    private Point blueSpawn, redSpawn;

    [SerializeField]
    private GameObject bluePortalPrefab;

    [SerializeField]
    private GameObject redPortalPrefab;

    public Dictionary<Point, TileScript> Tiles { get; set; }

    //Access the width property of Tile1 via the spriterenderer component in Unity
    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    /// <summary>
    /// Start of the code I guess??
    /// </summary>
    /// 

    // Start is called before the first frame update
    void Start()
    {

        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This is where the fun begins
    /// </summary>
    /// 

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();


        // Note: lines are indicators of when to start a new row (in level txt file)
        string[] mapData = ReadLevelText();

        //Determines the number of X and Y tiles on map
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        //Place across y axis
        for (int y = 0; y < mapY; y++)
        {
            //Stores every single character in string
            char[] newTiles = mapData[y].ToCharArray();

            //Place across x axis
            for (int x = 0; x < mapX; x++)
            {
                //This actually places the tile
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }

        //Take the last position in the map and store it into the next tile
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y -TileSize));

        SpawnPortals();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        // tileType is being read in from txt document, and it is then parsed into a int file for instantiation
        int tileIndex = int.Parse(tileType);

        // Creates Tile
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        // Assigns a point according to current x and y position to the newly placed tile

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));

        Tiles.Add(new Point(x,y),newTile); 


    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        //Converts string block into a single string line
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        //Converts string line to segments that end when they encounter the dash
        return data.Split('-');
    }

    private void SpawnPortals()
    {
        blueSpawn = new Point(0, 0);

       Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);

        redSpawn = new Point(11, 6);

        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }
}
