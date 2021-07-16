namespace Chess
{
    public sealed class Game
    {
        public Game()
        {
            this.Log = new Log();
            this.Log.Print("Initializing chess...");
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
            // todo: update
        }
        private void Render()
        {
            // todo: render
        }
        public IBackend Backend { get; private set; }
        public Log Log { get; private set; }
        private bool mRunning;
    }
}