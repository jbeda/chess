namespace Chess
{
    public interface IRenderer
    {
        IFrontend Frontend { get; }
        void Present();
        void Render(Board board);
        void ClearBuffer();
    }
}