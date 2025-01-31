using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ModularHell
{
    public class GameScreen 
    {
        protected ContentManager Content;
        [XmlIgnore]
        public Type Type;

        public Camera Camera1;

        public GameScreen()
        {
            Type = this.GetType();
        }

        public Map PlayMap;

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            PlayMap.LoadContent();
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            Vector2 camPos = Camera1.Position;
            PlayMap.Draw(spriteBatch, camPos);

        }

    }
}