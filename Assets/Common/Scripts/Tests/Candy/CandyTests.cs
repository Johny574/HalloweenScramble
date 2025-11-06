using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CandyTests : MonoBehaviour
{
    Candy candy;
    GameObject player;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        var task = SceneManager.LoadSceneAsync("SampleScene");
        yield return new WaitUntil(() => task.isDone);
        candy = GameObject.FindAnyObjectByType<Candy>();
        player = GameObject.FindGameObjectWithTag("Player");
        CandyTracker.Instance.OnCandyCollected();
        Assert.NotZero(CandyTracker.Instance.Collected);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CandyCollectedChangesCandyCount()
    {
        while (candy.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(1f);
            var dif = candy.transform.position - player.transform.position;
            player.transform.position += dif * 1f;
        }

        Assert.NotZero(CandyTracker.Instance.Collected);
    }

    [UnityTest]
    public IEnumerator CandyResetOnDrop()
    {
        CandyTracker.Instance.DropCandy();
        Assert.Zero(CandyTracker.Instance.Collected);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator ScoredIncreasedOnDrop() {

        CandyTracker.Instance.DropCandy();
        Assert.NotZero(ScoreTracker.Instance.Score);
        yield return null;
    }
    
    [UnityTearDown]
    public IEnumerator TearDown() {
        yield return null;
    }
}
