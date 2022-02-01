using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject objectToSpawn;

    [SerializeField]
    GameObject parent;

    [SerializeField]
    int numberToSpawn = 0;

    [SerializeField]
    float positionXModifier = 2.0f;

    [SerializeField]
    float positionYModifier = 2.0f;

    [SerializeField]
    float spawnRateModifier = 2.0f;

    [SerializeField]
    float spawnRate = 3.0f;

    [SerializeField]
    float startSpawnDelay = 3.0f;

    [SerializeField]
    EventManager EventManager;

    bool spawning = true;

    Coroutine spawnCoroutine;

    void Awake()
    {
        if (EventManager == null)
        {
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
            EventManager = gameController?.GetComponent<EventManager>();

            if (EventManager == null)
            {
                Debug.LogError($"Event manager reference missing on {gameObject.name}!", this);
            }
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(EventConstants.OnGameOver, StopSpawning);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventConstants.OnGameOver, StopSpawning);
    }

    // Update is called once per frame
    void Start()
    {
        Invoke("StartSpawnCoroutine", startSpawnDelay);
    }

    void StartSpawnCoroutine()
    {
        spawnCoroutine = StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        if (numberToSpawn > 0)
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                Instantiate(objectToSpawn, new Vector3(transform.position.x + GetModifier(positionXModifier), transform.position.y + GetModifier(positionYModifier)), Quaternion.identity, parent == null ? transform : parent.transform);
                yield return new WaitForSeconds(spawnRate + GetModifier(spawnRateModifier));
            }
        }
        else
        {
            while (spawning)
            {
                Instantiate(objectToSpawn, new Vector3(transform.position.x + GetModifier(positionXModifier), transform.position.y + GetModifier(positionYModifier)), Quaternion.identity, parent == null ? transform : parent.transform);
                yield return new WaitForSeconds(spawnRate + GetModifier(spawnRateModifier));
            }
        }
    }

    float GetModifier(float modifier)
    {
        return Random.Range(-modifier, modifier);
    }

    void StopSpawning()
    {
        StopCoroutine(spawnCoroutine);
    }
}
