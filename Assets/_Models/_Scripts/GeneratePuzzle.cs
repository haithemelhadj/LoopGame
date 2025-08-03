using System.Collections.Generic;
using UnityEngine;


public class GeneratePuzzle : MonoBehaviour
{

    //    [Header("Settings")]
    //    public GameObject[] centerObjectList;
    //    public GameObject centerObject;
    //    public float spacing = 1f;
    //    public float scaleFactor = 0.2f;
    //    public bool scalePositiveWithDistance = true;

    //    private Vector3 lineDirection;
    //    [Header("Settings")]
    //    //public GameObject centerObject;
    //    //public Vector3 lineDirection = Vector3.right;
    //    //public float spacing = 1f;
    //    //public float scaleFactor = 0.1f;
    //    public float baseScale = 1;

    //    void Start()
    //    {
    //        centerObject = centerObjectList[Random.Range(0, centerObjectList.Length)];
    //        centerObject = Instantiate(centerObject, Vector3.zero, Quaternion.identity);

    //        if (centerObject == null)
    //        {
    //            Debug.LogError("Center Object is not assigned.");
    //            return;
    //        }

    //        SpreadChildren();
    //    }

    //    void SpreadChildren()
    //    {
    //        // Normalize the direction to ensure consistency
    //        Vector3 dir = lineDirection.normalized;

    //        // Get children of the center object
    //        List<Transform> children = new List<Transform>();
    //        foreach (Transform child in centerObject.transform)
    //        {
    //            children.Add(child);
    //        }

    //        for (int i = 0; i < children.Count; i++)
    //        {
    //            Transform child = children[i];

    //            // Position along the line in order
    //            Vector3 newPosition = centerObject.transform.position + dir * spacing * i;
    //            child.position = newPosition;

    //            // Keep local rotation
    //            child.localRotation = child.localRotation;

    //            // Scale based on index
    //            float scale = baseScale + (i * scaleFactor);
    //            child.localScale = Vector3.Scale(child.localScale,Vector3.one * scale/100);
    //        }
    //    }
    //}

    [Header("Settings")]
    //public GameObject[] centerObjectList;
    public GameObject centerObject;
    public float spacing = 1f;
    public float scaleFactor = 0.1f;
    public bool scalePositiveWithDistance = true;

    private Vector3 lineDirection;

    void Start()
    {
        //centerObject = centerObjectList[Random.Range(0, centerObjectList.Length)];
        //centerObject = Instantiate(centerObject, Vector3.zero, Quaternion.identity);

        if (centerObject == null)
        {
            Debug.LogError("Center Object is not assigned.");
            return;
        }

        SpreadChildren();
    }

    void SpreadChildren()
    {
        // Generate a random direction
        lineDirection = Random.onUnitSphere.normalized;

        // Get children of center object
        List<Transform> children = new List<Transform>();
        foreach (Transform child in centerObject.transform)
        {
            children.Add(child);
        }

        //// Sort children based on current distance to center (optional)
        //children.Sort((a, b) =>
        //    Vector3.Distance(centerObject.transform.position, a.position)
        //    .CompareTo(Vector3.Distance(centerObject.transform.position, b.position))
        //);

        //int midIndex = children.Count / 2;

        for (int i = 0; i < children.Count; i++)
        {
            Transform child = children[i];

            // Calculate offset based on index relative to center
            int offsetIndex = i * Random.Range(i,10);
            Vector3 newPosition = centerObject.transform.position + lineDirection * offsetIndex * spacing;
            child.position = newPosition;

            // Maintain local rotation
            child.localRotation = child.localRotation;

            // Distance from center
            float distance = Mathf.Abs(offsetIndex * spacing);

            // Scale based on distance
            float scaleAmount = scalePositiveWithDistance ? 1 + (distance * scaleFactor) : 1 - (distance * scaleFactor);
            scaleAmount = Mathf.Max(0.01f, scaleAmount); // Prevent negative/zero scale
            child.localScale = child.localScale+Vector3.one * scaleAmount;
        }
    }
}

