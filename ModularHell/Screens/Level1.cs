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
        public string texturePath;

        private XmlManager<Entity> xmlEntityManager;
        Entity Character;
        

        public Level1() {
            xmlEntityManager = new XmlManager<Entity>();
        }
        public override void LoadContent()
        {
            base.LoadContent();
            ballTexture = Content.Load<Texture2D>(texturePath);


            xmlEntityManager.Type = typeof(Character);
            Character = xmlEntityManager.Load("Entity/Load/Character.xml");
            Character.Name = "Player1";
            ///Character.Generate();
            Character.LoadContent();
        }

        public override void UnloadContent()
        {
            Character.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            /*
            Force Unloader
            if (InputHandler.HoldingKey(Keys.K)) {
                Character.UnloadContent();
            }
            */

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
