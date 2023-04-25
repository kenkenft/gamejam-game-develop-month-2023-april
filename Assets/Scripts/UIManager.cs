using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, Canvas> _canvasesDict = new Dictionary<string, Canvas>(){};
    private Canvas _playerOverlayCanvas, _titleCanvas, _pauseCanvas, _resultsCanvas;
    private List<Canvas> _canvasList = new List<Canvas>();

    void OnEnable()
    {
        
    }

    void Start()
    {
        Canvas[] tempCanvasArray = GetComponentsInChildren<Canvas>();
        foreach(Canvas canvas in tempCanvasArray)
        {
            CheckWhichCanvas(canvas);    
        }

        // Debug.Log(_canvasesDict.Count + "," + _canvasesDict["TitleCanvas"]);
        Debug.Log(_canvasList.Count);

        ToggleCanvas("ResultsCanvas");
    }

    void CheckWhichCanvas(Canvas canvas)
    {
        switch(canvas.gameObject.name)
        {
            case "PlayerOverlayCanvas":
            {
                // _canvasesDict.Add("playerOverlayCanvas", canvas);
                _playerOverlayCanvas = canvas;
                _canvasList.Add(_playerOverlayCanvas);
                break;
            }
            case "TitleCanvas":
            {
                // _canvasesDict.Add("titleCanvas", canvas);
                _titleCanvas = canvas;
                _canvasList.Add(_titleCanvas);
                break;
            }
            case "PauseCanvas":
            {
                // _canvasesDict.Add("pauseCanvas", canvas);
                _pauseCanvas = canvas;
                _canvasList.Add(_pauseCanvas);
                break;
            }
            case "ResultsCanvas":
            {
                // _canvasesDict.Add("resultsCanvas", canvas);
                _resultsCanvas = canvas;
                _canvasList.Add(_resultsCanvas);
                break;
            }
            default:
                break;
        }
    }
    

    void ToggleCanvas(string targetCanvasName)
    {
        // Toggles off other canvases except target canvas
        foreach(Canvas canvas in _canvasList)
        {
            if(canvas.gameObject.name == targetCanvasName)
                canvas.gameObject.SetActive(true);
            else
                canvas.gameObject.SetActive(false);
        }

    }
    //ToDo method to Toggle active state of canvas
    //ToDo method to Toggle active state of buttons
    //ToDo method to start game and SetUp initial game state
    //ToDo method to show instruction text
    //ToDo method to show credit text
    //ToDo method to show results menu on timer expiring
    //ToDo method to return player back to menu and clear player data
    //ToDo method to detect pause button press

}
