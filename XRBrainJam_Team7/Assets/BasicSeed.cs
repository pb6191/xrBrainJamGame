using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSeed : MonoBehaviour
{
    private GardenerManager gardenerManager;

    // Start is called before the first frame update
    void Start()
    {
        gardenerManager = GameObject.FindObjectOfType<GardenerManager>();
        gardenerManager.seedsCollected = 0;
        StartCoroutine(UpdateSeedCount());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator UpdateSeedCount() 
{
    while(true) 
    { 
        //update seed count every 3 seconds
        if (gardenerManager.gardenerStatus == GardenerManager.GardenerState.Harvesting) {
            gardenerManager.seedsCollected = gardenerManager.seedsCollected + 1;
        }

        yield return new WaitForSeconds(3f);
    }
}

}
