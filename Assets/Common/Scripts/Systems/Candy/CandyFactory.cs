


using System.Collections.Generic;
using UnityEngine;

public class CandyFactory : Factory<CandyFactory, SpawnData>
{
     [SerializeField] List<Transform> _spawns;
     [Header("Amount")]
    [SerializeField] int _amount = 1;
     [Header("Rate")]
    [SerializeField] float _spawnDuration = 5;
    float _spawnTimer = 30;
    bool _spawning = true;
    [SerializeField] GameObject _pointer;
    GameObject _player;
    [SerializeField] int limit = 3;

    public async void SpawnCandy(SpawnData spawnData)
    {
        var candy = await GetObject(spawnData);
        var po = GameObject.Instantiate(_pointer);
        var pointer = po.GetComponent<Pointer>();
        ((MonoBehaviour)candy).GetComponent<Candy>().AssignPointer(pointer);
        pointer.Bind(_player, spawnData.Position);
    }

      void Start() {
        Health.Instance.Death += OnDeath;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnDeath()
    {
        _spawning = false;
        Pool.Clear(); 
    }

    void Update() {
        if (Pool.ActiveCount() >= limit || !CandyTracker.Instance.CanCollect())
            return;

        if (_spawnTimer < _spawnDuration)
            _spawnTimer += Time.deltaTime;

        else
        {
            _spawnTimer = 0;

            for (int i = 0; i < _amount; i++)
            {
                Transform spawn = _spawns[Random.Range(0, _spawns.Count)];
                SpawnData spawnData = new SpawnData(spawn.transform.position, spawn.transform.forward, Vector3.one, true, transform);
                SpawnCandy(spawnData);
            }
        }
    }
}