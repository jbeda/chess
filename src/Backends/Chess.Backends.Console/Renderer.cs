namespace Chess.Backends.Console
{
    internal sealed class Renderer : IRenderer
    {
        public Renderer(ConsoleBackend backend)
        {
            this.mBackend = backend;
        }
        public void Present()
        {
            // todo: present text
        }
        public void Render(Board board)
        {
            // todo: render board
        }
        public void ClearBuffer()
        {
            // todo: clear buffer
        }
        public IBackend Backend { get { return this.mBackend; } }
        private readonly ConsoleBackend mBackend;
    }
}