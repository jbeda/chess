using System;

namespace Chess {
    public enum Backend {
        CONSOLE // todo: add gui option later
    }
    public class Game {
        public Game(Backend backend = Backend.CONSOLE) {
            this.Backend = backend;
        }
        public void Run() {
            // todo: game loop
            throw new NotImplementedException();
        }
        public Backend Backend { get; private set; }
    }
}