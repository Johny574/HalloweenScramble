



using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {

    [SerializeField] LeaderboardPanel _leaderboardPanel;
    void Start() {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button startnewgameButton = root.Q<Button>("Play");
        Button leaderBoardButton = root.Q<Button>("Leaderboard");
        Button creditsButton = root.Q<Button>("Credits");
        Button helpButton = root.Q<Button>("Help");
        Button exitgameButton = root.Q<Button>("Exit");

        VisualElement mainMenu = root.Q<VisualElement>("MainMenu");
        VisualElement helpMenu = root.Q<VisualElement>("HelpMenu");
        VisualElement creditsMenu = root.Q<VisualElement>("CreditsMenu");

        startnewgameButton.clicked += GameManager.Instance.StartNewGame;

        leaderBoardButton.clicked += () =>
        {
            _leaderboardPanel.Open();
            mainMenu.style.visibility = Visibility.Hidden;
        };

        creditsButton.clicked += () =>
        {
            creditsMenu.style.visibility = Visibility.Visible;
            mainMenu.style.visibility = Visibility.Hidden;
        };

        helpButton.clicked += () =>
        {
            helpMenu.style.visibility = Visibility.Visible;
            mainMenu.style.visibility = Visibility.Hidden;
        };

        exitgameButton.clicked += () => Application.Quit();
    }
}