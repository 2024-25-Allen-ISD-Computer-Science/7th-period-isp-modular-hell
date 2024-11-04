using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ModularHell
{
    public class ScreenManager 
    {

        GameScreen currentScreen;

        public Vector2 Dimensions {private set; get;}
        public ContentManager Content {private set; get;}

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
        }
        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
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