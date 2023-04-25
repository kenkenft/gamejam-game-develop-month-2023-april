using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, Canvas> _canvasesDict = new Dictionary<string, Canvas>(){};
    private Canvas _playerOverlayCanvas, _titleCanvas, _pauseCanvas, _resultsCanvas;
    private List<Canvas> _canvasList = new List<Canvas>();

    bool _isPlaying = false, _isPaused = false;

    [SerializeField] private GameObject[] _instructionTextArray;
    private int _textIndexPointer = 0; 

    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;

    void OnEnable()
    {
        PlayerControl.TogglePauseUI += PauseGame;
    }
    void Disable()
    {
        PlayerControl.TogglePauseUI -= PauseGame;
    }

    void Start()
    {
        SetCanvasRefs();            
        ToggleCanvas("TitleCanvas");
        AdvanceInstructionText();
    }

    void SetCanvasRefs()
    {
        Canvas[] tempCanvasArray = GetComponentsInChildren<Canvas>();
        foreach(Canvas canvas in tempCanvasArray)
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

    public void AdvanceInstructionText()
    {
        if(_textIndexPointer >= _instructionTextArray.Length-1)
            _textIndexPointer = 0;
        else
            _textIndexPointer++;
        EnableInstructionText(_textIndexPointer);
    }

    void EnableInstructionText(int targetTextNum)
    {
        for(int i = 0; i < _instructionTextArray.Length; i++)
        {
            if(i == targetTextNum)
                _instructionTextArray[i].SetActive(true);
            else
                _instructionTextArray[i].SetActive(false);
        }
        PlaySFX?.Invoke("coinPickup");
    }

    public void PlayGame()
    {
        Debug.Log("Play button pressed!");
        ToggleCanvas("PlayerOverlayCanvas");
        _isPlaying = true;
        _isPaused = false;
        // Reset player score to zero, 
        // reset play speed to base default, 
        // set and start timer, 
        // empty player coinstorage, 
        // empty coin pool,
        // Remove coin prefabrications 
        // Spawn player in start location
    }

    public void PauseGame()
    {
        if(_isPlaying)
        {    
            if(!_isPaused)
            {    
                _pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                _isPaused = true;
            }
            else
            {
                _pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                _isPaused = false;
            }
        }
    }
    
    //ToDo invoke sound effects on button press
    //ToDo method to Toggle active state of buttons <-- Might be redundant
    //ToDo method to show results menu on timer expiring
    //ToDo method to return player back to menu and clear player data
    //ToDo method to enable and disable pause ui
    //ToDo method that invokes enable/disable pause ui from PlayerControl

}
