using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
//
// This script allows us to create anchors with
// a prefab attached in order to visbly discern where the anchors are created.
// Anchors are a particular point in space that you are asking your device to track.
//

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class AnchorCreator : MonoBehaviour
{
    public UICanvus uICanvus;
    private int level;
    // This is the prefab that will appear every time an anchor is created.
    [SerializeField]
    GameObject m_AnchorPrefab;

    public GameObject AnchorPrefab
    {
        get => m_AnchorPrefab;
        set => m_AnchorPrefab = value;
    }

    public GameObject player;


    // On Awake(), we obtains a reference to all the required components.
    // The ARRaycastManager allows us to perform raycasts so that we know where to place an anchor.
    // The ARPlaneManager detects surfaces we can place our objects on.
    // The ARAnchorManager handles the processing of all anchors and updates their position and rotation.
    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_AnchorManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        level = 0;
    }
    public void SetLevel(int l) { 
        this.level = l;
        switch (this.level) {
            case 1: SetLevelOne();  break;
            case 0: SetLevelZero();  break;
            default: break;
        }
    }

    private void SetLevelZero() {
        uICanvus.ShowOKBtn();
        anchorObject.SetActive(true);
    }
    private void SetLevelOne()
    {
        uICanvus.HideOKBtn();
        anchorObject.SetActive(false);
    }

    void Update()
    {
        if (level > 0) {
            return;
        }
        // If there is no tap, then simply do nothing until the next call to Update().
        if (Input.touchCount == 0)
            return;
        // 만일 UI 오브젝트가 터치대상이 되었을 경우 이 경우에도 별도의 처리를 AnchorCreator.cs에선 안함.
        if (EventSystem.current.currentSelectedGameObject)
            return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended && anchorObject != null)
        {
            ShowPlayer(anchorObject.transform.position);
        }
        else
        {
            SetAnchorPos(touch);
        }


    }

    private void ShowPlayer(Vector3 spawnPos) {
        if (player == null) {
            return;
        }
        if (player.activeSelf == false)
        {
            player.SetActive(true);
        }
        player.transform.position = spawnPos;
    }

    private void SetAnchorPos(Touch touch) {
        if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            var hitTrackableId = s_Hits[0].trackableId;
            var hitPlane = m_PlaneManager.GetPlane(hitTrackableId);

            // This attaches an anchor to the area on the plane corresponding to the raycast hit,
            // and afterwards instantiates an instance of your chosen prefab at that point.
            // This prefab instance is parented to the anchor to make sure the position of the prefab is consistent
            // with the anchor, since an anchor attached to an ARPlane will be updated automatically by the ARAnchorManager as the ARPlane's exact position is refined.
            anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);


            if (anchor == null)
            {
                Debug.Log("Error creating anchor.");
            }
            else
            {
                if (anchorObject == null)
                {
                    anchorObject = Instantiate(m_AnchorPrefab, anchor.transform);
                }
                else
                {
                    anchorObject.transform.position = anchor.transform.position;
                }
            }
        }
    }

    private GameObject anchorObject;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARAnchor anchor;

    ARRaycastManager m_RaycastManager;

    ARAnchorManager m_AnchorManager;

    ARPlaneManager m_PlaneManager;
}
