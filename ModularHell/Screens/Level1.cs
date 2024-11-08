using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell
{
    public class Level1 : GameScreen
    {
        private Texture2D ballTexture;
        public string path;

        Entity Character;
        
        public override void LoadContent()
        {
            base.LoadContent();
            ballTexture = Content.Load<Texture2D>(path);

            Character = new Character();
            Character.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Content.Unload();

            Character.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Character.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(ballTexture, new Vector2(0,0), Color.White);

            Character.Draw(spriteBatch);
        }
    }
}
