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
using System.Diagnostics;

namespace ModularHell 
{
    [XmlInclude(typeof(Torso))]
    public class EntityAttachment
    {
        [XmlIgnore]
        public Entity Host;

        [XmlIgnore]
        public XmlManager<EntityAttachment> xmlAttachmentManager;

        public (EntityAttachment, int, Vector2)[] AttachmentSlots;
        //      (Attachment   , Slot Tier, Position)
        protected List<Action> attacks = new List<Action>(); 

        public List<string> tags = [];

        public float speedModifier = 1.0f;
        public float damageModifier = 1.0f;
        protected Texture2D _attachmentTexture;
        public Vector2 Dimensions;
        [XmlIgnore]
        public Rectangle TextureRect;
        [XmlIgnore]
        public Rectangle PhysicsRect;
        public Vector2 Origin;
        public Vector2 PositionOnHost;
        public string texturePath;
        public float storedRotation;
        public Vector2 storedOffset;

        [XmlIgnore]
        public Func<float, float, float> animation;

        protected ContentManager Content;
        //public HealthComponent Health;  
        //public Vector2 PosOnEntity;

        public EntityAttachment() {
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            Host = null;
            AttachmentSlots = new (EntityAttachment, int, Vector2)[0];
            //default 0 attachment slots
        }
        public EntityAttachment(int slots = 0) {
            xmlAttachmentManager = new XmlManager<EntityAttachment>();
            Host = null;
            AttachmentSlots = new (EntityAttachment, int, Vector2)[slots];
            //default 0 attachment slots
        }
        
        public virtual void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            _attachmentTexture = Content.Load<Texture2D>(texturePath);
            Dimensions.Y = _attachmentTexture.Height;
            Dimensions.X = _attachmentTexture.Width;
            Origin = new Vector2(_attachmentTexture.Width / 2f, _attachmentTexture.Height / 7f);

            if (AttachmentSlots.Length > 0) {
                foreach (var (attachment, tier, position) in AttachmentSlots){
                    attachment.Host = this.Host;
                    attachment.PositionOnHost = position;
                    attachment.LoadContent();
                    //temp
                    
                }
            }
        }

        public virtual void UnloadContent()
        {
            if (AttachmentSlots.Length > 0) {
                foreach (var (attachment, tier, position) in AttachmentSlots){
                    AttachmentSlots[0].Item1.UnloadContent();
                }
            }
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
                //Console.WriteLine(method.Name);
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

        public virtual void Draw(ref Camera cam, SpriteBatch spriteBatch, ref Vector2 AnimationOffset, ref float rotation)
        {
            Vector2 _position = Host._position + PositionOnHost + AnimationOffset;
            Vector2 screenPosition = cam.WorldToScreenPosition(_position);
            TextureRect = new Rectangle((int)screenPosition.X,(int)screenPosition.Y, (int)(Dimensions.X * cam.Scale), (int)(Dimensions.Y * cam.Scale));
            storedOffset = AnimationOffset;
            storedRotation = rotation;
            spriteBatch.Draw(_attachmentTexture, TextureRect, null, Color.White, rotation, Origin, SpriteEffects.None, 1f);
        }

        public virtual void Generate() {
            for (int slot = 0; slot < AttachmentSlots.Length; slot++) {
                AttachmentSlots[slot].Item1 = new EntityAttachment();
                AttachmentSlots[slot].Item1.texturePath = "Arm";
                AttachmentSlots[slot].Item1.speedModifier = 0.5f;
                AttachmentSlots[slot].Item1.damageModifier = 1f;
            }
        }
    }
}