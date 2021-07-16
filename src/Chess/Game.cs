namespace Chess
{
    public sealed class Game
    {
        public Game()
        {
            Log = new Log();
            Log.Print("Initializing chess...");
            Board = new Board(this);
            Backend = null;
            mRunning = true;
        }
        public void SetBackend(IBackend backend)
        {
            Backend = backend;
            Log.Print(string.Format("Backend: {0}", Backend.Name));
        }
        public void Run()
        {
            if (Backend == null)
            {
                Log.Print("Chess cannot run without a backend!");
                return;
            }
            while (mRunning)
            {
                Update();
                Render();
            }
            Log.WriteLog();
        }
        public void Quit()
        {
            mRunning = false;
        }
        private void Update()
        {
            Backend.Update();
            if (Backend.InputManager[Key.Q].Down)
            {
                Quit();
            }
            // todo: update
        }
        private void Render()
        {
            IRenderer renderer = Backend.Renderer;
            renderer.ClearBuffer();
            renderer.Render(Board);
            renderer.Present();
        }
        public IBackend Backend { get; private set; }
        public Log Log { get; private set; }
        public Board Board { get; private set; }
        private bool mRunning;
    }
}