using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public GameObject mainCamera;
    bool carrying;
    public GameObject carriedObject;
    public Camera cam;
    public GameObject carryPos;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carrying)
        {
            Carrying(carriedObject);
        }
        else
        {
            Pickup();
        }
    }
    void Carrying(GameObject o)
    {
        o.GetComponent<Rigidbody>().isKinematic = true;
        o.transform.position = carryPos.transform.position;
    }
    void Pickup()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            
            Ray ray = cam.ScreenPointToRay(new Vector3(x,y));
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                Pickable p = hit.collider.GetComponent<Pickable>();
                if (p != null)
                {
                    carrying = true;
                    carriedObject = p.gameObject;
                }
            }
        }
    }
}
