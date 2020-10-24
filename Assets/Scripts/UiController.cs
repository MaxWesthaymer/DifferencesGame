using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private Text doneScoreTxt;
    [SerializeField] private Text wrongScoreTxt;
    [SerializeField] private Text levelTxt;
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameController.Instance;
        _gameController.onDataChange += UpdateUi;
    }
    public void UpdateUi()
    {
        doneScoreTxt.text = $"{_gameController.DoneScore}";
        wrongScoreTxt.text = $"{_gameController.WrongScore}";
        levelTxt.text = $"Уровень {_gameController.CurrentLevel + 1}";
    }
}
