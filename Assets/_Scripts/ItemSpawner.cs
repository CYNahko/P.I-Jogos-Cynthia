using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject itemPrefab;
    public float intervalo = 30f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalo)
        {
            SpawnItem();
            timer = 0;
        }
    }

    void SpawnItem()
    {
        float x = Random.Range(-6.2f, 6.2f); 
        float y = Random.Range(-3.2f, 3.2f);
    
        Instantiate(itemPrefab, new Vector2(x, y), Quaternion.identity);
    }
}
