using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace BarGame {
    public class TagUtils : MonoBehaviour {
        public const string BusyTagName = "Busy";
        public const string PlayerTagName = "Player";
        public const string BottleTagName = "Bottle";
        public const string WineTagName = "Wine";
        public const string ShakerTagName = "Shaker";
        public const string SpoonTagName = "Spoon";
        public const string GlassTagName = "Glass";
        public const string IceTagName = "Ice";

        public static bool IsShaker(GameObject obj) => obj.CompareTag(ShakerTagName);
        public static bool IsGlass(GameObject obj) => obj.CompareTag(GlassTagName);
        public static bool IsSpoon(GameObject obj) => obj.CompareTag(SpoonTagName);

        public static bool IsIngredient(GameObject obj)
        {
            foreach (string tagName in _ingredients)
            {
                if (obj.CompareTag(tagName))
                    return true;
            }
            return false;
        }

        private static List<string> _ingredients = new List<string>
        {
            BottleTagName,
            IceTagName,
            WineTagName
        };

        public static List<string> PickUps = new List<string>
        {
            BottleTagName, 
            ShakerTagName, 
            SpoonTagName, 
            GlassTagName,
            IceTagName,
            WineTagName,
        };






    }
}