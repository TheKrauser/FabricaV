using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class SpiderSpawn : MonoBehaviour
{
    [SerializeField] private float xMin, xMax, yMin, yMax;
    [SerializeField] private GameObject spider;
    private bool canSpawn = true;
    private float spawnRate = 3f;
    private List<GameObject> spiders = new List<GameObject>();

    public static int spidersKilled;
    private bool timelinePlayed = false;
    public static bool playerCanLose;
    [SerializeField] private PlayableDirector director;
    void Start()
    {
        playerCanLose = true;
    }

    void Update()
    {
        StartCoroutine(Spawn(spawnRate));

        if (spidersKilled > 35 && !timelinePlayed)
        {
            timelinePlayed = true;
            director.Play();
            playerCanLose = false;
        }
    }

    private IEnumerator Spawn(float timer)
    {
        if (canSpawn)
        {
            canSpawn = false;
            spawnRate -= 0.05f;

            int c = Random.Range(0, 3);

            if (c == 0)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), 12);
                GameObject go = Instantiate(spider, pos, Quaternion.identity);
                spiders.Add(go);
            }

            if (c == 1)
            {
                Vector2 pos = new Vector2(15, Random.Range(yMin, yMax));
                GameObject go = Instantiate(spider, pos, Quaternion.identity);
                spiders.Add(go);
            }

            if (c == 2)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), -12);
                GameObject go = Instantiate(spider, pos, Quaternion.identity);
                spiders.Add(go);
            }

            if (c == 3)
            {
                Vector2 pos = new Vector2(-15, Random.Range(yMin, yMax));
                GameObject go = Instantiate(spider, pos, Quaternion.identity);
                spiders.Add(go);
            }

            yield return new WaitForSeconds(timer);
            canSpawn = true;
        }
    }

    public void DestroySpiders()
    {
        foreach(GameObject go in spiders)
        {
            Destroy(go);
        }
    }
}
