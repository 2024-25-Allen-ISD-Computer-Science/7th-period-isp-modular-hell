using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace ModularHell 
{
    public class Entity
    {
        protected byte _MaxAttachments;
        protected Texture2D _entityTexture;
        public Vector2 _position;
        protected static ContentManager Content;
        protected List<EntityAttachment> _attachmentSlots;

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public void MoveX(int distance) {
            _position.X += distance;
            System.Console.WriteLine("character is at " + _position);
        }

        public void MoveY(int distance) {
            _position.Y += distance;
            System.Console.WriteLine("character is at " + _position);
        }

    }
}