using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private Dictionary<string, Canvas> _canvasesDict = new Dictionary<string, Canvas>(){};
    private Canvas _playerOverlayCanvas, _titleCanvas, _pauseCanvas, _resultsCanvas;
    private List<Canvas> _canvasList = new List<Canvas>();
    public Image ResultsPanelImage;

    bool _isPlaying = false, _isPaused = false;

    public Sprite[] EndResultImages;

    [SerializeField] private GameObject[] _instructionTextArray;
    private int _textIndexPointer = 0; 
    [SerializeField] private Text[] _resultsUITextArray;

    [HideInInspector] public delegate void OnPlayButtonClicked();
    [HideInInspector] public static OnPlayButtonClicked StartGameSetUp;
    [HideInInspector] public delegate int OnEndgGameTriggered();
    [HideInInspector] public static OnEndgGameTriggered GetFinalScore;

    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;
    
    void OnEnable()
    {
        PlayerControl.TogglePauseUI += PauseGame;
        PlayerControl.CheckIsPlaying += GetIsPlaying;
        Timer.CheckIsPaused += GetIsPaused;
        Timer.TriggerEndGame += TriggerEndGame;
    }
    void Disable()
    {
        PlayerControl.TogglePauseUI -= PauseGame;
        PlayerControl.CheckIsPlaying += GetIsPlaying;
        Timer.CheckIsPaused -= GetIsPaused;
        Timer.TriggerEndGame -= TriggerEndGame;
    }

    void Start()
    {
        SetCanvasRefs();            
        TriggerTitleCanvas();
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
        Time.timeScale = 1;
        Debug.Log("Play button pressed!");
        ToggleCanvas("PlayerOverlayCanvas");
        
        _isPlaying = true;
        _isPaused = false;
        StartGameSetUp?.Invoke();
        PlaySFX?.Invoke("deposit");
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
            if(!_isPaused)
            {    
                _pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                _isPaused = true;
                PlaySFX?.Invoke("coinPickup");
            }
            else
            {
                _pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                _isPaused = false;
                PlaySFX?.Invoke("coinPickup");
            }
    }

    public bool GetIsPaused()
    {
        return _isPaused;
    }

    public bool GetIsPlaying()
    {
        return _isPlaying;
    }
    
    public void TriggerEndGame()
    {
        int score = GetFinalScore.Invoke(), colorTagIndex = 0;
        string[] colorTag = {"<color=#000000>", "<color=#ffffff>", "<color=#ffffff>"};
        string tempString = score.ToString(), results; 
        ToggleCanvas("ResultsCanvas");
        _isPlaying = false;
        
        if(score > 14000)
        {
            colorTagIndex = 0;
            results = "A";
            PlaySFX?.Invoke("resultsBest");
        }
        else if(score > 0 && score <= 14000)
        {    
            colorTagIndex = 2;
            results = "B";
            PlaySFX?.Invoke("resultsAverage");
        }
        else
        {    
            results = "F";
            colorTagIndex = 1;
            PlaySFX?.Invoke("resultsFail");
        }

        ResultsPanelImage.sprite = EndResultImages[colorTagIndex];
        _resultsUITextArray[0].text = colorTag[colorTagIndex] + tempString + "</color>";
        _resultsUITextArray[1].text = colorTag[colorTagIndex] + results + "</color>";
        _resultsUITextArray[2].text = colorTag[colorTagIndex] + _resultsUITextArray[2].text.ToString() + "</color>";
        _resultsUITextArray[3].text = colorTag[colorTagIndex] + _resultsUITextArray[3].text.ToString() + "</color>";
        _resultsUITextArray[4].text = colorTag[colorTagIndex] + _resultsUITextArray[4].text.ToString() + "</color>";
        // _resultsUITextArray[5].text = colorTag[colorTagIndex] + _resultsUITextArray[5].text.ToString() + "</color>";
        // _resultsUITextArray[6].text = colorTag[colorTagIndex] + _resultsUITextArray[6].text.ToString() + "</color>";

        //ToDo Set endscreen graphics based on score.
        //ToDo Play Audio based on score
    }

    public void TriggerTitleCanvas()
    {
        ToggleCanvas("TitleCanvas");
        _isPlaying = true;
        _isPaused = false;
        _textIndexPointer = 0;
        AdvanceInstructionText();
        PlaySFX?.Invoke("coinPickup");
    }

}
