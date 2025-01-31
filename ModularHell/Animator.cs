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

        public static void Idle(ref Character entity, SpriteBatch spriteBatch, float ticks)
        {

            // each item in list is a frame, contains information for each body part (rotation and offset)
            var keyframes = new List<Dictionary<string, Dictionary<string, object>>>()
            {
                new() {
                    {"torso", new(){
                            {"Rotation", 2.0f},
                            {"Offset", new Vector2(45f,80f)}
                        }
                    },
                    {"rArm", new(){
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(20f,80f)}
                        }
                    },
                    {"lArm", new(){
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(50f,80f)}
                        }
                    },
                    {"rLeg", new(){
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(35f,120f)}
                        }
                    },
                    {"lLeg", new(){
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(60f,120f)}
                        }
                    },
                },
                new() {
                    {"torso", new(){
                            {"Rotation", 2.0f},
                            {"Offset", new Vector2(45f,80f)}
                        }
                    },
                    {"rArm", new(){
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(20f,80f)}
                        }
                    },
                    {"lArm", new(){
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(50f,80f)}
                        }
                    },
                    {"rLeg", new(){
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(35f,120f)}
                        }
                    },
                    {"lLeg", new(){
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(60f,120f)}
                        }
                    },
                }
            };

            var keyframeTimes = new List<int>(){
                0, //at frame 0, pose character as keyframe[0] prescribes
                100
            };

            var attachments = entity.AttachmentSlots;
            var torso = attachments[0].Item1;
            var rArm = attachments[0].Item1.AttachmentSlots[1].Item1;
            var lArm = attachments[0].Item1.AttachmentSlots[0].Item1;
            var rLeg = attachments[0].Item1.AttachmentSlots[3].Item1;
            var lLeg = attachments[0].Item1.AttachmentSlots[2].Item1;

            if (entity.isMoving) {
                if (entity.frame >= keyframeTimes.Last()) {
                    entity.frameRate = -1;
                } else if (entity.frame == 0) {
                    entity.frameRate = 1;
                }

                entity.frame += entity.frameRate;
            }

            for (int i = 1; i < keyframeTimes.Count; i++) {
                if (entity.frame <= keyframeTimes[i]) {
                    var distance = ((float)entity.frame-(float)keyframeTimes[i-1])/((float)keyframeTimes[i]-(float)keyframeTimes[i-1]);
                    var frame = keyframes[keyframeTimes[0]];

                    var lArmRotationLerp = float.Lerp((float)keyframes[i-1]["lArm"]["Rotation"], (float)keyframes[i]["lArm"]["Rotation"], distance);
                    var lArmOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["lArm"]["Offset"], (Vector2)keyframes[i]["lArm"]["Offset"], distance);
                    lArm.Draw(spriteBatch, lArmOffsetLerp, lArmRotationLerp);

                    var lLegRotationLerp = float.Lerp((float)keyframes[i-1]["lLeg"]["Rotation"], (float)keyframes[i]["lLeg"]["Rotation"], distance);
                    var lLegOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["lLeg"]["Offset"], (Vector2)keyframes[i]["lLeg"]["Offset"], distance);
                    lLeg.Draw(spriteBatch, lLegOffsetLerp, lLegRotationLerp);

                    var torsoRotationLerp = float.Lerp((float)keyframes[i-1]["torso"]["Rotation"], (float)keyframes[i]["torso"]["Rotation"], distance);
                    var torsoOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["torso"]["Offset"], (Vector2)keyframes[i]["torso"]["Offset"], distance);
                    torso.Draw(spriteBatch, torsoOffsetLerp, torsoRotationLerp);    

                    var rLegRotationLerp = float.Lerp((float)keyframes[i-1]["rLeg"]["Rotation"], (float)keyframes[i]["rLeg"]["Rotation"], distance);
                    var rLegOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["rLeg"]["Offset"], (Vector2)keyframes[i]["rLeg"]["Offset"], distance);
                    rLeg.Draw(spriteBatch, rLegOffsetLerp, rLegRotationLerp);

                     var rArmRotationLerp = float.Lerp((float)keyframes[i-1]["rArm"]["Rotation"], (float)keyframes[i]["rArm"]["Rotation"], distance);
                    var rArmOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["rArm"]["Offset"], (Vector2)keyframes[i]["rArm"]["Offset"], distance);
                    rArm.Draw(spriteBatch, rArmOffsetLerp, rArmRotationLerp);                

                    return;
                }
            }
        }

        public static void Walk(ref Character entity, SpriteBatch spriteBatch, float ticks)
        {

            // each item in list is a frame, contains information for each body part (rotation and offset)
            var keyframes = new List<Dictionary<string, Dictionary<string, object>>>()
            {
                new() {
                    {"torso", new(){
                            {"Rotation", 2.0f},
                            {"Offset", new Vector2(45f,80f)}
                        }
                    },
                    {"rArm", new(){
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(20f,80f)}
                        }
                    },
                    {"lArm", new(){
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(50f,80f)}
                        }
                    },
                    {"rLeg", new(){
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(35f,120f)}
                        }
                    },
                    {"lLeg", new(){
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(60f,120f)}
                        }
                    },
                },
                new() {
                    {"torso", new(){
                            {"Rotation", 2.0f},
                            {"Offset", new Vector2(45f,80f)}
                        }
                    },
                    {"rArm", new(){
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(20f,80f)}
                        }
                    },
                    {"lArm", new(){
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(50f,80f)}
                        }
                    },
                    {"rLeg", new(){
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(35f,120f)}
                        }
                    },
                    {"lLeg", new(){
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(60f,120f)}
                        }
                    },
                }
            };

            var keyframeTimes = new List<int>(){
                0, //at frame 0, pose character as keyframe[0] prescribes
                100
            };

            var attachments = entity.AttachmentSlots;
            var torso = attachments[0].Item1;
            var rArm = attachments[0].Item1.AttachmentSlots[1].Item1;
            var lArm = attachments[0].Item1.AttachmentSlots[0].Item1;
            var rLeg = attachments[0].Item1.AttachmentSlots[3].Item1;
            var lLeg = attachments[0].Item1.AttachmentSlots[2].Item1;

            if (entity.isMoving) {
                if (entity.frame >= keyframeTimes.Last()) {
                    entity.frameRate = -1;
                } else if (entity.frame == 0) {
                    entity.frameRate = 1;
                }

                entity.frame += entity.frameRate;
            }

            for (int i = 1; i < keyframeTimes.Count; i++) {
                if (entity.frame <= keyframeTimes[i]) {
                    var distance = ((float)entity.frame-(float)keyframeTimes[i-1])/((float)keyframeTimes[i]-(float)keyframeTimes[i-1]);
                    var frame = keyframes[keyframeTimes[0]];

                    var lArmRotationLerp = float.Lerp((float)keyframes[i-1]["lArm"]["Rotation"], (float)keyframes[i]["lArm"]["Rotation"], distance);
                    var lArmOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["lArm"]["Offset"], (Vector2)keyframes[i]["lArm"]["Offset"], distance);
                    lArm.Draw(spriteBatch, lArmOffsetLerp, lArmRotationLerp);

                    var lLegRotationLerp = float.Lerp((float)keyframes[i-1]["lLeg"]["Rotation"], (float)keyframes[i]["lLeg"]["Rotation"], distance);
                    var lLegOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["lLeg"]["Offset"], (Vector2)keyframes[i]["lLeg"]["Offset"], distance);
                    lLeg.Draw(spriteBatch, lLegOffsetLerp, lLegRotationLerp);

                    var torsoRotationLerp = float.Lerp((float)keyframes[i-1]["torso"]["Rotation"], (float)keyframes[i]["torso"]["Rotation"], distance);
                    var torsoOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["torso"]["Offset"], (Vector2)keyframes[i]["torso"]["Offset"], distance);
                    torso.Draw(spriteBatch, torsoOffsetLerp, torsoRotationLerp);    

                    var rLegRotationLerp = float.Lerp((float)keyframes[i-1]["rLeg"]["Rotation"], (float)keyframes[i]["rLeg"]["Rotation"], distance);
                    var rLegOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["rLeg"]["Offset"], (Vector2)keyframes[i]["rLeg"]["Offset"], distance);
                    rLeg.Draw(spriteBatch, rLegOffsetLerp, rLegRotationLerp);

                     var rArmRotationLerp = float.Lerp((float)keyframes[i-1]["rArm"]["Rotation"], (float)keyframes[i]["rArm"]["Rotation"], distance);
                    var rArmOffsetLerp = Vector2.Lerp((Vector2)keyframes[i-1]["rArm"]["Offset"], (Vector2)keyframes[i]["rArm"]["Offset"], distance);
                    rArm.Draw(spriteBatch, rArmOffsetLerp, rArmRotationLerp);                

                    return;
                }
            }
        }
    }
}