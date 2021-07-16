using System;

namespace Chess {
    public sealed class Game
    {
        public Game()
        {
            this.Log = new Log();
            this.Log.Print("Initializing chess...");
            this.Backend = null;
            this.Renderer = null;
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
            this.Renderer = this.Backend.CreateRenderer();
            if (this.Renderer == null)
            {
                this.Log.Print("Failed to create renderer!");
                return;
            }
            // todo: game loop
            this.Log.WriteLog();
        }
        public IBackend Backend { get; private set; }
        public IRenderer Renderer { get; private set; }
        public Log Log { get; private set; }
    }
}