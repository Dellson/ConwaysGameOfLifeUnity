using Assets.Backend.Scripts.Model;
using ConwaysGameOfLife.Backend.Actions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConwaysGameOfLife.Backend
{
    public class Core
    {
        public bool[][] Grid;
        private const int GRID_SIZE = 15;
        private IDraw drawer; 

        public Core(List<(int,int)> startingLiveCells = null)
        {
            //Grid = new bool[GRID_SIZE][];
            //drawer = new LogDraw();

            //for (int i = 0; i < GRID_SIZE; i++)
            //{
            //    Grid[i] = new bool[GRID_SIZE];
            //    for (int j = 0; j < GRID_SIZE; j++)
            //    {
            //        Grid[i][j] = startingLiveCells.Exists(cell => cell.Item1 == i && cell.Item2 == j);
            //    }
            //}
        }

        public void RunExample(int ticks)
        {
            //int sleep = 5;

            //for (int i = 0; i < ticks; i++)
            //{
            //    bool[][] gridCopy = new bool[GRID_SIZE][];
            //    for (int j = 0; j < GRID_SIZE; j++)
            //    {
            //        gridCopy[j] = new bool[GRID_SIZE];
            //        Array.Copy(Grid[j], gridCopy[j], GRID_SIZE);
            //    }
                
            //    Console.SetCursorPosition(10, 10);
            //    drawer.Draw(Grid);
            //    //////////////////
            //    gridCopy = Recalculate(gridCopy);

            //    for (int j = 0; j < GRID_SIZE; j++)
            //    {
            //        //gridCopy[j] = new bool[GRID_SIZE];
            //        Array.Copy(gridCopy[j], Grid[j], GRID_SIZE);
            //    }


            //    Thread.Sleep(sleep);
            //}
        }

        public Dictionary<(int, int), Cell> Recalculate(Dictionary<(int, int), Cell> gridOrigin)
        {
            //var grid = new Dictionary<(int, int), Cell>(gridOrigin);
            var resultGrid = new Dictionary<(int, int), Cell>();
            foreach (var key in gridOrigin.Keys)
            {
                resultGrid.Add(key, gridOrigin[key].GetCopyOfThis());
                var x = gridOrigin[key].X;
                var y = gridOrigin[key].Y;
                int neighbours = 0;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (GetCellState(gridOrigin, (x + i, y + j))) neighbours++;
                    }
                }
                if (gridOrigin[key].State) neighbours--;

                if (gridOrigin[key].State)
                {
                    if (neighbours < 2 || neighbours > 3)
                        resultGrid[key].State = false;
                }
                else
                {
                    if (neighbours == 3)
                        resultGrid[key].State = true;
                }
            }

            foreach (var key in resultGrid.Keys)
            {
                gridOrigin[key].State = resultGrid[key].State;
            }

            return gridOrigin;
        }
    
        /// <summary>
        /// Creates a new Cell if it does not exist
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="coords"></param>
        /// <returns></returns>
        private bool GetCellState(Dictionary<(int, int), Cell> grid, (int, int) coords)
        {
            if (!grid.ContainsKey(coords))
            {
                //grid.Add(coords, new Cell(coords, false));
                return false;
            }
            else if (grid[coords].State)
            {
                return true;
            }
            else
            {
                return false;
            }
            //return grid[coords].State;
        }
    }
}
