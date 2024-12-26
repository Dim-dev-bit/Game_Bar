using UnityEngine;

namespace BarGame {
    public static class LayerUtils {
        public const string PlayerLayerName = "player";
        public const string PickUpLayerName = "pickUp";

        public static readonly int PlayerLayer = LayerMask.GetMask(PlayerLayerName);
        public static readonly int PickUpLayer = LayerMask.GetMask(PickUpLayerName);


    }
}