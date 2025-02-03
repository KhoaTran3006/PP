using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    //public GameObject mainCamera;
    bool carrying;
    public GameObject carriedObject;
    public Camera cam;
    public GameObject carryPos;
    public float smoothTime;
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private float rayDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = GameObject.FindWithTag("MainCamera");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carrying)
        {
            Carrying(carriedObject);
            checkDrop();
            ThrowObject();
        }
        else
        {
            Pickup();
        }
    }
    void Carrying(GameObject o)
    {
        //o.GetComponent<Rigidbody>().isKinematic = true;
        o.transform.position = Vector3.Lerp (o.transform.position, carryPos.transform.position, Time.deltaTime * smoothTime);
    }
    void Pickup()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            
            Ray ray = cam.ScreenPointToRay(new Vector3(x,y));
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                Pickable p = hit.collider.GetComponent<Pickable>();
                if (p != null)
                {
                    carrying = true;
                    carriedObject = p.gameObject;
                    p.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
    void checkDrop()
    {
        if (Input.GetMouseButtonDown (1))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        carrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }

    void ThrowObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            carrying = false;
            carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            carriedObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
            carriedObject = null;
        }
    }
}
