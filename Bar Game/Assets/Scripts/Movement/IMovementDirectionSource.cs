using UnityEngine;

namespace BarGame.Movement {
    public interface IMovementDirectionSource {
        Vector2 MovementDirection {  get; }
    }
}