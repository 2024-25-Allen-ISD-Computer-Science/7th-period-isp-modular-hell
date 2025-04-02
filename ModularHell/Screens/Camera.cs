using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModularHell
{
    public class Camera {
        public Vector2 Position {
            get {return _pos;} 
            set { _targetPos = value;}
        }
        private Vector2 _pos;
        private Vector2 _targetPos; 

        public float Scale {
            get {return _scale;} 
            set { _targetScale = value;}
        }

        private float _scale;
        private float _targetScale;


        public Camera() {
            _scale = 1f;
            _targetScale = _scale;
            _pos = Vector2.Zero;
            _targetPos = _pos;
        }

        public Camera(Vector2 pos, float scale = 1f) {
            _scale = scale;
            _targetScale = scale;
            _pos = pos;
            _targetPos = pos;
        }
        public void Update() {
            if (_pos != _targetPos) {
                _pos = Vector2.Lerp(_pos,_targetPos,.125f);
            }

        }

        public Vector2 ScreenToWorldPosition(Vector2 screenCoords) {
            Vector2 camOffset = Vector2.Subtract(screenCoords, ScreenManager.Instance.MiddleScreen);
            return Vector2.Add(camOffset, _pos);
        }

        public Vector2 WorldToScreenPosition(Vector2 worldCoords) {
            Vector2 camOffset = Vector2.Subtract(worldCoords, _pos);
            return Vector2.Add(camOffset, ScreenManager.Instance.MiddleScreen);
        }

        public void Draw(SpriteBatch spriteBatch) {
            
        }

    }
}