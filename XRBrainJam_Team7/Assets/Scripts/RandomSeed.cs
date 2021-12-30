using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeed : MonoBehaviour
{
    public int maxSeeds = 10;
    public int seedCounter = 0;
    public List<GameObject> BasicSeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeCheck());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
    var camera = Camera.main.transform;
    int someNum = Random.Range(0, BasicSeed.Count);
    var positionOffset = new Vector3(Random.Range(-100, 100), -camera.position.y, Random.Range(-100, 100));
   
    Instantiate(BasicSeed[someNum], camera.position + positionOffset, Quaternion.identity);
    seedCounter = seedCounter + 1;
    }

IEnumerator TimeCheck() 
{
    while(seedCounter < maxSeeds) 
    { 
        //adding 1 seed every 3 minutes
        Spawn();
        yield return new WaitForSeconds(180f);
    }
}

}
