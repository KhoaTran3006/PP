using System;
using System.Collections.Generic;
using UnityEngine;

namespace PaintingBoard
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "Project/BoardConfig", order = 0)]

    public class BoardConfig: ScriptableObject
    {
        public List<BoardInfo> Boards;

        [Serializable]
        public class BoardInfo
        {
            public string Name;
            public List<PaintColor> PaintColorsNeeded;
        }

        public enum PaintColor
        { 
            Red,
            Green,
            Blue,
            Yellow
        }
    }


}
