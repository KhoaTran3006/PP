using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public float followSpeed;
    public float maxOffset;

    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 5f;
        targetPosition = Camera.main.ScreenToWorldPoint(mousePos);

        targetPosition.x = Mathf.Clamp(targetPosition.x, -maxOffset * aspectRatio, maxOffset * aspectRatio);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -maxOffset * aspectRatio, maxOffset * aspectRatio);
        targetPosition.z = 0f;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }
}
