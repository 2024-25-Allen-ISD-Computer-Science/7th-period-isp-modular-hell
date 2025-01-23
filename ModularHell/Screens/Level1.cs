using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;

namespace ModularHell
{
    public class Level1 : GameScreen
    {
        private Texture2D ballTexture;
        public string texturePath;

        private XmlManager<Entity> xmlEntityManager;
        Entity Player1;
        [XmlIgnore]
        public Camera Camera1;

        public Level1() {
            xmlEntityManager = new XmlManager<Entity>();
            Camera1 = new Camera(new Vector2(0,0), 1);
        }
        public override void LoadContent()
        {
            base.LoadContent();
            ballTexture = Content.Load<Texture2D>(texturePath);


            xmlEntityManager.Type = typeof(Character);
            Player1 = xmlEntityManager.Load($"Entity/Load/Player1.xml");
            Player1.LoadContent();
            Camera1.Position = Player1._position;
        }

        public override void UnloadContent()
        {
            Player1.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            
            if (InputHandler.HoldingKey(Keys.K)) {
                xmlEntityManager.Type = typeof(Character);
                xmlEntityManager.Save($"Entity/Load/Player1.xml", Player1);
            }
            
            Player1.Update(gameTime);
            Camera1.Position = Player1._position;
            Camera1.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(ballTexture, new Vector2(0,0), Color.Black);
            spriteBatch.Draw(ballTexture, Camera1.Position, Color.White);

            Vector2 camOffset = Camera1.Position;
            Player1.Draw(spriteBatch, camOffset);
        }

        public void Generate() {
            Player1 = new Character(1);
            Player1.texturePath = "Head";
            Player1.AttachmentSlots = new (EntityAttachment, int, Vector2)[1];
            Player1.Name = "Player1";
            Player1.Generate();
            Player1.LoadContent();
        }
    }
}
