using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NVorbis;

namespace ModularHell 
{
    public class InputState
    {
        private Keys[] pressedKeys;
        protected KeyboardState keyboardState;

        public Keys[] UpdatePressedKeys() {
            keyboardState = Keyboard.GetState();

            pressedKeys = keyboardState.GetPressedKeys();
            return pressedKeys;
        }
        
    }

}