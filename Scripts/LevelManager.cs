using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/* Last Edited 12/2/2020
 * Edited by Amp
 * 
 * Note - uppercase are properties while lowercase are fields
 */


public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    public float TileSize
    {
        //Access the width property of Tile1 via the spriterenderer component in Unity
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update
    void Start()
    {

        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel()
    {
        // Note: lines are indicators of when to start a new row (in level txt file)
        string[] mapData = ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        //Place across y axis
        for (int y = 0; y < mapY; y++)
        {
            //Stores every single character in string
            char[] newTiles = mapData[y].ToCharArray();

            //Place across x axis
            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        // tileType is being read in from txt document, and it is then parsed into a int file for instantiation
        int tileIndex = int.Parse(tileType);

        // Creates Tile
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);

        // Changes position of new tile to one equal the next empty position of last one placed
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        //Converts string block into a single string line
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        //Converts string line to segments that end when they encounter the dash
        return data.Split('-');
    }
}
