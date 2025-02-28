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
using System.Reflection;

namespace ModularHell 
{
    public class Animator
    {

        private static void Animate(ref Character entity, SpriteBatch spriteBatch, Vector2 screenPosition, float ticks, string characterState, List<List<Dictionary<string, object>>> keyframes, List<int> keyframeTimes) {

            var torso = entity.AttachmentSlots[0];
            var attachments = torso.Item1.AttachmentSlots; 

            
            if (entity.frame >= keyframeTimes.Last()) {
                entity.frameRate = -1;
            } else if (entity.frame == 0) {
                entity.frameRate = 1;
            }

            entity.frame += entity.frameRate;
            

            for (int i = 0; i < keyframeTimes.Count - 1; i++) {
                if (entity.frame <= keyframeTimes[i+1]) {
                    var distance = ((float)entity.frame-(float)keyframeTimes[i])/((float)keyframeTimes[i+1]-(float)keyframeTimes[i]);
                    var frame = keyframes[keyframeTimes[0]];

                    for (int LeftPart = 1; LeftPart < attachments.Length; LeftPart += 2) {
                        var RotationLerp = float.Lerp((float)keyframes[i][LeftPart]["Rotation"], (float)keyframes[i+1][LeftPart]["Rotation"], distance);
                        var OffsetLerp = Vector2.Lerp((Vector2)keyframes[i][LeftPart]["Offset"], (Vector2)keyframes[i+1][LeftPart]["Offset"], distance);
                        attachments[LeftPart - 1].Item1.Draw(spriteBatch, Vector2.Add(screenPosition, OffsetLerp), RotationLerp);
                        // above must be "LeftPart - 1" to account for offset by torso keyframes
                    }

                    var TorsoRotationLerp = float.Lerp((float)keyframes[i][0]["Rotation"], (float)keyframes[i+1][0]["Rotation"], distance);
                    var TorsoOffsetLerp = Vector2.Lerp((Vector2)keyframes[i][0]["Offset"], (Vector2)keyframes[i+1][0]["Offset"], distance);
                    torso.Item1.Draw(spriteBatch, Vector2.Add(screenPosition, TorsoOffsetLerp), TorsoRotationLerp);
                    
                    for (int RightPart = attachments.Length; RightPart > 0; RightPart -= 2) {
                        var RotationLerp = float.Lerp((float)keyframes[i][RightPart]["Rotation"], (float)keyframes[i+1][RightPart]["Rotation"], distance);
                        var OffsetLerp = Vector2.Lerp((Vector2)keyframes[i][RightPart]["Offset"], (Vector2)keyframes[i+1][RightPart]["Offset"], distance);
                        attachments[RightPart - 1].Item1.Draw(spriteBatch, Vector2.Add(screenPosition, OffsetLerp), RotationLerp);
                        // above must be "RightPart - 1" to account for offset by torso keyframes
                    }

                    return;
                }
            }
            
            
        }

        public static void Idle(ref Character entity, SpriteBatch spriteBatch, Vector2 screenPosition, float ticks)
        {

            // each item in list is a frame, contains information for each body part (rotation and offset)
            var keyframes = new List<List<Dictionary<string, object>>>()
            {
                new() {
                    new() { //torso
                            {"Rotation", .2f},
                            {"Offset", new Vector2(45f,75f)}
                    },
                    new() {  //leftArm
                            {"Rotation", .1f},
                            {"Offset", new Vector2(50f,80f)}
                    },
                    new() { //rightArm
                            {"Rotation", -.1f},
                            {"Offset", new Vector2(20f,80f)}
                    },
                     new() { //leftLeg
                            {"Rotation", -.1f},
                            {"Offset", new Vector2(60f,120f)}
                    },
                    new() { //rightLeg
                            {"Rotation", .1f},
                            {"Offset", new Vector2(35f,120f)}
                    }
                },
                new() {
                    new() { //torso
                            {"Rotation", .21f},
                            {"Offset", new Vector2(45f,72f)}
                    },
                    new() {  //leftArm
                            {"Rotation", .1f},
                            {"Offset", new Vector2(50f,78f)}
                    },
                    new() { //rightArm
                            {"Rotation", .1f},
                            {"Offset", new Vector2(20f,78f)}
                    },
                     new() { //leftLeg
                            {"Rotation", -.2f},
                            {"Offset", new Vector2(60f,120f)}
                    },
                    new() { //rightLeg
                            {"Rotation", .2f},
                            {"Offset", new Vector2(35f,120f)}
                    }
                }
            };

            var keyframeTimes = new List<int>(){
                0, //at frame 0, pose character as keyframe[0] prescribes
                100
            };

            Animate(ref entity, spriteBatch, screenPosition, ticks, "Idle", keyframes, keyframeTimes);
        }

        public static void Walk(ref Character entity, SpriteBatch spriteBatch, Vector2 screenPosition, float ticks)
        {

            // each item in list is a frame, contains information for each body part (rotation and offset)
            var keyframes = new List<List<Dictionary<string, object>>>()
            {
                new() {
                    new() { //torso
                            {"Rotation", 2.0f},
                            {"Offset", new Vector2(45f,75f)}
                    },
                    new() {  //leftArm
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(50f,80f)}
                    },
                    new() {  //rightArm
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(20f,80f)}
                    },
                    new() { //leftLeg
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(60f,120f)}
                    },
                    new() {  //rightLeg
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(35f,120f)}
                    }
                },
                new() {
                    new() { //torso
                            {"Rotation", 2.0f},
                            {"Offset", new Vector2(45f,75f)}
                    },
                    new() { //leftArm
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(50f,80f)}
                    },
                    new() { //rightArm
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(20f,80f)}
                    },
                    new() { //leftLeg
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(60f,120f)}
                    },
                    new() { //rightLeg
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(35f,120f)}
                    }
                }
            };
            var keyframeTimes = new List<int>(){
                0, //at frame 0, pose character as keyframe[0] prescribes
                100
            };

            Animate(ref entity, spriteBatch, screenPosition, ticks, "Walk", keyframes, keyframeTimes);
        }
    }
}