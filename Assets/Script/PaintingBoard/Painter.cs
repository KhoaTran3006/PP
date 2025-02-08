using System.Collections;
using UnityEngine;

namespace PaintingBoard
{
    public class Painter : MonoBehaviour, IPainting
    {
        [field: SerializeField] public BoardConfig.PaintColor Color { get; private set; }

        private Vector3 _originPos;

        public bool Pickable { get; private set; }

        private void Awake()
        {
            _originPos = transform.position;    
        }

        public void Pickup()
        {
            Pickable = false;
        }

        public void ResetPainter()
        {
            transform.position = _originPos;
            Pickable = true;
        }
    }
}