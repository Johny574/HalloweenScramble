


using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour {

    [SerializeField] VisualTreeAsset _gameOver_t;
    VisualElement gameoverscreen_e;
    Label _runScore, _highScore;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        gameoverscreen_e = _gameOver_t.CloneTree().Children().First();
        gameoverscreen_e.style.visibility = Visibility.Hidden;

        var retrybutton = gameoverscreen_e.Q<Button>("Retry");
        var upload = gameoverscreen_e.Q<Button>("Upload");
        var exitbutton = gameoverscreen_e.Q<Button>("Exit");

        retrybutton.clicked += () => GameManager.Instance.StartNewGame();
        exitbutton.clicked += () => Application.Quit();

        _runScore = gameoverscreen_e.Q<Label>("Runscore");
        _highScore = gameoverscreen_e.Q<Label>("Highscore");
            
        root.Add(gameoverscreen_e);
        GameManager.Instance.OnStartNewGame += () => gameoverscreen_e.style.visibility = Visibility.Hidden;
        Health.Instance.Death += OnDeath;
    }

    private async void OnDeath()
    {
        gameoverscreen_e.style.visibility = Visibility.Visible;
        _runScore.text = $"Your current score was {ScoreTracker.Instance.Score}";
        var lastScore = await Leaderboard.Instance.GetPlayerScore();
        if (lastScore == null)
            return;
        
        if (ScoreTracker.Instance.Score > lastScore.Score)
        {
            _highScore.text = $"{lastScore.PlayerName} Your score was updated to {ScoreTracker.Instance.Score}";
            Leaderboard.Instance.AddScore(ScoreTracker.Instance.Score);
        }
        else
            _highScore.text = $"{lastScore.PlayerName} Your highest score is {lastScore.Score}";
    }
}