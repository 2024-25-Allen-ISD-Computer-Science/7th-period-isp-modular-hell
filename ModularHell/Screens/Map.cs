using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ModularHell
{
    public class Map {

        protected ContentManager Content;

        public string mapTexturePath;
        private Texture2D mapTexture;

        public Map() {
            mapTexturePath = "Maps/windowsbackground";
        }

        public virtual void LoadContent() {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            mapTexture = Content.Load<Texture2D>(mapTexturePath);


        }
        
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 camPos)
        {       
            spriteBatch.Draw(mapTexture, -camPos, Color.White);
         
        }

    }
}