using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasColorChecker : MonoBehaviour
{
    [System.Serializable]
    public class ColorCombination
    {
        public List<Color> requiredColors;
        public GameObject picturePrefab;
    }

    public List<ColorCombination> possibleCombinations;
    public Transform spawnPoint;

    [SerializeField]
    private List<Color> collectedColors = new List<Color>();
    private List<ThrowableColor> collectedColorObjects = new List<ThrowableColor>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        ThrowableColor colorObject = other.GetComponent<ThrowableColor>();
        
        if (colorObject != null )
        {
            AddColor(colorObject);
            colorObject.gameObject.SetActive(false); //Hide the color temporarily
        }
    }
    public void AddColor(ThrowableColor colorObject)
    {
        collectedColors.Add(colorObject.colorValue);
        collectedColorObjects.Add(colorObject);
        Debug.Log($"Added color: {colorObject.colorValue} | Total collected: {collectedColorObjects.Count}");
        CheckCombination();
    }

    private void CheckCombination()
    {
        foreach (var combination in possibleCombinations)
        {
            if (IsMatchingCombination(combination.requiredColors))
            {
                Instantiate(combination.picturePrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log("Colors Matched! Picture should spawn");
                ResetColors(true);
                return;
            }
        }

        if (collectedColors.Count >= 3)
        {
            ResetColors(false);
        }
    }

    private bool IsMatchingCombination(List<Color> requiredColors)
    {
        Debug.Log("Comparing Collected Colors with Required Colors...");
        if (collectedColorObjects.Count != requiredColors.Count)
        {
            Debug.Log("Mismatch in color count. Expected: " + requiredColors.Count + ", Got: " + collectedColors.Count);
            return false;
        }

        List<Color> tempCollected = new List<Color>(collectedColors);
        
        foreach (var requiredColor in requiredColors)
        {
            bool matchFound = false;

            for (int i = 0; i < tempCollected.Count; i++)
            {
                if (AreColorsEqual(requiredColor, tempCollected[i]))
                {
                    tempCollected.RemoveAt(i); //remove matched color to prevent duplicates
                    matchFound = true;
                    break;
                }
                
            }

            if (!matchFound)
            {
                Debug.Log("Missing Color: " + requiredColor);
                return false;

            }
            
        }
        Debug.Log("Colors Matched!");
        return true;
    }

    private bool AreColorsEqual(Color a, Color b)
    {
        //Debug.Log($"Comparing Colors -> Required: {a} (RGB: {a.r}, {a.g}, {a.b}), Collected: {b} (RGB: {b.r}, {b.g}, {b.b}))");
        float tolerance = 0.05f;

        return Mathf.Abs(a.r - b.r) < tolerance &&
            Mathf.Abs(a.g - b.g) < tolerance &&
            Mathf.Abs(a.b - b.b) < tolerance;
    }

    private void ResetColors(bool success)
    {
        Debug.Log($"resetting {collectedColorObjects.Count} colors");
        foreach (var colorObject in collectedColorObjects)
        {
            if (colorObject != null)
            {
                Debug.Log($"Reactivating {colorObject.gameObject.name}");
                StartCoroutine(DelayedReactivation(colorObject));
            }
            //Debug.Log($"Before SetActive: {colorObject.gameObject.name} isActive = {colorObject.gameObject.activeSelf}");
            //colorObject.gameObject.SetActive(true);
            colorObject.ResetPosition();
            //Debug.Log($"Resetting color: {colorObject.colorValue} at {colorObject.ogPos}");
                                  
        }

        collectedColors.Clear();
        collectedColorObjects.Clear();

        if (!success)
        {
            //Debug.Log("Combination failed, Returning colors");
        }
    }

    private IEnumerator DelayedReactivation(ThrowableColor colorObject)
    {
        yield return new WaitForSeconds(0.1f); 
        Debug.Log($"Final Reactivation {colorObject.gameObject.name} | Before: {colorObject.gameObject.activeSelf}");
        colorObject.gameObject.SetActive(true);
        Debug.Log($"Final Reactivation {colorObject.gameObject.name} | After: {colorObject.gameObject.activeSelf}");
    }


    // Start is called before the first frame update
    //void Start()
    //{
    //    
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //if (success)
    //{
    //    colorObject.ResetPosition();
    //    colorObject.gameObject.SetActive(true);
    //}
    //
    //else
    //{
    //    colorObject.ResetPosition();
    //    colorObject.gameObject.SetActive(true);
    //}
    //}
}
