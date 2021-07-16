namespace Chess
{
    public sealed class Game
    {
        public Game()
        {
            Log = new Log();
            Log.Print("Initializing chess...");
            Board = new Board(this);
            Frontend = null;
            mRunning = true;
        }
        public void SetFrontend(IFrontend frontend)
        {
            Frontend = frontend;
            Log.Print(string.Format("Frontend: {0}", Frontend.Name));
        }
        public void Run()
        {
            if (Frontend == null)
            {
                Log.Print("Chess cannot run without a frontend!");
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
            Frontend.Update();
            if (Frontend.InputManager[Key.Q].Down)
            {
                Quit();
            }
            // todo: update
        }
        private void Render()
        {
            IRenderer renderer = Frontend.Renderer;
            renderer.ClearBuffer();
            renderer.Render(Board);
            renderer.Present();
        }
        public IFrontend Frontend { get; private set; }
        public Log Log { get; private set; }
        public Board Board { get; private set; }
        private bool mRunning;
    }
}