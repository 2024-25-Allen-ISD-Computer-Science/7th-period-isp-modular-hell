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

namespace ModularHell {
    public class Keyframe
    {
        public Dictionary<string, object> Torso = new() 
        {
            {"Rotation", null},
            {"Offset", null}
        };

        public Dictionary<string, object> LeftArm = new() 
        {
            {"Rotation", null},
            {"Offset", null}
        };
        
        public Dictionary<string, object> RightArm = new() 
        {
            {"Rotation", null},
            {"Offset", null}
        };
        public Dictionary<string, object> LeftLeg = new() 
        {
            {"Rotation", null},
            {"Offset", null}
        };

        public Dictionary<string, object> RightLeg = new() 
        {
            {"Rotation", null},
            {"Offset", null}
        };

        //declares which frame this keyframe is completely shown on
        public int frame = 0;

        public Keyframe(List<Dictionary<string, object>> list, int keyframeTime) 
        {
            Torso["Rotation"] = list[0]["Rotation"];
            Torso["Offset"] = list[0]["Offset"];

            LeftArm["Rotation"] = list[1]["Rotation"];
            LeftArm["Offset"] = list[1]["Offset"];

            RightArm["Rotation"] = list[2]["Rotation"];
            RightArm["Offset"] = list[2]["Offset"];

            LeftLeg["Rotation"] = list[3]["Rotation"];
            LeftLeg["Offset"] = list[3]["Offset"];

            RightLeg["Rotation"] = list[4]["Rotation"];
            RightLeg["Offset"] = list[4]["Offset"];

            frame = keyframeTime;
            
        }

        public static implicit operator List<object>(Keyframe v)
        {
            throw new NotImplementedException();
        }
    }
}