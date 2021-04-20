using UnityEngine;
using System.Collections.Generic;
using Assets.Backend.Scripts.Model;

public class BoardManager : MonoBehaviour
{
    public GameObject TileTemplate;
    public Sprite AliveTile;
    public Sprite DeadTile;
    private Dictionary<(int, int), Cell> Items;
    private MapParser _parser;

    public void Start()
    {
        MapSetup("level");
    }

    public void Update()
    {
        Debug.Log("tick");
    }

    public void MapSetup(string levelName)
    {
        _parser = new MapParser(levelName);

        var board = _parser.GetCellBoard();

        for (int x = _parser.Eastmost; x < _parser.Westmost; ++x)
        {
            for (int y = _parser.Southmost; y < _parser.Northmost; ++y)
            {
                var cell = board[(x, y)];
                GameObject tileGameObject = CreateTile(cell);

                tileGameObject.transform
                    .SetParent(transform, false);
            }
        }
    }

    public GameObject CreateTile(Cell cell)
    {
        GameObject newCell = Instantiate(TileTemplate, cell.Coordinates, Quaternion.identity);

        newCell.name = $"Tile({cell.X})-({cell.Y})";
        cell.ChangeImage(newCell, "Tile");

        return newCell;
    }
}
