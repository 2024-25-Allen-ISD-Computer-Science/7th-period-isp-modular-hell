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

        public float Zoom {
            get {return _zoom;} 
            set { _targetZoom = value;}
        }

        private float _zoom;
        private float _targetZoom;


        public Camera() {
            _zoom = 1;
            _targetZoom = _zoom;
            _pos = Vector2.Zero;
            _targetPos = _pos;
        }

        public Camera(Vector2 pos, float zoom = 1) {
            _zoom = zoom;
            _targetZoom = zoom;
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
            return Vector2.Add(screenCoords, _pos);
        }

        public Vector2 WorldToScreenPosition(Vector2 screenCoords) {
            Vector2 camOffset = Vector2.Subtract(screenCoords, _pos);
            return Vector2.Add(screenCoords, ScreenManager.Instance.MiddleScreen);
        }

        public void Draw(SpriteBatch spriteBatch) {
            
        }

    }
}