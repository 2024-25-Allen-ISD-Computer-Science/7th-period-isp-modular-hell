using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace ModularHell
{
    public class ScreenManager 
    {
        public bool Debug = true; 
        GameScreen currentScreen;
        XmlManager<GameScreen> xmlGameScreenManager;
        public ContentManager Content {private set; get;}
        public Vector2 Dimensions {private set; get;}
        public Vector2 MiddleScreen {private set; get;}

        public SpriteFont fontArial;

        //The following makes this class a "Singleton"
        //Only one of these objects can ever be made and can be called without initialization (like a static class)
        //but can be passed as a parameter in functions (unlike a static class)
        private static ScreenManager instance;

        public static ScreenManager Instance
        {
            get 
            {
                if (instance == null)
                    instance = new ScreenManager();

                    return instance;
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(640, 640);
            MiddleScreen = Vector2.Divide(Dimensions,2);
            
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = typeof(Level1);
            currentScreen = xmlGameScreenManager.Load("Screens/Load/Level1.xml");
        }
        public void LoadContent(ContentManager passedContent)
        {
            this.Content = new ContentManager(passedContent.ServiceProvider, "Content");
            fontArial = Content.Load<SpriteFont>("Arial");

            currentScreen.LoadContent();

        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentScreen.Draw(spriteBatch, gameTime);

            if (Debug) {
                
                Vector2 mouseScreenPos = InputHandler.MousePosition;
                Vector2 mouseWorldPos = currentScreen.Camera1.ScreenToWorldPosition(mouseScreenPos);
                spriteBatch.DrawString(fontArial, mouseWorldPos.ToString(), new Vector2(0,20), Color.LightGreen);
                spriteBatch.DrawString(fontArial, "[K] to Save Character Data to XML", new Vector2(0,40), Color.LightGreen);
            }
        }
    }
}