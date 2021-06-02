using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    [SerializeField] private float xMin, xMax, yMin, yMax;
    [SerializeField] private GameObject spider;
    private bool canSpawn = true;
    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(Spawn(2f));
    }

    private IEnumerator Spawn(float timer)
    {
        if (canSpawn)
        {
            canSpawn = false;

            int c = Random.Range(0, 3);

            if (c == 0)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), 12);
                GameObject go = Instantiate(spider, pos, Quaternion.identity);
            }

            if (c == 1)
            {
                Vector2 pos = new Vector2(15, Random.Range(yMin, yMax));
                GameObject go = Instantiate(spider, pos, Quaternion.identity);
            }

            if (c == 2)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), -12);
                GameObject go = Instantiate(spider, pos, Quaternion.identity);
            }

            if (c == 3)
            {
                Vector2 pos = new Vector2(-15, Random.Range(yMin, yMax));
                GameObject go = Instantiate(spider, pos, Quaternion.identity);
            }

            yield return new WaitForSeconds(timer);
            canSpawn = true;
        }
    }
}
