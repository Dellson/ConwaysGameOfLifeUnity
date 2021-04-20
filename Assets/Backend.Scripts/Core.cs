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

        public Core(List<(int,int)> startingLiveCells)
        {
            Grid = new bool[GRID_SIZE][];
            drawer = new LogDraw();

            for (int i = 0; i < GRID_SIZE; i++)
            {
                Grid[i] = new bool[GRID_SIZE];
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    Grid[i][j] = startingLiveCells.Exists(cell => cell.Item1 == i && cell.Item2 == j);
                }
            }
        }

        public void RunExample(int ticks)
        {
            int sleep = 5;

            for (int i = 0; i < ticks; i++)
            {
                bool[][] gridCopy = new bool[GRID_SIZE][];
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    gridCopy[j] = new bool[GRID_SIZE];
                    Array.Copy(Grid[j], gridCopy[j], GRID_SIZE);
                }
                
                Console.SetCursorPosition(10, 10);
                drawer.Draw(Grid);
                //////////////////
                gridCopy = Recalculate(gridCopy);

                for (int j = 0; j < GRID_SIZE; j++)
                {
                    //gridCopy[j] = new bool[GRID_SIZE];
                    Array.Copy(gridCopy[j], Grid[j], GRID_SIZE);
                }


                Thread.Sleep(sleep);
            }
        }

        private bool[][] Recalculate(bool[][] gridCopy)
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    int neighbours = 0;
                    
                    if (i != 0 && Grid[i - 1][j]) neighbours++;
                    if (i != GRID_SIZE - 1 && Grid[i + 1][j]) neighbours++;

                    if (j != 0 && Grid[i][j - 1]) neighbours++;
                    if (j != GRID_SIZE - 1 && Grid[i][j + 1]) neighbours++;
                    
                    if (i != 0 && j != 0 && Grid[i - 1][j - 1]) neighbours++;
                    if (i != GRID_SIZE - 1 && j != GRID_SIZE - 1 && Grid[i + 1][j + 1]) neighbours++;

                    if (i != 0 && j != GRID_SIZE - 1 && Grid[i - 1][j + 1]) neighbours++;
                    if (i != GRID_SIZE - 1 && j != 0 && Grid[i + 1][j - 1]) neighbours++;

                    if (Grid[i][j])
                    {
                        if (i == 1 && j == 2)
                        {
                            gridCopy[i][j] = false;
                        }
                        if (neighbours < 2) gridCopy[i][j] = false;
                        if (neighbours > 3) gridCopy[i][j] = false;
                        //if (neighbours == 2 || neighbours == 3) gridCopy[i][j] = true;
                    }
                    else
                    {
                        if (neighbours == 3) gridCopy[i][j] = true;
                    }
                }
            }
            return gridCopy;
        }
    }
}
