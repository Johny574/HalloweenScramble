




using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HelpPanel : MonoBehaviour {
    [SerializeField] VisualTreeAsset _leaderboardPanel_t, _leaderboardEntry;
    VisualElement _leaderboardPanel_e, _mainMenu_e;
    void Awake() {
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
         _leaderboardPanel_e.style.visibility = Visibility.Hidden;
    }

}