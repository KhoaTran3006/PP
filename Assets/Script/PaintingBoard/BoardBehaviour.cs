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
        private List<BoardConfig.PaintColor> _colorStored;
        public IList<BoardConfig.PaintColor> ColorStoredList => _colorStored;

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
            Debug.Log($"Board:{info.Name}");
            string s = "";
            foreach (var i in info.PaintColorsNeeded)
            {
                s += $"{i}/";
            }
            Debug.Log($"Color needed: {s}");
            ResetCheckState();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var paint = collision.transform.GetComponent<IPainting>();
            if (paint != null && paint.Usable && _colorPools != null)
            {
                checkColor(paint.Color);
                paint.UsePaint();
            }
        }

        private void checkColor(BoardConfig.PaintColor color)
        {
            _paintCount++;
            _colorStored.Add(color);

            if (_colorPools.Contains(color))
            {
                _colorPools.Remove(color);
                Debug.Log($"Pools: {_colorPools.Count}");
            }
            if (_paintCount == _info.PaintColorsNeeded.Count) //reach maximum paint
            {
                if (_colorPools.Count == 0)
                {
                    Debug.Log("Success");
                    OnSuceess?.Invoke(this);
                }
                else
                {
                    Debug.Log("Fail");
                    OnFail?.Invoke(this);
                }
            }
        }

        public void ResetCheckState()
        {
            _paintCount = 0;
            _colorPools = new List<BoardConfig.PaintColor>(_info.PaintColorsNeeded);
            _colorStored = new List<BoardConfig.PaintColor>();
        }
    }
}
