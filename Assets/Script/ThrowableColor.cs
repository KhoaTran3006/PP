using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableColor : MonoBehaviour
{
    public Color colorValue;
    public Vector3 ogPos;


    // Start is called before the first frame update
    void Start()
    {
        colorValue = new Color(colorValue.r, colorValue.g, colorValue.b, 1f);
        ogPos = transform.position; 
    }

    // Update is called once per frame
    public void ResetPosition()
    {
        Debug.Log($"Resetting {gameObject.name} | Active: {gameObject.activeSelf}");

        Rigidbody rb = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();

        if ( rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (col != null )
        {
            col.enabled = false;
        }

        transform.position = ogPos;
        transform.rotation = Quaternion.identity;
        Debug.Log($"Reactivating {gameObject.name} before SetActive: {gameObject.activeSelf}");
        gameObject.SetActive(true);
        Debug.Log($"Reactivating {gameObject.name} after SetActive: {gameObject.activeSelf}");
        Invoke(nameof(ReEnablePhysics), 0.5f);
    }

    private void ReEnablePhysics()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();   

        if ( rb != null )
        {
            rb.isKinematic = false;
        }
        if ( col != null )
        {
            col.enabled = true;
        }
    }

    

    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogWarning($"{gameObject.name} was disabled! Re-enabling...");
            gameObject.SetActive(true);
        }
    }
}
