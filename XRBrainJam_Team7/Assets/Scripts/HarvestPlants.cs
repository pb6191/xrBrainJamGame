using UnityEngine;
using System.Collections.Generic;

public class HarvestPlants : MonoBehaviour
{

    public GardenerManager gardenerManager;
    public GameObject fruitCrate;
    public Transform crateSpotRoot;

    private List<Transform> CrateSpots = new List<Transform>();
    private int spotIndex;


    public void onHarvestTapped ()
    {
        fruitCrate.SetActive(!fruitCrate.activeSelf);

    }

    private void Start ()
    {
        foreach (Transform child in crateSpotRoot)
        {
            CrateSpots.Add(child);
        }
    }
    
       // Update is called once per frame
    void Update()
    {
        if (gardenerManager.gardenerStatus != GardenerManager.GardenerState.Harvesting)
        {
            return;
        }

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray,out hit, Mathf.Infinity, LayerMask.GetMask("Plants")))
                {
                    var hitObject = hit.collider.gameObject.GetComponentInParent<BasicPlant>().gameObject;
                    hitObject.transform.SetParent(fruitCrate.transform, true);

                    var destination = pickCratePosition();
                    // iTween.MoveTo(hitObject, destination, 0.450f);
                    iTween.MoveTo(hitObject, iTween.Hash("position", destination, "islocal", true, "time", 0.950f, "easetype", iTween.EaseType.easeInOutElastic));
                }

            }
        }
    }

    private Vector3 pickCratePosition()
    {
        var thisSpotIndex = spotIndex;
        spotIndex = (spotIndex + 1) % CrateSpots.Count;
        return CrateSpots[thisSpotIndex].localPosition;
    }

//    private IEnumerator AnimatePlantToBasket(GameObject plant, float durationMs = 450f)
//    {
//        var frames = durationMs / 16; // ~16ms / frame
//        var destination = fruitCrate.transform.position;
//        for (var i=0; i < 100; ++i)
//        {
//            float delta = speed * Time.deltaTime;
//            Vector3 currentPosition = gameObject.transform.position;
//            Vector3 nextPosition = Vector3.MoveTowards(currentPosition, destination, delta);
//            gameObject.transform.position = nextPosition;
//            yield return new WaitForEndOfFrame ();
//        }
//    }
}
