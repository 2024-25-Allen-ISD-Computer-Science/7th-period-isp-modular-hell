using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ModularHell 
{
    public class Character
    {

        public string name;
        public int velocity;
        public Vector2 Position;
        
        private InputComponent inputComponent = new InputComponent();
        
        private EntityAttachment slot1 = new EntityAttachment();

        public Character() {
            Position = new Vector2(0,0);
        }
        
        public void Update() {
            // fix error here 
            this.inputComponent.Update(this);

        }

        public void MoveX(int distance) {
            Position.X += distance;
            System.Console.WriteLine("character is at " + Position.X);
        }

        public void MoveY(int distance) {
            Position.Y += distance;
            System.Console.WriteLine("character is at " + Position.Y);
        }
    };
};