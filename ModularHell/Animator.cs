using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;

namespace ModularHell 
{
    public class Animator
    {

        public static float Swing(float rotation, float tick)
        {
            
            rotation = (float)Math.Sin(tick);
            
              return rotation;
        }

        public static void Idle(ref Character entity, SpriteBatch spriteBatch, Vector2 screenPosition, float ticks)
        {

            // each item in list is a frame, contains information for each body part (rotation and offset)
            var keyframes = new List<List<Dictionary<string, object>>>()
            {
                new() {
                    new() {
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(50f,80f)}
                    },
                    new() {
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(20f,80f)}
                    },
                     new() {
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(60f,120f)}
                    },
                    new() {
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(35f,120f)}
                    }
                },
                new() {
                    new() {
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(50f,80f)}
                    },
                    new() {
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(20f,80f)}
                    },
                    new() {
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(60f,120f)}
                    },
                    new() {
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(35f,120f)}
                    }
                }
            };

            var keyframeTimes = new List<int>(){
                0, //at frame 0, pose character as keyframe[0] prescribes
                100
            };


            var torso = entity.AttachmentSlots[0];
            var attachments = torso.Item1.AttachmentSlots;

            var drawOrder = new List<int>() {0,2,3,1};
            if (entity.isMoving) {
                if (entity.frame >= keyframeTimes.Last()) {
                    entity.frameRate = -1;
                } else if (entity.frame == 0) {
                    entity.frameRate = 1;
                }

                entity.frame += entity.frameRate;
            }

            for (int i = 0; i < keyframeTimes.Count - 1; i++) {
                if (entity.frame <= keyframeTimes[i+1]) {
                    var distance = ((float)entity.frame-(float)keyframeTimes[i])/((float)keyframeTimes[i+1]-(float)keyframeTimes[i]);
                    var frame = keyframes[keyframeTimes[0]];


                    for (int part = 0; part < attachments.Length; part++) {

                        var RotationLerp = float.Lerp((float)keyframes[i][drawOrder[part]]["Rotation"], (float)keyframes[i+1][drawOrder[part]]["Rotation"], distance);
                        var OffsetLerp = Vector2.Lerp((Vector2)keyframes[i][drawOrder[part]]["Offset"], (Vector2)keyframes[i+1][drawOrder[part]]["Offset"], distance);
                        attachments[drawOrder[part]].Item1.Draw(spriteBatch, Vector2.Add(screenPosition, OffsetLerp), RotationLerp);
                        if (part == 1) {
                            torso.Item1.Draw(spriteBatch, Vector2.Add(screenPosition, torso.Item3), 2f);
                        }
                    }

                    return;
                }
            }
        }

        public static void Walk(ref Character entity, SpriteBatch spriteBatch, Vector2 screenPosition, float ticks)
        {

            // each item in list is a frame, contains information for each body part (rotation and offset)
            var keyframes = new List<List<Dictionary<string, object>>>()
            {
                new() {
                    new() {
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(50f,80f)}
                    },
                    new() {
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(20f,80f)}
                    },
                     new() {
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(60f,120f)}
                    },
                    new() {
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(35f,120f)}
                    }
                },
                new() {
                    new() {
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(50f,80f)}
                    },
                    new() {
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(20f,80f)}
                    },
                    new() {
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(60f,120f)}
                    },
                    new() {
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(35f,120f)}
                    }
                }
            };
            

            var drawOrder = new List<int>() {0,2,3,1};
            var keyframeTimes = new List<int>(){
                0, //at frame 0, pose character as keyframe[0] prescribes
                100
            };

            
            var torso = entity.AttachmentSlots[0];
            var attachments = torso.Item1.AttachmentSlots; 

            if (entity.isMoving) {
                if (entity.frame >= keyframeTimes.Last()) {
                    entity.frameRate = -1;
                } else if (entity.frame == 0) {
                    entity.frameRate = 1;
                }

                entity.frame += entity.frameRate;
            }

            for (int i = 0; i < keyframeTimes.Count - 1; i++) {
                if (entity.frame <= keyframeTimes[i+1]) {
                    var distance = ((float)entity.frame-(float)keyframeTimes[i])/((float)keyframeTimes[i+1]-(float)keyframeTimes[i]);
                    var frame = keyframes[keyframeTimes[0]];


                    for (int part = 0; part < attachments.Length; part++) {

                        var RotationLerp = float.Lerp((float)keyframes[i][drawOrder[part]]["Rotation"], (float)keyframes[i+1][drawOrder[part]]["Rotation"], distance);
                        var OffsetLerp = Vector2.Lerp((Vector2)keyframes[i][drawOrder[part]]["Offset"], (Vector2)keyframes[i+1][drawOrder[part]]["Offset"], distance);
                        attachments[drawOrder[part]].Item1.Draw(spriteBatch, Vector2.Add(screenPosition, OffsetLerp), RotationLerp);
                        if (part == 1) {
                            torso.Item1.Draw(spriteBatch, Vector2.Add(screenPosition, torso.Item3), 2f);
                        }
                    }

                    return;
                }
            }
        }
    }
}