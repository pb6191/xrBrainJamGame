using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ARRepositionBed : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;

    public GameObject plantBed;
    private float InitialAngle;
    private Vector2 InitialPosition;

    private bool IsDragging;
    private bool IsRotating;

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        var bed = plantBed;
        if (!bed) return; // Not placed yet

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast (ray, out hit, Mathf.Infinity, LayerMask.GetMask("SceneObject")))
                {
                    IsRotating = false;
                    IsDragging = true;
                }
                else
                {
                    IsDragging = false;
                    IsRotating = true;
                    InitialPosition = Input.GetTouch(0).position;
                    InitialAngle = plantBed.transform.localRotation.eulerAngles.y;
                }
            }
            else if (IsDragging)
            {
                MoveObject();
            }
            else if (IsRotating)
            {
                RotateObject();
            }

        }

    }

    private void MoveObject()
    {
        var finger = Input.GetTouch(0);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        _arRaycastManager.Raycast(finger.position, hitResults);
        if (hitResults.Count >0)
        {
            Pose pose = hitResults[0].pose;
            plantBed.transform.position = pose.position;
        }
    }

    private void RotateObject()
    {
        var CurrentPosition = Input.GetTouch(0).position;
        var NewAngle = InitialAngle + (InitialPosition - CurrentPosition).x;
        plantBed.transform.localRotation = Quaternion.Euler(0, NewAngle, 0);
    }

}
