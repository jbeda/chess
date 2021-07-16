using System;
using System.Collections.Generic;

namespace Chess.Backends.Console
{
    internal class InputManager : IInputManager
    {
        public InputManager(ConsoleBackend backend)
        {
            mBackend = backend;
            ResetStates();
        }
        public KeyState this[Key key]
        {
            get
            {
                return GetKey(key);
            }
        }
        public KeyState GetKey(Key key)
        {
            return mKeyStates[key];
        }
        public void Update()
        {
            var keys = new List<ConsoleKey>();
            while (System.Console.KeyAvailable)
            {
                keys.Add(System.Console.ReadKey(true).Key);
            }
            var lastValues = ResetStates();
            foreach (var obj in Enum.GetValues(typeof(Key)))
            {
                if (obj is Key key)
                {
                    var state = new KeyState();
                    foreach (ConsoleKey consoleKey in keys)
                    {
                        if (Convert(key) == consoleKey)
                        {
                            state.Held = true;
                            break;
                        }
                    }
                    KeyState lastState = lastValues[key];
                    state.Down = state.Held && !lastState.Held;
                    state.Up = !state.Held && lastState.Held;
                    mKeyStates[key] = state;
                }
            }
        }
        private Dictionary<Key, KeyState> ResetStates()
        {
            var originalValue = mKeyStates;
            mKeyStates = new();
            foreach (var obj in Enum.GetValues(typeof(Key)))
            {
                if (obj is Key key)
                {
                    mKeyStates[key] = KeyState.Default();
                }
            }
            return originalValue;
        }
        private static ConsoleKey Convert(Key key)
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
        public IBackend Backend { get { return mBackend; } }
        private readonly ConsoleBackend mBackend;
        private Dictionary<Key, KeyState> mKeyStates;
    }
}