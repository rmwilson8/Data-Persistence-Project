using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;
    [SerializeField] private Text _gameOverText;

    private MainManager _mainManager;

    private void Start()
    {
        _mainManager = GameObject.FindFirstObjectByType<MainManager>();
        SetHighScoreText();
    }
    private void OnEnable()
    {
        if(_mainManager != null)
        {
            _mainManager.OnScoreIncrease += MainManager_OnScoreIncrease;
            _mainManager.OnGameOver += MainManager_OnGameOver;
        }
    }

    private void OnDisable()
    {
        if(_mainManager != null)
        {
            _mainManager.OnScoreIncrease -= MainManager_OnScoreIncrease;
            _mainManager.OnGameOver -= MainManager_OnGameOver;   
        }
    }

    private void SetHighScoreText()
    {
        if(PersistentData.Instance.HighScore == 0)
        {
            _highScoreText.text = PersistentData.Instance.CurrentName;
        }

        else
        {
            _highScoreText.text = "High Score: " + PersistentData.Instance.HighScoreName + " : " + PersistentData.Instance.HighScore;
        }
    }

    private void MainManager_OnScoreIncrease(object sender, EventArgs e)
    {
        _scoreText.text = _mainManager.GetScore().ToString();
    }

    private void MainManager_OnGameOver(object sender, EventArgs e)
    {
        _gameOverText.enabled = true;
    }
}
