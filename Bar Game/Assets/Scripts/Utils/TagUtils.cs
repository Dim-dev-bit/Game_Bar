using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace BarGame {
    public class TagUtils : MonoBehaviour {
        public const string BusyTagName = "Busy";

        public const string PlayerTagName = "Player";
        public const string CustomerTagName = "Customer";

        public const string AppleJuiceTagName = "AppleJuice";
        public const string OrangeJuiceTagName = "OrangeJuice";
        public const string StrawberryJuiceTagName = "StrawberryJuice";

        public const string WineTagName = "Wine";
        public const string ShakerTagName = "Shaker";
        public const string SpoonTagName = "Spoon";
        public const string GlassTagName = "Glass";
        public const string IceTagName = "Ice";


        public const string StrawberryTagName = "Strawberry";
        public const string OrangeTagName = "Orange";
        public const string AppleTagName = "Apple";

        public static bool IsShaker(GameObject obj) => obj.CompareTag(ShakerTagName);
        public static bool IsGlass(GameObject obj) => obj.CompareTag(GlassTagName);
        public static bool IsSpoon(GameObject obj) => obj.CompareTag(SpoonTagName);
        public static bool IsIce(GameObject obj) => obj.CompareTag(IceTagName);


        public static bool IsIngredient(GameObject obj)
        {
            foreach (string tagName in _ingredients)
            {
                if (obj.CompareTag(tagName))
                    return true;
            }
            return false;
        }

        public static bool IsFruit(GameObject obj)
        {
            foreach (string tagName in _fruits)
            {
                if (obj.CompareTag(tagName))
                    return true;
            }
            return false;
        }

        private static List<string> _fruits = new List<string>
        {
            StrawberryTagName,
            OrangeTagName,
            AppleTagName
        };

        private static List<string> _ingredients = new List<string>
        {
            AppleJuiceTagName,
            OrangeJuiceTagName,
            StrawberryJuiceTagName,
            IceTagName,
            WineTagName
        };

        public static List<string> PickUps = new List<string>
        {
            AppleJuiceTagName, 
            OrangeJuiceTagName,
            StrawberryJuiceTagName,
            ShakerTagName, 
            SpoonTagName, 
            GlassTagName,
            IceTagName,
            WineTagName,
            StrawberryTagName,
            OrangeTagName,
            AppleTagName
        };






    }
}