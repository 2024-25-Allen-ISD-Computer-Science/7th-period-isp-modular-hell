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
    public class Entity
    {
        protected Texture2D _entityTexture;
        public string texturePath;
        public string Name {get; set;}

        [XmlIgnore]
        public Vector2 _velocity;
        [XmlIgnore]
        public Vector2 _position;
        protected static ContentManager Content;

        [XmlIgnore]
        public XmlManager<EntityAttachment> xmlAttachmentManager;

        [XmlIgnore]
        public bool isMoving = false;

        public Entity()
        {
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _entityTexture = Content.Load<Texture2D>(texturePath);
        }

        public virtual void UnloadContent()
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_velocity.X != 0 || _velocity.Y != 0)
                doVelocity(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void Generate() {
            LoadContent();
        }

        protected void doVelocity(GameTime gameTime) {
            _position += (Vector2.Multiply(_velocity, (float)gameTime.ElapsedGameTime.TotalSeconds));
            System.Console.WriteLine("character is at " + _position);
            //use world friction once put into xml
            _velocity.X *= (.5f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            _velocity.Y *= (.5f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (_velocity.X * _velocity.X < 1) 
            {
                _velocity.X = 0;
            }
            if (_velocity.Y * _velocity.Y < 1)
            {
                _velocity.Y = 0;
            }
        }

    }
}