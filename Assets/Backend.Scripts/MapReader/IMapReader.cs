namespace ConwaysGameOfLife.Assets.Backend.Scripts.MapReader
{
    public interface IMapReader
    {
        public string[] ReadMapFile(string mapName, char trueVal, char falseVal);
    }
}
