using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorite : MonoBehaviour
{
    [SerializeField] GameObject meteorite;
    [SerializeField] float spawnRangeHorizontal;
    [SerializeField] float spawnRangeVertical;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Spawn Meteorite");
            
            //Instantiate(meteorite,new Vector3 (this.transform.position.x + spawnRangeHorizontal, Random.Range(-spawnRangeVertical, spawnRangeVertical),0),Quaternion.identity);
            Instantiate(meteorite, new Vector3(this.transform.position.x + spawnRangeHorizontal, this.transform.position.y + Random.Range(-transform.localScale.y/2, transform.localScale.y/2), 0), Quaternion.identity);
            Debug.Log(transform.localScale.y);

        }
    }
}
