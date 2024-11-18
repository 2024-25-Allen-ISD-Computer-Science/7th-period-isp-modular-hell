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
  
        public Entity host {get;set;}
        protected List<Action> attacks = new List<Action>(); 

        public List<string> tags = [];

        public float speedModifier = 1.0f;
        public float damageModifier = 1.0f;
        protected Texture2D _attachmentTexture;
        public string texturePath;

        private float rotation;

        protected ContentManager Content;
        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;
        
        [XmlIgnore]
        public Type Type;

        public EntityAttachment()
        {
            Type = this.GetType();
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
            Console.WriteLine("BBBBBB");
            //rotation += 0.1f;
            //spriteBatch.Draw(_attachmentTexture, host._position + new Vector2(0,10), Color.White);
        }
    }
}