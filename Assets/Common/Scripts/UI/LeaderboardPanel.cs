using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] VisualTreeAsset _leaderboardPanel_t, _leaderboardEntry;
    VisualElement _leaderboardPanel_e, _mainMenu_e;
    ListView _list;

    void Awake() {
        AwakeAsync();
    }

    async void AwakeAsync()
    {
        UIDocument _document = GetComponent<UIDocument>();
        var root = _document.rootVisualElement;
        _leaderboardPanel_e = _leaderboardPanel_t.CloneTree().Children().First();
        _mainMenu_e = root.Q<VisualElement>("MainMenu");
        root.Add(_leaderboardPanel_e);

        var backbutton = _leaderboardPanel_e.Q<Button>("Back");

        backbutton.clicked += () =>
        {
            _leaderboardPanel_e.style.visibility = Visibility.Hidden;
            _mainMenu_e.style.visibility = Visibility.Visible;
        };

        _list = _leaderboardPanel_e.Q<ListView>("List");
        _list.makeItem = () => _leaderboardEntry.CloneTree();
        _leaderboardPanel_e.style.visibility = Visibility.Hidden;
    }

    public async void Open()
    {
        _leaderboardPanel_e.style.visibility = Visibility.Visible;
        var scores = await Leaderboard.Instance.GetScores();
        _list.itemsSource = scores;

        _list.bindItem = (element, i) =>
        {
            var rank = element.Q<Label>("Rank");
            var name = element.Q<Label>("Name");
            var score = element.Q<Label>("Score");
            rank.text = (i + 1).ToString();
            name.text = scores[i].PlayerName;
            score.text = scores[i].Score.ToString();
        };
    }
}