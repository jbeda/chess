namespace Chess
{
    public interface IRenderer
    {
        IBackend Backend { get; }
        void Present();
        void Render(Board board);
        void ClearBuffer();
    }
}