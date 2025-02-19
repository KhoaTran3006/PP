﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using P = PaintingBoard;

namespace Assets.Script
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private P.BoardBehaviour[] _boardList;

        [SerializeField] private P.Painter[] _painterList;
        private Dictionary<P.BoardConfig.PaintColor, P.Painter> _painterDict;

        [SerializeField] P.BoardConfig _config;

        private List<P.BoardConfig.BoardInfo> _currentBoard;

        private void Awake()
        {
            _painterDict = new Dictionary<P.BoardConfig.PaintColor, P.Painter>();
            foreach (var color in _painterList)
            {
                _painterDict[color.Color] = color;
            }
        }

        private void Start()
        {
            startGame();
        }

        private void startGame()
        {
            _currentBoard = new List<P.BoardConfig.BoardInfo>();

            //select board to spawn
            //var boardC = Random.Range(0, 4); //from 1 to 3, change value here
            var boardC = _boardList.Length;
            for (int i = 0; i < boardC; i++)
            {
                var boardInfo = getRandomBoard();
                _currentBoard.Add(boardInfo);
                _boardList[i].SetupBoard(boardInfo);
                _boardList[i].OnFail += resetPainter;
                _boardList[i].OnSuceess += onBoardSuccess;
            }

            P.BoardConfig.BoardInfo getRandomBoard()
            {
                var idx = Random.Range(0, _config.Boards.Count);
                return _config.Boards[idx];
            }
        }

        private void resetPainter(P.BoardBehaviour board)
        {
            StartCoroutine(cor());

            //use coroutine because somehow the last painter cannot reset in the same frame lead to weird bug
            IEnumerator cor()
            {
                yield return new WaitForEndOfFrame();
                var colors = board.ColorStoredList;
                foreach (var color in colors)
                {
                    _painterDict[color].ResetPainter();
                }
                board.ResetCheckState();
            }
        }

        private void onBoardSuccess(P.BoardBehaviour behaviour)
        {
            resetPainter(behaviour);
            //render picture or do something like check task lisk
        }
    }
}