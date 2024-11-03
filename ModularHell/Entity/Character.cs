using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//remove later
using Microsoft.Xna.Framework.Content;

namespace ModularHell 
{
    public class Character : Entity
    {
        private Texture2D characterTexture;

        public Vector2 Position;
        
        private InputComponent inputComponent = new InputComponent();
        
        private EntityAttachment slot1 = new EntityAttachment();

        //remove later
        protected ContentManager content;

        public Character() {
            Position = new Vector2(0,0);
        }
        
        public override void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            characterTexture = content.Load<Texture2D>("CharacterHead1");
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            // fix error here 
            this.inputComponent.Update(this);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(characterTexture, this.Position, Color.White);
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