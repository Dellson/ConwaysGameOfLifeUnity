using UnityEngine;
using System.Collections.Generic;
using Assets.Backend.Scripts.Model;
using ConwaysGameOfLife.Backend;

public class BoardManager : MonoBehaviour
{
    static int ticks = 0;
    public GameObject TileTemplate;
    public Sprite AliveTile;
    public Sprite DeadTile;
    private Dictionary<(int, int), Cell> Items;
    private MapParser _parser;
    private Core _core;

    public void Start()
    {
        _core = new Core();
        _parser = new MapParser("level_1");
        Items = _parser.GetCellBoard();

        MapSetup("level_1");
    }

    public void Update()
    {
        ticks++;

        if (ticks == 200)
        {
            _core.Recalculate(Items);

            foreach (var key in Items.Keys)
            {
                var cell = Items[key];
                cell.ChangeImage(cell.State);
            }

            ticks = 0;
        }
    }

    public void MapSetup(string levelName)
    {
        for (int x = 0; x < _parser.MapDimensions.rows; ++x)
        {
            for (int y = 0; y < _parser.MapDimensions.columns; ++y)
            {
                var cell = Items[(x, y)];

                GameObject tileGameObject = CreateTile(cell);

                tileGameObject.transform
                    .SetParent(transform, false);
                //if (cell.State)
                //{
                //    GameObject tileGameObject = CreateTile(cell);

                //    tileGameObject.transform
                //        .SetParent(transform, false);

                //}
            }
        }
    }

    public GameObject CreateTile(Cell cell)
    {
        GameObject newCell = Instantiate(TileTemplate, cell.Coordinates, Quaternion.identity);

        cell.thisObject = newCell;
        newCell.name = $"Tile({cell.X})-({cell.Y})";
        cell.ChangeImage(cell.State);
        return newCell;
    }
}
