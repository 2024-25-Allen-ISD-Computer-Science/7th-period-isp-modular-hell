using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ModularHell 
{
    public class Character
    {

        public string name;
        public int velocity;
        public int x,y;
        
        private InputComponent inputComponent = new InputComponent();
        
        private EntityAttachment slot1 = new EntityAttachment();

        
        public void Update() {
            // fix error here 
            this.inputComponent.Update(this);
        }
    };
};