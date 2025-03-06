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

        private static void Animate(ref Character entity, ref Camera cam, SpriteBatch spriteBatch, float ticks, string characterState, List<Keyframe> keyframes) {

            var torso = entity.AttachmentSlots[0];
            var attachments = torso.Item1.AttachmentSlots; 

            if (entity.transitionFrame < 25) {
                
                var goalFrame = keyframes[0];
                var distance = (float)entity.transitionFrame / 25.0f;
                

                for (int LeftPart = 1; LeftPart < attachments.Length; LeftPart += 2)
                {
                    var RotationLerp = 0.0f;
                    var OffsetLerp = new Vector2(0, 0);

                    if (LeftPart % 4 == 3)
                    {
                        RotationLerp = float.Lerp((float)entity.previousKeyframe.LeftLeg["Rotation"], (float)goalFrame.LeftLeg["Rotation"], distance);
                        OffsetLerp = Vector2.Lerp((Vector2)entity.previousKeyframe.LeftLeg["Offset"], (Vector2)goalFrame.LeftLeg["Offset"], distance);
                    }
                    else if (LeftPart % 4 == 1)
                    {
                        RotationLerp = float.Lerp((float)entity.previousKeyframe.LeftArm["Rotation"], (float)goalFrame.LeftArm["Rotation"], distance);
                        OffsetLerp = Vector2.Lerp((Vector2)entity.previousKeyframe.LeftArm["Offset"], (Vector2)goalFrame.LeftArm["Offset"], distance);
                    }

                    attachments[LeftPart - 1].Item1.Draw(ref cam, spriteBatch, ref OffsetLerp, ref RotationLerp);
                    // above must be "LeftPart - 1" to account for offset by torso keyframes
                }

                var TorsoRotationLerp = float.Lerp((float)entity.previousKeyframe.Torso["Rotation"], (float)goalFrame.Torso["Rotation"], distance);
                var TorsoOffsetLerp = Vector2.Lerp((Vector2)entity.previousKeyframe.Torso["Offset"], (Vector2)goalFrame.Torso["Offset"], distance);
                torso.Item1.Draw(ref cam, spriteBatch, ref TorsoOffsetLerp, ref TorsoRotationLerp);

                for (int RightPart = attachments.Length; RightPart > 0; RightPart -= 2)
                {
                    var RotationLerp = 0.0f;
                    var OffsetLerp = new Vector2(0, 0);

                    if (RightPart % 4 == 0)
                    {
                        RotationLerp = float.Lerp((float)entity.previousKeyframe.RightLeg["Rotation"], (float)goalFrame.RightLeg["Rotation"], distance);
                        OffsetLerp = Vector2.Lerp((Vector2)entity.previousKeyframe.RightLeg["Offset"], (Vector2)goalFrame.RightLeg["Offset"], distance);
                    }
                    else if (RightPart % 4 == 2)
                    {
                        RotationLerp = float.Lerp((float)entity.previousKeyframe.RightArm["Rotation"], (float)goalFrame.RightArm["Rotation"], distance);
                        OffsetLerp = Vector2.Lerp((Vector2)entity.previousKeyframe.RightArm["Offset"], (Vector2)goalFrame.RightArm["Offset"], distance);
                    }

                    attachments[RightPart - 1].Item1.Draw(ref cam, spriteBatch, ref OffsetLerp, ref RotationLerp);
                    // above must be "RightPart - 1" to account for offset by torso keyframes
                }

                entity.transitionFrame += 1;
            } else {
                if (entity.characterState == characterState) {
                    if (entity.frame >= keyframes.Last().frame) {
                        entity.frameRate = -1;
                    } else if (entity.frame == 0) {
                        entity.frameRate = 1;
                }

                entity.frame += entity.frameRate;
                }

                for (int i = 0; i < keyframes.Count - 1; i++) {
                    if (entity.frame <= keyframes[i+1].frame) {
                        var distance = ((float)entity.frame-(float)keyframes[i].frame)/((float)keyframes[i+1].frame-(float)keyframes[i].frame);
                        entity.previousKeyframe = new Keyframe(
                            new() {
                                new() { //torso
                                    {"Rotation", torso.Item1.storedRotation},
                                    {"Offset", torso.Item1.storedOffset}
                                },
                                new() { //leftArm
                                    {"Rotation", attachments[0].Item1.storedRotation},
                                    {"Offset", attachments[0].Item1.storedOffset}
                                },
                                new() { //rightArm
                                    {"Rotation", attachments[1].Item1.storedRotation},
                                    {"Offset", attachments[1].Item1.storedOffset}
                                },
                                new() { //leftLeg
                                    {"Rotation", attachments[2].Item1.storedRotation},
                                    {"Offset", attachments[2].Item1.storedOffset}
                                },
                                new() { //rightLeg
                                    {"Rotation", attachments[3].Item1.storedRotation},
                                    {"Offset", attachments[3].Item1.storedOffset}
                                }
                            }, 0
                        );

                        for (int LeftPart = 1; LeftPart < attachments.Length; LeftPart += 2) {
                            var RotationLerp = 0.0f;
                            var OffsetLerp = new Vector2(0,0);

                            if (LeftPart % 4 == 3) {
                                RotationLerp = float.Lerp((float)keyframes[i].LeftLeg["Rotation"], (float)keyframes[i+1].LeftLeg["Rotation"], distance);
                                OffsetLerp = Vector2.Lerp((Vector2)keyframes[i].LeftLeg["Offset"], (Vector2)keyframes[i+1].LeftLeg["Offset"], distance);
                            } else if (LeftPart % 4 == 1) {
                                RotationLerp = float.Lerp((float)keyframes[i].LeftArm["Rotation"], (float)keyframes[i+1].LeftArm["Rotation"], distance);
                                OffsetLerp = Vector2.Lerp((Vector2)keyframes[i].LeftArm["Offset"], (Vector2)keyframes[i+1].LeftArm["Offset"], distance);
                            }

                            attachments[LeftPart - 1].Item1.Draw(ref cam, spriteBatch, ref OffsetLerp, ref RotationLerp);
                            // above must be "LeftPart - 1" to account for offset by torso keyframes
                        }

                        var TorsoRotationLerp = float.Lerp((float)keyframes[i].Torso["Rotation"], (float)keyframes[i+1].Torso["Rotation"], distance);
                        var TorsoOffsetLerp = Vector2.Lerp((Vector2)keyframes[i].Torso["Offset"], (Vector2)keyframes[i+1].Torso["Offset"], distance);
                        torso.Item1.Draw(ref cam, spriteBatch, ref TorsoOffsetLerp, ref TorsoRotationLerp);
                    
                        for (int RightPart = attachments.Length; RightPart > 0; RightPart -= 2) {
                            var RotationLerp = 0.0f;
                            var OffsetLerp = new Vector2(0,0);

                            if (RightPart % 4 == 0) {
                                RotationLerp = float.Lerp((float)keyframes[i].RightLeg["Rotation"], (float)keyframes[i+1].RightLeg["Rotation"], distance);
                                OffsetLerp = Vector2.Lerp((Vector2)keyframes[i].RightLeg["Offset"], (Vector2)keyframes[i+1].RightLeg["Offset"], distance);
                            } else if (RightPart % 4 == 2) {
                                RotationLerp = float.Lerp((float)keyframes[i].RightArm["Rotation"], (float)keyframes[i+1].RightArm["Rotation"], distance);
                                OffsetLerp = Vector2.Lerp((Vector2)keyframes[i].RightArm["Offset"], (Vector2)keyframes[i+1].RightArm["Offset"], distance);
                            }

                            attachments[RightPart - 1].Item1.Draw(ref cam, spriteBatch, ref OffsetLerp, ref RotationLerp);
                            // above must be "RightPart - 1" to account for offset by torso keyframes
                        }

                        return;
                    }
            }
            }
            
            
        }

        public static void Idle(ref Character entity, ref Camera cam, SpriteBatch spriteBatch, float ticks)
        {

            // each item in list is a frame, contains information for each body part (rotation and offset)
            var keyframes = new List<Keyframe>() 
            {
                new Keyframe(new() {
                    new() { //torso
                            {"Rotation", 0.0f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() {  //leftArm
                            {"Rotation", -0.7f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //rightArm
                            {"Rotation", 0.7f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                     new() { //leftLeg
                            {"Rotation", -0.3f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //rightLeg
                            {"Rotation", 0.3f},
                            {"Offset", new Vector2(0f,0f)}
                    }
                },
                //keyframe time
                0),
                new Keyframe(new() {
                    new() { //torso
                            {"Rotation", 0.0f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //leftArm
                            {"Rotation", -0.5f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //rightArm
                            {"Rotation", 0.5f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() {//leftLeg
                            {"Rotation", -0.25f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //rightLeg
                            {"Rotation", 0.25f},
                            {"Offset", new Vector2(0f,0f)}
                    }
                },
                //keyframe time
                100)
            };

            Animate(ref entity, ref cam, spriteBatch, ticks, "Idle", keyframes);
        }

        public static void Walk(ref Character entity, ref Camera cam, SpriteBatch spriteBatch, float ticks)
        {

            // each item in list is a frame, contains information for each body part (rotation and offset)
            var keyframes = new List<Keyframe>()
            {
                new Keyframe(new() {
                    new() { //torso
                            {"Rotation", 0.0f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() {  //leftArm
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() {  //rightArm
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //leftLeg
                            {"Rotation", -0.8f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() {  //rightLeg
                            {"Rotation", 0.8f},
                            {"Offset", new Vector2(0f,0f)}
                    }
                },
                //keyframe time
                0),
                new Keyframe(new() {
                    new() { //torso
                            {"Rotation", 0.0f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //leftArm
                            {"Rotation", -1.0f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //rightArm
                            {"Rotation", 1.0f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //leftLeg
                            {"Rotation", 0.8f},
                            {"Offset", new Vector2(0f,0f)}
                    },
                    new() { //rightLeg
                            {"Rotation", -0.8f},
                            {"Offset", new Vector2(0f,0f)}
                    }
                },
                //keyframe time
                100)
            };

            Animate(ref entity, ref cam, spriteBatch, ticks, "Walking", keyframes);
        }
    }
}