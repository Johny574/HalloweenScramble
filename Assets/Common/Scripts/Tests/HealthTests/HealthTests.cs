



using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HealthTests  {
    GameObject player;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
         var task = SceneManager.LoadSceneAsync("SampleScene");
        yield return new WaitUntil(() => task.isDone);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    [UnityTest]
    public IEnumerator HeartRemovedOnDamage()
    {
        Health.Instance.Damage(Vector2.zero);
        Assert.AreNotEqual(Health.Instance.Hearts, 3);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DiedOnZeroHearts()
    {
        Health.Instance.TestDamage(3);
        
        Assert.False(player.gameObject.activeSelf);
        yield return null;
    }

}