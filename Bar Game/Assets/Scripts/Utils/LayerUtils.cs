using UnityEngine;

namespace BarGame {
    public static class LayerUtils {
        public const string PlayerLayerName = "player";
        public const string PickUpLayerName = "pickUp";
        public const string FreeChairLayerName = "freeChair";
        public const string BusyChairLayerName = "busyChair";
        public const string TableLayerName = "table";
        public const string PickedUpLayerName = "pickedUp";

        public const int BusyChairLayerNum = 9;
        public const int PickUpLayerNum = 7;
        public const int PickedUpLayerNum = 11;

        public static readonly int PlayerLayer = LayerMask.GetMask(PlayerLayerName);
        public static readonly int PickUpLayer = LayerMask.GetMask(PickUpLayerName);
        public static readonly int FreeChairLayer = LayerMask.GetMask(FreeChairLayerName);
        public static readonly int TableLayer = LayerMask.GetMask(TableLayerName);

    }
}