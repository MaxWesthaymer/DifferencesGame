using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    #region InspectorFields
    [SerializeField] private Text doneScoreTxt;
    [SerializeField] private Text wrongScoreTxt;
    [SerializeField] private Text levelTxt;
    #endregion
    
    #region PrivateFields
    private GameController _gameController;
    #endregion

    #region UnityMethods
    private void Start()
    {
        _gameController = GameController.Instance;
        _gameController.onDataChange += UpdateUi;
    }
    #endregion
    
    #region PrivateMethods
    private void UpdateUi()
    {
        doneScoreTxt.text = $"{_gameController.DoneScore}";
        wrongScoreTxt.text = $"{_gameController.WrongScore}";
        levelTxt.text = $"Уровень {_gameController.CurrentLevel + 1}";
    }
    #endregion
}
