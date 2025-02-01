using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarGame {
    public class Combinations : MonoBehaviour {
        public  static List<KeyCode> stirringSequence = new List<KeyCode>{
        KeyCode.LeftArrow,
        KeyCode.UpArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
          KeyCode.LeftArrow,
        KeyCode.UpArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow
        };

        public static List<KeyCode> shakingSequence = new List<KeyCode>{
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow
        };  

    }
}