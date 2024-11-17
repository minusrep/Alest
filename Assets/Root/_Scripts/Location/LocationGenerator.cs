using System.Collections.Generic;
using UnityEngine;

public class LocationGenerator : MonoBehaviour
{
    private List<GameObject> _rooms;

    [SerializeField] private GameObject _spawnRoom;

    [SerializeField] private GameObject _finalRoom;

    [SerializeField] private List<GameObject> _roomPrefabs;

    [SerializeField] private List<GameObject> _enemyPrefabs;

    [SerializeField] private Vector3 _startPoint;

    [SerializeField] private float _roomSize;

    [SerializeField] private int _roomCount;

    [SerializeField] private float _enemySpawnStep;

    [SerializeField] private Transform _parent;


    private void Start()
        => Init();

    public void Init()
    {
        _rooms = new List<GameObject>();

        Invoke();
    }

    private void Invoke()
    {
        CreateStart();

        CreateContent();

        CreateFinal();

        SpawnEnemies();
    }

    private void CreateStart()
        => CreateRoom(_spawnRoom);
    private void CreateContent()
    {
        for (int i = 0; i < _roomCount; i++)
            CreateRoom();
    }

    private void CreateFinal()
        => CreateRoom(_finalRoom);

    private void CreateRoom()
        => CreateRoom(_roomPrefabs[Random.Range(0, _roomPrefabs.Count)]);

    private void CreateRoom(GameObject room)
    {
        var position = Vector3.zero;

        position.x += _rooms.Count * _roomSize;

        var result = Instantiate(room, position, Quaternion.identity, _parent);

        _rooms.Add(result);
    }

    private void SpawnEnemies()
    {
        var prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];

        for(float i = _roomSize; i < (_roomSize * _roomCount); i += _enemySpawnStep)
        {
            var position = Vector3.zero;

            position.y = 4f;

            position.x = i;

            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
