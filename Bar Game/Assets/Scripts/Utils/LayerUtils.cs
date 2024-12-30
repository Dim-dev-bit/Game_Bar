using UnityEngine;

namespace BarGame {
    public static class LayerUtils {
        public const string PlayerLayerName = "player";
        public const string PickUpLayerName = "pickUp";
        public const string FreeChairLayerName = "freeChair";
        public const string BusyChairLayerName = "busyChair";

        public const int BusyChairLayer = 9;

        public static readonly int PlayerLayer = LayerMask.GetMask(PlayerLayerName);
        public static readonly int PickUpLayer = LayerMask.GetMask(PickUpLayerName);
        public static readonly int FreeChairLayer = LayerMask.GetMask(FreeChairLayerName);

    }
}