using System.Collections.Generic;
using UnityEngine;

namespace BarGame {
    public class TagUtils : MonoBehaviour {
        public const string BusyTagName = "Busy";
        public const string PlayerTagName = "Player";
        public const string BottleTagName = "Bottle";
        public const string ShakerTagName = "Shaker";
        public const string SpoonTagName = "Spoon";
        public const string GlassTagName = "Glass";

        public static List<string> PickUps = new List<string>
        {
            BottleTagName, 
            ShakerTagName, 
            SpoonTagName, 
            GlassTagName
        };






    }
}