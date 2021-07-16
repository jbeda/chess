namespace Chess
{
    public interface IBackend
    {
        IRenderer CreateRenderer();
        string Name { get; }
        Game Game { get; }
    }
}