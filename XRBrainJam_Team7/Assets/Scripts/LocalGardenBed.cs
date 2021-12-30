using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LocalGardenBed : MonoBehaviour
{
   
    public Transform plantSpotRoot;
    public List<Transform> PlantSpots;
    int spotIndex;
    public GardenerManager localGardener;

    private void Start()
    {
        localGardener = GameObject.Find("AR Session Origin").GetComponent<GardenerManager>();

        foreach (Transform child in plantSpotRoot)
        {
            PlantSpots.Add(child);
        }
    }

    private void OnMouseUp()
    {
        if(localGardener.gardenerStatus == GardenerManager.GardenerState.Planting && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            if (spotIndex < PlantSpots.Count)
            {
                if (PlantSpots[spotIndex].childCount == 0)
                {
                    localGardener.PlantInBed(PlantSpots[spotIndex]);
                    spotIndex++;
                }

            }
            else if(spotIndex >= PlantSpots.Count)
            {
                FillEmptySpots();
            }
        }
                
                
    }

    void FillEmptySpots()
    {
        List<Transform> openSpots = new List<Transform>();

        foreach(Transform spot in PlantSpots)
        {
            if (spot.childCount == 0)
            {
                openSpots.Add(spot);
            }
        }

        if(openSpots.Count > 0)
        {
            localGardener.PlantInBed(openSpots[0]);
        }
    }
}
