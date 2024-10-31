using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ModularHell 
{
    public class InputComponent
    {

        private InputState inputState = new InputState();
        private Keys[] pressedKeys;

        
        public void TestMethod()
        {
            Console.WriteLine("Hello");
        }
        
        public void Update(Character character)
        {
            pressedKeys = inputState.UpdatePressedKeys();
            

            if (pressedKeys.Contains(Keys.Left)) {
                System.Console.WriteLine("Left");
            }
        }

    };
};