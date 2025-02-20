using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintingBoard;

public class PickupObject : MonoBehaviour
{
    //public GameObject mainCamera;
    bool carrying;
    public IPickable carriedObject;

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
            Carrying(carriedObject.GameObject);
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
                IPickable p = hit.collider.GetComponent<IPickable>();
                if (p != null)
                {
                    carrying = true;
                    carriedObject = p;
                    p.Pickup();
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
        carriedObject.Drop();
        carriedObject = null;
    }

    void ThrowObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            carrying = false;
            carriedObject.GameObject.GetComponent<Rigidbody>().isKinematic = false;
            carriedObject.GameObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
            carriedObject = null;
        }
    }
}
