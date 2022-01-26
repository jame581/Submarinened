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

    bool spawning = true;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(SpawnObject());
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
}
