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

            if (AttachmentSlots.Length > 0) {
                foreach (var (attachment, tier, position) in AttachmentSlots){
                    attachment.Host = this.Host;
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

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 screenPosition, Vector2 offset, float rotation)
        {
            Rectangle attachmentRect = new Rectangle(0,0, _attachmentTexture.Width, _attachmentTexture.Height);
            var origin = new Vector2(_attachmentTexture.Width / 2f, _attachmentTexture.Height / 7f);
            storedOffset = offset;
            storedRotation = rotation;

            spriteBatch.Draw(
                _attachmentTexture, // texture
                screenPosition , // position
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
            for (int slot = 0; slot < AttachmentSlots.Length; slot++) {
                AttachmentSlots[slot].Item1 = new EntityAttachment();
                AttachmentSlots[slot].Item1.texturePath = "Arm";
                AttachmentSlots[slot].Item1.speedModifier = 0.5f;
                AttachmentSlots[slot].Item1.damageModifier = 1f;
            }
        }
    }
}