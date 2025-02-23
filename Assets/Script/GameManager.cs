using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using P = PaintingBoard;

public class GameManager : MonoBehaviour
{
    [SerializeField] private P.BoardBehaviour _board;

    [SerializeField] private P.Painter[] _painterList;
    private Dictionary<P.BoardConfig.PaintColor, P.Painter> _painterDict;

    [SerializeField] private P.BoardConfig _config;
    [SerializeField] private Transform _picutreTrans;

    [SerializeField] private P.TaskDetail _taskDetail;

    private Queue<P.BoardConfig.BoardInfo> _currentTask;

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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab)) _taskDetail.gameObject.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Tab)) _taskDetail.gameObject.SetActive(true);
    }

    private void startGame()
    {
        _currentTask = new Queue<P.BoardConfig.BoardInfo>();

        //select board to spawn
        //var boardC = Random.Range(0, 4); //from 1 to 3, change value here
        var boardC = _board;
        for (int i = 0; i < 4; i++)
        {
            var boardInfo = getRandomBoard();
            _currentTask.Enqueue(boardInfo);
        }

        _board.OnFail += resetPainter;
        _board.OnSuceess += onBoardSuccess;
        showNextTask();

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

        if (behaviour.Info.Prefab != null)
        {
            Instantiate(behaviour.Info.Prefab, _picutreTrans);
        }

        showNextTask();
    }

    private void showNextTask()
    {
        if (_currentTask.Count == 0)
        {
            //dosomething

            return;
        }
        var task = _currentTask.Dequeue();
        _board.SetupBoard(task);
        _taskDetail.Display(task);
        _taskDetail.gameObject.SetActive(true);
        
    }
}