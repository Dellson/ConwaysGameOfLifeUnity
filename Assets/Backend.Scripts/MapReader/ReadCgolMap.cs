using System.IO;

namespace ConwaysGameOfLife.Assets.Backend.Scripts.MapReader
{
    public class ReadCgolMap : IMapReader
    {
        public string[] ReadMapFile(string mapName, char trueVal, char falseVal)
        {
            return File.ReadAllLines(
                Path.Combine(
                    Directory.GetCurrentDirectory(), 
                    "Assets\\Resources\\Maps", 
                    $"{mapName}.cgol"));
        }
    }
}
