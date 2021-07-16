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
        public IBackend Backend { get { return this.mBackend; } }
        private ConsoleBackend mBackend;
    }
}