namespace Chess
{
    public interface IRenderer
    {
        IBackend Backend { get; }
        void Present();
        // todo: add more methods, such as Render
    }
}