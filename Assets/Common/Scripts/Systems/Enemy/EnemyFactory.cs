
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFactory : Factory<EnemyFactory, SpawnData>
{
    [SerializeField] List<Transform> _spawns;

    [Header("Amount")]
    [SerializeField] int _minSpawn = 1, _maxSpawn = 2;
    [Header("Rate")]
    [SerializeField] float _spawnDuration = 5;
    float _spawnTimer = 30;
    public string HUDDisplay;
    bool _spawning = true;
    [SerializeField] int limit = 3;

    void Start() {
        Health.Instance.Death += OnDeath;
        CandyTracker.Instance.CandyDropped += Kill;
        HUDDisplay = $"Enemies {Pool.ActiveCount()}";
    }

    private void OnDeath()
    {
        _spawning = false;
        Pool.Clear(); 
    }

    public async void SpawnEnemy(SpawnData spawnData)
    {
        var enemy = await GetObject(spawnData);
        HUDDisplay = $"Enemies {Pool.ActiveCount()}";
    }

    void Update()
    {
        if (Pool.ActiveCount() >= limit)
            return;

        if (!_spawning)
            return;

        if (_spawnTimer < _spawnDuration)
            _spawnTimer += Time.deltaTime;

        else
        {
            _spawnTimer = 0;
            int random = Random.Range(_minSpawn + 1, _maxSpawn);

            for (int i = 0; i < random; i++)
            {
                Transform spawn = _spawns[Random.Range(0, _spawns.Count)];
                SpawnData spawnData = new SpawnData(spawn.transform.position, spawn.transform.forward, Vector3.one, true, transform);
                SpawnEnemy(spawnData);
            }
        }
    }

    public void Kill(int amount)
    {
        var activeObjects = Pool.Objects.Where(x => x.gameObject.activeSelf.Equals(true));
        int i = 0;
        foreach (var obj in activeObjects)
        {
            if (i >= amount)
                break;

            obj.GetComponent<Enemy>().Kill();
            i++;
        }
    }

}