using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, Canvas> _canvasesDict = new Dictionary<string, Canvas>(){};
    private Canvas _playerOverlayCanvas, _titleCanvas, _pauseCanvas, _resultsCanvas;
    private List<Canvas> _canvasList = new List<Canvas>();

    private GameObject _creditsMenu, _instructionsMenu;

    private bool _isCreditMenuEnabled = false;

    void OnEnable()
    {
        
    }

    void Start()
    {
        SetCanvasRefs();    

        SetTitleGameObjectRefs();
        ToggleCanvas("TitleCanvas");
        SwitchMainTitleScreen();
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

    void SetTitleGameObjectRefs()
    {
        Transform[] tempArray = _titleCanvas.gameObject.GetComponentsInChildren<Transform>();
        Debug.Log("tempArray.Length: " + tempArray.Length);
        foreach(Transform transform in tempArray)
        {
            switch(transform.gameObject.name)
            {
                case "CreditsMenu":
                {
                    _creditsMenu = transform.gameObject;
                    Debug.Log("CreditsMenu found! " + _creditsMenu.name);
                    break;
                }
                case "InstructionMenu":
                {
                    _instructionsMenu = transform.gameObject;
                    Debug.Log("InstructionMenu found! " + _instructionsMenu.name);
                    break;
                }
                default:
                    break;
            }
        }

    }
    public void SwitchMainTitleScreen()
    {
        // Inverts the active status for appropriate ui elements to switch between the credits text and instruction text on the title ui
        _creditsMenu.SetActive(!_isCreditMenuEnabled);
        _instructionsMenu.SetActive(_isCreditMenuEnabled);

        _isCreditMenuEnabled = !_isCreditMenuEnabled;
        
    }
    
    //ToDo separate instruction text into two sections
    //ToDo method to Toggle active state of buttons <-- Might be redundant
    //ToDo method to start game and SetUp initial game state
    //ToDo method to show results menu on timer expiring
    //ToDo method to return player back to menu and clear player data
    //ToDo method to enable and disable pause ui
    //ToDo method that invokes enable/disable pause ui from PlayerControl

}
