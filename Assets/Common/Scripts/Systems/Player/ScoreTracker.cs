public class ScoreTracker : Singleton<ScoreTracker> {
    public int Score = 0;
    public string HUDDisplay;
    void Start()
    {
        HUDDisplay = $"Score {Score}";
        CandyTracker.Instance.CandyDropped += OnCandyDropped;
    }

    private void OnCandyDropped(int amount)
    {
        Score += amount;
        HUDDisplay = $"Score {Score}";
    }


}