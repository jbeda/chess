namespace Chess
{
    public sealed class Game
    {
        public Game()
        {
            this.Log = new Log();
            this.Log.Print("Initializing chess...");
            this.Board = new Board(this);
            this.Backend = null;
            this.mRunning = true;
        }
        public void SetBackend(IBackend backend)
        {
            this.Backend = backend;
            this.Log.Print(string.Format("Backend: {0}", this.Backend.Name));
        }
        public void Run()
        {
            if (this.Backend == null)
            {
                this.Log.Print("Chess cannot run without a backend!");
                return;
            }
            while (this.mRunning)
            {
                this.Update();
                this.Render();
            }
            this.Log.WriteLog();
        }
        public void Quit()
        {
            this.mRunning = false;
        }
        private void Update()
        {
            this.Backend.Update();
            if (this.Backend.InputManager[Key.Q].Down)
            {
                this.Quit();
            }
            // todo: update
        }
        private void Render()
        {
            IRenderer renderer = this.Backend.Renderer;
            renderer.ClearBuffer();
            renderer.Render(this.Board);
            renderer.Present();
        }
        public IBackend Backend { get; private set; }
        public Log Log { get; private set; }
        public Board Board { get; private set; }
        private bool mRunning;
    }
}