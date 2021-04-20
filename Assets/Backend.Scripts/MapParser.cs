//using Assets.Scripts.Models;
//using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Backend.Scripts.Model;

/// <summary>
/// Converts data under given path into two-dimensional array matching given converting key
/// </summary>
public class MapParser
{
    private readonly int ColumnsCount;
    private readonly int RowsCount;
    private readonly string[] DataToParse;
    private string Regex;// = "\\d{15}";
    //private readonly MapConfig MapConfig;
    private const int tilePixelSize = 64;
    private Dictionary<(int, int), Cell> Items;
    private const bool dead = false;
    private const bool alive = true;

    /// <summary>
    /// getters below work on currently "parsed" content
    /// </summary>
    public int Northmost { get; private set; } = 0;
    public int Eastmost { get; private set; } = 0;
    public int Southmost { get; private set; } = 0;
    public int Westmost { get; private set; } = 0;

    /// <summary>
    /// Left upper corner - offset of X axis
    /// </summary>
    private int OffsetX
    {
        get
        {
            return -((ColumnsCount / 2) * tilePixelSize) + (tilePixelSize / 2);
        }
    }

    /// <summary>
    /// Left upper corner - offset of Y axis
    /// </summary>
    private int OffsetY
    {
        get
        {
            return ((RowsCount / 2) * tilePixelSize) - (tilePixelSize / 2);
        }
    }

    /// <summary>
    /// Map parser constructor
    /// </summary>
    /// <param name="mapName">Name of the configuration file to be loaded</param>
    public MapParser(string mapName)
    {
        // "global" var wannabe?
        string resourcesDirectory = Path.Combine(
            Directory.GetCurrentDirectory(), "Assets\\Resources\\Maps");

        string[] mapsFilesDirectories = Directory.GetFiles(resourcesDirectory, "*.cgo");

        //TextAsset configTextAsset = Resources.Load<TextAsset>("Maps\\Legend");
        //MapConfig = JsonConvert.DeserializeObject<MapConfig>(configTextAsset.text);

        DataToParse = File.ReadAllLines(
            Path.Combine(mapsFilesDirectories[0]));

        Regex = "\\d{15}";
        ColumnsCount = MapDimensions.columns;
        RowsCount = MapDimensions.rows;

        GetCellBoard();
        //Regex = MapConfig.TileParser;
    }

    /// <summary>
    /// Parses current data. ParsedData variable must be up to date.
    /// </summary>
    /// <param name="dataToParse">Data to be parsed. Data needs to be in form of an array of rows</param>
    /// <returns>ParsedData two-dimensional array</returns>
    public Dictionary<(int, int), Cell> GetCellBoard()
    {
        Eastmost = DataToParse.Length;
        Eastmost = DataToParse[0].Length;
        Southmost = 0;
        Westmost = 0;
        Items = new Dictionary<(int, int), Cell>();

        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < ColumnsCount; j++)
            {
                // x stands for ALIVE
                bool state = false;

                if (DataToParse[i][j] == '.')
                    state = dead;
                else if (DataToParse[i][j] == 'x')
                    state = alive;

                var cell = new Cell((i, j), state);
                Items.Add((i, j), cell);
            }
        }

        return Items;
    }

    private (int columns, int rows) MapDimensions => (DataToParse[0].Length, DataToParse.Length);
}
