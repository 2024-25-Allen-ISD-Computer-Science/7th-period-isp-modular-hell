using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell
{
    public class ScreenManager 
    {

        GameScreen currentScreen;
        XmlManager<GameScreen> xmlGameScreenManager;
        public ContentManager Content {private set; get;}
        public Vector2 Dimensions {private set; get;}

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
            
            currentScreen = new Level1();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            currentScreen = xmlGameScreenManager.Load("Screens/Load/Level1.xml");
        }
        public void LoadContent(ContentManager passedContent)
        {
            this.Content = new ContentManager(passedContent.ServiceProvider, "Content");
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

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }
    }
}