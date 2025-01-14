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
        public Entity Host;
        protected List<Action> attacks = new List<Action>(); 

        public List<string> tags = [];

        public float speedModifier = 1.0f;
        public float damageModifier = 1.0f;
        protected Texture2D _attachmentTexture;
        public string texturePath;

        [XmlIgnore]
        private float rotation;
        [XmlIgnore]
        private float tick = 0.0f;
        [XmlIgnore]
        public Func<float, float, float> animation;

        protected ContentManager Content;
        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;

        public EntityAttachment() {
            Host = null;
        }
        
        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _attachmentTexture = Content.Load<Texture2D>(texturePath);
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
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

        public void LoadAnimation(string name)
        {
            foreach (var method in typeof(Animator).GetMethods())
            {
                Console.WriteLine(method.Name);
                if (method.Name == name)
                {
                   this.animation = new Func<float, float, float>((rotation, tick) =>
                {
                    return (float)method.Invoke(this, [rotation, tick]);
                });
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset, float interval)
        {
            Rectangle attachmentRect = new Rectangle(0,0, _attachmentTexture.Width, _attachmentTexture.Height);
            var origin = new Vector2(_attachmentTexture.Width / 2f, _attachmentTexture.Height / 7f);
            
            if (animation != null){
                tick += interval;

                rotation = animation(rotation, tick);
            }

            spriteBatch.Draw(
                _attachmentTexture, // texture
                new Vector2(this.Host._position.X + offset.X, this.Host._position.Y + offset.Y), // position
                attachmentRect, // rect
                Color.White, // color (useful for recolors of the same attachment sprites)
                rotation, // rotation
                origin, // origin
                0.1f, // scale
                SpriteEffects.None, // sprite effects
                0.5f // layerdepth
                );
        }

        public virtual void Generate() {
            LoadContent();
        }
    }
}