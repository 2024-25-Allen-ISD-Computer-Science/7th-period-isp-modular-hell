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
using System.IO;
using System.Runtime.CompilerServices;

namespace ModularHell 
{
    public class Object
    {
        public Vector2 _position;
        public Vector2 _velocity = Vector2.Zero;
        public string texturePath = "ball";
        protected Texture2D _texture;
        [XmlIgnore]
        public Rectangle TextureRect;
        protected static ContentManager Content;
        private int timer = 0;
        public Vector2 Origin;
        public bool dead = false;

        public Object(Vector2 position, Vector2 velocity) {
            _position = position;
            _velocity = velocity;
        } 
        public void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _texture = Content.Load<Texture2D>(texturePath);
            Origin = new Vector2(50,50);
        }

        public void UnloadContent()
        {
            Content.Unload();
        }
        

        public void Update(GameTime gameTime)
        {
            doMovement();
            if (timer == 2) {
                dead = true;
                UnloadContent();
            }
        }

        public void Draw(ref Camera cam, SpriteBatch spriteBatch)
        {
            Vector2 screenPosition = cam.WorldToScreenPosition(_position);
            float rotation = 0f;
            TextureRect = new Rectangle((int)screenPosition.X,(int)screenPosition.Y, (int)(100 * cam.Scale), (int)(100 * cam.Scale));
            spriteBatch.Draw(_texture, TextureRect, null, Color.White, rotation, Origin, SpriteEffects.None, 1f);
        }

        private void doMovement() {
            _position += _velocity;
            timer += 1;
        }
    }
}