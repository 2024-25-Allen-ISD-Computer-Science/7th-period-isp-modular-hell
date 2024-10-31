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
                character.MoveX(-10);
            }
            if (pressedKeys.Contains(Keys.Right)) {
            System.Console.WriteLine("Right");
            character.MoveX(10);
            }
            if (pressedKeys.Contains(Keys.Down)) {
            System.Console.WriteLine("Down");
            character.MoveY(10);
            }
            if (pressedKeys.Contains(Keys.Up)) {
            System.Console.WriteLine("Up");
            character.MoveY(-10);
            }
        }

    };
};