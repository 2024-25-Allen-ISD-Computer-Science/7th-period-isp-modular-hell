using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Data;


namespace ModularHell 
{
    [XmlInclude(typeof(Character))]
    public class Entity
    {
        protected Texture2D _entityTexture;

        public (EntityAttachment, int, Vector2)[] AttachmentSlots;
        //      (Attachment, Slot Tier, Position)
        public string texturePath;
        public string Name {get; set;}

        [XmlIgnore]
        public Vector2 _velocity;
        public Vector2 _position;
        protected static ContentManager Content;

        [XmlIgnore]
        public XmlManager<EntityAttachment> xmlAttachmentManager;

        [XmlIgnore]
        public bool isMoving = false;

        public Entity()
        {
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            AttachmentSlots = new (EntityAttachment, int, Vector2)[0];
        }

        public Entity(int slots = 0){
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            AttachmentSlots = new (EntityAttachment, int, Vector2)[slots];
        }

        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _entityTexture = Content.Load<Texture2D>(texturePath);

            if (AttachmentSlots.Length > 0) {
                foreach (var (attachment, tier, position) in AttachmentSlots){
                    attachment.Host = this;
                    attachment.LoadContent();
                    //temp
                    attachment.LoadAnimation("Swing");
                }
            }
        }

        public virtual void UnloadContent()
        {
            if (AttachmentSlots.Length > 0) {
                foreach (var (attachment, tier, position) in AttachmentSlots){
                    attachment.UnloadContent();
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_velocity.X != 0 || _velocity.Y != 0)
                doVelocity(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 camPos)
        {
        }

        public virtual void Generate() {
            //LoadContent();

            xmlAttachmentManager.Type = typeof(Torso);

            /*
            if (AttachmentSlots.Length > 0) {
                AttachmentSlots[1].Item1 = new Torso();
                AttachmentSlots[1].Item1.Generate();
            }
            */
            AttachmentSlots[0].Item1 = (Torso)xmlAttachmentManager.Load($"Entity/Load/Torso.xml");
            AttachmentSlots[0].Item1.Host = this;
            AttachmentSlots[0].Item1.Generate();

            /*
            if (!string.IsNullOrEmpty(Name)) {
                NeckSlotXml = $"{Name}_NeckSlot";
            }
            */
        }

        protected void doVelocity(GameTime gameTime) {
            _position += (Vector2.Multiply(_velocity, (float)gameTime.ElapsedGameTime.TotalSeconds));
            //System.Console.WriteLine("character is at " + _position);
            //use world friction once put into xml
            _velocity.X *= .5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _velocity.Y *= .5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
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