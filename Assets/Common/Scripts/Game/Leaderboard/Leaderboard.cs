
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Exceptions;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class Leaderboard : Singleton<Leaderboard> {
    const string LeaderboardId = "Score";
    protected override void Awake()
    {
        base.Awake();
        AwakeAsync();
    }


    async void AwakeAsync()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }


    public async void AddScore(int score)
    {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
    }

    public async Task<List<LeaderboardEntry>> GetScores()
    {
        var options = new GetScoresOptions
        {
            IncludeMetadata = true
        };
        LeaderboardScoresPage scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, options);
        return scoresResponse.Results;
    }

    public async Task<LeaderboardEntry>GetPlayerScore()
    {
        try
        {
        var entry = await LeaderboardsService.Instance.GetPlayerScoreAsync("your_leaderboard_id");
        return entry;
        }
        catch
        {
            return null;
        }
    }
}