using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlant : MonoBehaviour
{
    public enum plantState
    {
        Seed,
        Seedling,
        Young,
        Mature,
        Harvested,
        Dead,
    }

    public plantState localPlantStatus;

    //Growth Cycle 
    bool watered;
    int growthProgress;
    public int growthRate;
    public int foodCount;
    public int seedCount;
    public int compostCount;


    //Audio and Visual Refs
    public List<GameObject> PlantMeshes;
    public List<AudioClip> PlantSounds;

    private GardenerManager gardenerManager;
    AudioSource plantVoice;

    private void Start()
    {
        plantVoice = gameObject.GetComponent<AudioSource>();
        gardenerManager = GameObject.FindObjectOfType<GardenerManager>();
    }

    private void OnMouseUp()
    {
        switch (gardenerManager.gardenerStatus)
        {
            case GardenerManager.GardenerState.BedPlace:

                break;

            case GardenerManager.GardenerState.Planting:

                plantPhaseShift();

                break;

            case GardenerManager.GardenerState.Watering:

                plantPhaseShift();

                break;

            case GardenerManager.GardenerState.Harvesting:



                break;

            case GardenerManager.GardenerState.Sharing:

                break;

            case GardenerManager.GardenerState.ReadRecipe:

                //Bring up Info or Recipe

                break;
        }
        
    }


    public void plantPhaseShift()
    {
        switch (localPlantStatus)
        {
            case plantState.Seed:

                Destroy(this.gameObject);  // after removing dead plant, soil appears, then if you tap again, the plant is removed from Garden Bed
                /*
                localPlantStatus = plantState.Seedling;
                PlantMeshUpdate(1);
                iTween.ScaleFrom(PlantMeshes[1], iTween.Hash("time", 1.0f, "scale", Vector3.one * 0.5f, "easetype", iTween.EaseType.easeInOutElastic));
                plantVoice.PlayOneShot(PlantSounds[1]);
                */
                break;

            case plantState.Seedling:
                localPlantStatus = plantState.Young;
                PlantMeshUpdate(2);
                iTween.ScaleFrom(PlantMeshes[2], iTween.Hash("time", 1.0f, "scale", Vector3.one * 0.5f, "easetype", iTween.EaseType.easeInOutElastic));

                plantVoice.PlayOneShot(PlantSounds[2]);

                break;

            case plantState.Young:
                localPlantStatus = plantState.Mature;
                PlantMeshUpdate(3);
                iTween.ScaleFrom(PlantMeshes[3], iTween.Hash("time", 1.0f, "scale", Vector3.one * 0.5f, "easetype", iTween.EaseType.easeInOutElastic));

                plantVoice.PlayOneShot(PlantSounds[3]);

                break;

            case plantState.Mature:
                localPlantStatus = plantState.Harvested;
                PlantMeshUpdate(4);
                //iTween.ScaleFrom(PlantMeshes[4], iTween.Hash("time", 1.0f, "scale", Vector3.one * 0.5f, "easetype", iTween.EaseType.easeInOutElastic));

                plantVoice.PlayOneShot(PlantSounds[4]);

                break;

            case plantState.Harvested:
                localPlantStatus = plantState.Dead;
                PlantMeshUpdate(5);
                //iTween.ScaleFrom(PlantMeshes[1], iTween.Hash("time", 1.0f, "scale", Vector3.one * 0.5f, "easetype", iTween.EaseType.easeInOutElastic));

                plantVoice.PlayOneShot(PlantSounds[5]);

                break;

            case plantState.Dead:
                
                localPlantStatus = plantState.Seedling;
                PlantMeshUpdate(0);
                iTween.ScaleFrom(PlantMeshes[0], iTween.Hash("time", 1.0f, "scale", Vector3.one * 0.5f, "easetype", iTween.EaseType.easeInOutElastic));

                plantVoice.PlayOneShot(PlantSounds[0]);

                break;
        }
    }

    IEnumerator GrowthCycle()
    {
        while (growthProgress < 100)
        {
            growthProgress += growthRate;
            //Visual Update of growth progress here:

            yield return new WaitForSeconds(1.0f);
        }
    }

    void GrowthCycleReset()
    {
        growthProgress = 0;
        growthRate = 0;
    }

    public void PlantMeshUpdate(int meshIndex)
    {
        foreach(GameObject plant in PlantMeshes)
        {
            plant.SetActive(false);
        }
        PlantMeshes[meshIndex].SetActive(true);
    }
}
