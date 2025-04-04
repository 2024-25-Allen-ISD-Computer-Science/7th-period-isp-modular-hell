using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;
using Microsoft.VisualBasic;

namespace ModularHell
{
    public class Level1 : GameScreen
    {
        private Texture2D ballTexture;
        public string texturePath;

        private XmlManager<Entity> xmlEntityManager;
        Entity Player1;

        //temporary until we need to do more than one enemy
        Entity Enemy;

        [XmlIgnore]
        public SpriteFont fontArial;
        public Level1() {
            xmlEntityManager = new XmlManager<Entity>();
            Camera1 = new Camera(Vector2.Zero, 1f);
            PlayMap = new Map();
        }
        public override void LoadContent()
        {
            base.LoadContent();
            ballTexture = Content.Load<Texture2D>(texturePath);


            xmlEntityManager.Type = typeof(Character);
            Player1 = xmlEntityManager.Load($"Entity/Load/Player1.xml");
            Player1.LoadContent();
            Camera1.Position = Player1._position + new Vector2(Player1.Dimensions.X * Camera1.Scale / 2, Player1.Dimensions.Y * Camera1.Scale);

            xmlEntityManager.Type = typeof(Enemy);
            Enemy = xmlEntityManager.Load($"Entity/Load/Enemy.xml");
            Enemy.LoadContent();

            fontArial = Content.Load<SpriteFont>("Arial");
        }

        public override void UnloadContent()
        {
            Enemy.UnloadContent();
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
            
            Player1.Update(gameTime, ref PlayMap.collisionMap);
            Camera1.Position = Player1._position + new Vector2(Player1.Dimensions.X * Camera1.Scale / 2, Player1.Dimensions.Y * Camera1.Scale);
            Enemy.Update(gameTime, ref PlayMap.collisionMap);
            Camera1.Update();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);

            //spriteBatch.Draw(ballTexture, ScreenManager.Instance.MiddleScreen, Color.White);

            Vector2 camPos = Camera1.Position;

            if (Player1._position.Y <= Enemy._position.Y) {
                Player1.Draw(spriteBatch, gameTime, ref Camera1);
                Enemy.Draw(spriteBatch, gameTime, ref Camera1);
            } else {
                Enemy.Draw(spriteBatch, gameTime, ref Camera1);
                Player1.Draw(spriteBatch, gameTime, ref Camera1);
            }

            if (ScreenManager.Instance.Debug) {
                spriteBatch.DrawString(fontArial, Player1._position.ToString(), Vector2.Zero, Color.LightGreen);
            }
        }

        public override void Generate() {

            /*        
            Player1 = new Character(1);
            Player1.texturePath = "Head";
            Player1.AttachmentSlots = new (EntityAttachment, int, Vector2)[1];
            Player1.Name = "Player1";
            Player1.Generate();
            Player1.LoadContent();
            */
            
            Enemy = new Enemy();
            Enemy.texturePath = "EvilHead";
            Enemy.AttachmentSlots = new (EntityAttachment, int, Vector2)[1];
            Enemy.Name = "EvilEntity";
            Enemy.Generate();
            Enemy.LoadContent();
            
        }
    }
}
