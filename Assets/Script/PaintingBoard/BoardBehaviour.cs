using System;
using System.Collections.Generic;
using UnityEngine;

namespace PaintingBoard
{
    public class BoardBehaviour : MonoBehaviour
    {
        private BoardConfig.BoardInfo _info;
        public BoardConfig.BoardInfo Info => _info;

        private List<BoardConfig.PaintColor> _colorPools;
        private int _paintCount;

        public event Action<BoardBehaviour> OnSuceess;
        public event Action<BoardBehaviour> OnFail;

        private enum CheckState
        { 
            NotChecked,
            Valid,
            NotValid
        }

        public void SetupBoard(BoardConfig.BoardInfo info)
        {
            _info = info;
            clearState();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var paint = GetComponent<IPainting>();
            if (paint != null && _colorPools != null)
            {
                checkColor(paint.Color);
            }
        }

        private void checkColor(BoardConfig.PaintColor color)
        {
            _paintCount++;
            if (_colorPools.Contains(color))
            {
                _colorPools.Remove(color);
            }
            if (_paintCount == _info.PaintColorsNeeded.Count) //reach maximum paint
            {
                if (_colorPools.Count == 0)
                {
                    OnSuceess?.Invoke(this);
                }
                else
                {
                    clearState();
                    OnFail?.Invoke(this);
                }
            }
        }

        private void clearState()
        {
            _paintCount = 0;
            _colorPools = new List<BoardConfig.PaintColor>(_info.PaintColorsNeeded);
        }
    }
}
