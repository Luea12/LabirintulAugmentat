using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARController : MonoBehaviour
{

    [SerializeField]
    private string[] prefabNames;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager m_TrackedImageManager;

    private void Awake() 
    {
        m_TrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach(string prefabName in prefabNames)
        {
            GameObject go = GameObject.Find(prefabName);
            if(go != null)
            {
                go.SetActive(false);
            }
            spawnedPrefabs.Add(prefabName, go);
        }
    }

    // Start is called before the first frame update
    private void Start() {}

    // Update is called once per frame
    private void Update()
    {
        foreach(string prefabName in prefabNames)
        {
            if(spawnedPrefabs[prefabName] == null)
            {
                Debug.Log(prefabName);
                GameObject go = GameObject.Find(prefabName);
                if(go != null)
                {
                    go.SetActive(false);
                }
                spawnedPrefabs[prefabName] = go;
            }
        }
    }

    private void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            ActivatePrefabs(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated)
        {
            ActivatePrefabs(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.removed)
        {
            DeactivatePrefabs(trackedImage);
        }
    }

    private void ActivatePrefabs(ARTrackedImage trackedImage)
    {
        Vector3 position = trackedImage.transform.position;
        foreach(GameObject go in spawnedPrefabs.Values)
        {
            go.transform.position = position;
            go.SetActive(true);
        }
    }

    private void DeactivatePrefabs(ARTrackedImage trackedImage)
    {
        foreach(GameObject go in spawnedPrefabs.Values)
        {
            go.SetActive(false);
        }
    }


}



