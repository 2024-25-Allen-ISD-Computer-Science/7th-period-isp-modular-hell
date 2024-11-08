using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices.Marshalling;
using System.Xml;
using System.Xml.Serialization;

namespace ModularHell 
{
    public class EntityAttachment
    {
        [XmlIgnore]
        protected Entity host;

        [XmlIgnore]
        protected List<Action> attacks = new List<Action>(); 

        protected string type;

        public List<string> tags = [];

        public float speedModifier = 1.0f;
        public float damageModifier = 0.5f;
        protected Texture2D _attachmentTexture;
        protected ContentManager Content;
        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;

        public EntityAttachment(Entity _host, string _xmlID)
        {
            host = _host;

        }
        
        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
        }
         public void LoadMethods()
        {
            foreach (var method in typeof(AttachmentFunctionality).GetMethods())
            {
                if (this.tags.Contains(method.Name))
                {
                    this.attacks.Add( ()=> method.Invoke(this, null));
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_attachmentTexture, host._position + new Vector2(0,10), Color.White);
        }
    }
}