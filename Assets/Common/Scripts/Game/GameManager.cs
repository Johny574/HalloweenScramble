using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>{
    public Action OnStartNewGame;
    public async void StartNewGame()
    {
        await SceneLoader.Instance.LoadScene("Game");
    }

    public async void ReturnToMainMenu()
    {
        await SceneLoader.Instance.LoadScene("MainMenu");
    }
}