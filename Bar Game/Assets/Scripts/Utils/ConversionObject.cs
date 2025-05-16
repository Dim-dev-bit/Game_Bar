using BarGame.Utils;
using UnityEngine;

namespace Assets.Scripts.Utils {
    [CreateAssetMenu(menuName = "Items/ConversionObject")]
    public class ConversionObject : ScriptableObject {
        public Conversion[] conversions;
    }
}