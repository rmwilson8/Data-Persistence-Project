using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _highScoreText;
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] Button _quitButton;
    [SerializeField] Button _resetButton;
    private async void OnEnable()
    {
        _quitButton.onClick.AddListener(PersistentData.Instance.SaveGameData);
    }

    private void OnDisable()
    {
        _quitButton.onClick.RemoveListener(PersistentData.Instance.SaveGameData);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetHighScoreText();
    }

    private void SetHighScoreText()
    {
        if (PersistentData.Instance.HighScoreName == null) 
        {
            _highScoreText.text = "Enter Name To Play";
        }

        else
        {
            _highScoreText.text ="High Score: " + PersistentData.Instance.HighScoreName + " : " + PersistentData.Instance.HighScore;
        }
    }

    public void StartGameButtonClicked()
    {
        if(string.IsNullOrEmpty(_inputField.text)) { return; } // only starts game if player has typed their name
        PersistentData.Instance.StartGame(_inputField.text);
    }

    public void QuitGameButtonClicked()
    {
        PersistentData.Instance.QuitGame();
    }

    public void DeleteGameButtonClicked()
    {
        PersistentData.Instance.DeleteGameData();
        SetHighScoreText();
    }
}
