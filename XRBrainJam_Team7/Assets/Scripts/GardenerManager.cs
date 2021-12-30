using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class GardenerManager : MonoBehaviour
{
    //UI Refs
    public List<Sprite> ActionIcons;
    public List<Sprite> PlantIcons;
    public Image activeActionImage;
    public GameObject activeActionReticle;
    
    //Plant Refs
    public List<GameObject> PlantPrefabs;
    public int currentPlantIndex;

    public List<int> PlantSeedCount;
    public List<int> PlantHarvestCount;
    int compostCount;

    
    public int seedsCollected;


    //Gardener Refs
    public enum GardenerState
    {
        BedPlace,
        Planting,
        Watering,
        Harvesting,
        Sharing,
        ReadRecipe,
    }

    public GardenerState gardenerStatus;
    public ARRepositionBed repositionScript;

    private void Start()
    {
        
    }

    public void SetGardenerState (int gardenerIndex)
    {
        switch (gardenerIndex)
        {
            case 0:
                //BedPlace
                gardenerStatus = GardenerState.BedPlace;
                repositionScript.enabled = true;
                break;

            case 1:
                //Planting
                repositionScript.enabled = false;

                gardenerStatus = GardenerState.Planting;
                activeActionImage.sprite = ActionIcons[gardenerIndex];
                iTween.ScaleFrom(activeActionReticle, iTween.Hash("time", 1.0f, "scale", Vector3.one * 1.2f, "easetype", iTween.EaseType.easeInOutElastic));

                break;

            case 2:
                //Watering
                repositionScript.enabled = false;
                
                gardenerStatus = GardenerState.Watering;
                activeActionImage.sprite = ActionIcons[gardenerIndex];
                iTween.ScaleFrom(activeActionReticle, iTween.Hash("time", 1.0f, "scale", Vector3.one * 1.2f, "easetype", iTween.EaseType.easeInOutElastic));
                break;

            case 3:
                //Harvesting
                repositionScript.enabled = false;
                
                gardenerStatus = GardenerState.Harvesting;
                activeActionImage.sprite = ActionIcons[gardenerIndex];
                iTween.ScaleFrom(activeActionReticle, iTween.Hash("time", 1.0f, "scale", Vector3.one * 1.2f, "easetype", iTween.EaseType.easeInOutElastic));
                break;

            case 4:
                //Sharing
                repositionScript.enabled = false;

                gardenerStatus = GardenerState.Sharing;
                activeActionImage.sprite = ActionIcons[gardenerIndex];
                iTween.ScaleFrom(activeActionReticle, iTween.Hash("time", 1.0f, "scale", Vector3.one * 1.2f, "easetype", iTween.EaseType.easeInOutElastic));
                break;

            case 5:
                //ReadRecipe
                repositionScript.enabled = false;
                gardenerStatus = GardenerState.ReadRecipe;
                activeActionImage.sprite = ActionIcons[gardenerIndex];
                iTween.ScaleFrom(activeActionReticle, iTween.Hash("time", 1.0f, "scale", Vector3.one * 1.2f, "easetype", iTween.EaseType.easeInOutElastic));
                break;
        }
    }

    public void SetCurrentPlant(int plantIndex)
    {
        currentPlantIndex = plantIndex;
        activeActionImage.sprite = PlantIcons[plantIndex];
        iTween.ScaleFrom(activeActionReticle, iTween.Hash("time", 1.0f, "scale", Vector3.one * 1.2f, "easetype", iTween.EaseType.easeInOutElastic));

    }
    public void PlantInBed(Transform plantSpot)
    {
        Instantiate(PlantPrefabs[currentPlantIndex], plantSpot.position, Quaternion.identity, plantSpot);
    }
}
