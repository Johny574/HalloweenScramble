



using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour {
    VisualElement _candy_e, _enemies_e, _score_e, _health_e;
    [SerializeField] VisualTreeAsset _heart_t;

    void Start() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _candy_e = root.Q<VisualElement>("CandyLabel");
        _enemies_e = root.Q<VisualElement>("EnemyLabel");
        _score_e = root.Q<VisualElement>("ScoreLabel");
        _health_e = root.Q<VisualElement>("Health");

        _candy_e.dataSource = CandyTracker.Instance;
        _enemies_e.dataSource = EnemyFactory.Instance;
        _score_e.dataSource = ScoreTracker.Instance;

        Health.Instance.HealthChanged += OnHealthChanged;

        foreach (var heart in _health_e.Children())
        {
            var start = Vector3.one;
            var end = new Vector3(1.2f, 1.2f, 1.2f);
            DOTween.To(() => start, s =>
            {
                start = s;
                heart.style.scale = new StyleScale(start);
            }, end, 1f).SetLoops(-1, LoopType.Restart);
        }
    }

    private void OnHealthChanged(int obj)
    {
        _health_e.Clear();

        for (int i = 0; i < obj; i++)
        {
            var heart = _heart_t.CloneTree();
            _health_e.Add(heart); 
            var start = Vector3.one;
            var end = new Vector3(1.1f, 1.1f, 1.1f);
            DOTween.To(() => start, s =>
            {
                start = s;
                heart.style.scale = new StyleScale(start);
            }, end, 1f).SetLoops(-1, LoopType.Restart);
        }
    }
}