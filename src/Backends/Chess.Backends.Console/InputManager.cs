using System;
using System.Collections.Generic;

namespace Chess.Backends.Console
{
    internal class InputManager : IInputManager
    {
        public InputManager(ConsoleBackend backend)
        {
            this.mBackend = backend;
            this.ResetStates();
        }
        public KeyState this[Key key]
        {
            get
            {
                return this.GetKey(key);
            }
        }
        public KeyState GetKey(Key key)
        {
            return this.mKeyStates[key];
        }
        public void Update()
        {
            var keys = new List<ConsoleKey>();
            while (System.Console.KeyAvailable)
            {
                keys.Add(System.Console.ReadKey(true).Key);
            }
            var lastValues = this.ResetStates();
            foreach (var obj in Enum.GetValues(typeof(Key)))
            {
                if (obj is Key key)
                {
                    KeyState state = new KeyState();
                    foreach (ConsoleKey consoleKey in keys)
                    {
                        if (this.Convert(key) == consoleKey)
                        {
                            state.Held = true;
                            break;
                        }
                    }
                    KeyState lastState = lastValues[key];
                    state.Down = state.Held && !lastState.Held;
                    state.Up = !state.Held && lastState.Held;
                    this.mKeyStates[key] = state;
                }
            }
        }
        private Dictionary<Key, KeyState> ResetStates()
        {
            var originalValue = this.mKeyStates;
            this.mKeyStates = new();
            foreach (var obj in Enum.GetValues(typeof(Key)))
            {
                if (obj is Key key)
                {
                    this.mKeyStates[key] = KeyState.Default();
                }
            }
            return originalValue;
        }
        private ConsoleKey Convert(Key key)
        {
            foreach (var obj in Enum.GetValues(typeof(ConsoleKey)))
            {
                if (obj is ConsoleKey consoleKey)
                {
                    if (consoleKey.ToString() == key.ToString())
                    {
                        return consoleKey;
                    }
                }
            }
            throw new InvalidCastException();
        }
        public IBackend Backend { get { return this.mBackend; } }
        private ConsoleBackend mBackend;
        private Dictionary<Key, KeyState> mKeyStates;
    }
}