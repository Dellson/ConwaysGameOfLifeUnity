using System.Text;
using UnityEngine;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.MapReader
{
    public class ReadPngMap : IMapReader
    {
        Texture2D map;

        public ReadPngMap(string mapName)
        {
            map = Resources.Load<Texture2D>($"Maps\\{mapName}");
        }
        public int GetMapHeight() => map.height;
        public int GetMapWidth() => map.width;

        public string[] ReadMapFile(string mapName, char trueVal, char falseVal)
        {
            //var map = Resources.Load<Texture2D>($"Maps\\{mapName}");
            var Output = new string[map.width];

            for (int column = 0; column < map.width; column++)
            {
                var sb = new StringBuilder();
                for (int row = 0; row < map.height; row++)
                {
                    var color = map.GetPixel(column, row);
                    sb.Append(
                        color.maxColorComponent == 0f ? trueVal : falseVal);
                }
                Output[column] = sb.ToString();
            }

            return Output;
        }
    }
}
