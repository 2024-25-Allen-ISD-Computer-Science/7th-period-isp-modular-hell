using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices.Marshalling;
using System.Xml;

namespace ModularHell 
{
    public class EntityAttachment
    {
        protected Entity _host;

        protected string _xmlID;
        protected Texture2D _attachmentTexture;
        protected ContentManager Content;
        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;
        
        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void LoadDataFromXml(string id)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}