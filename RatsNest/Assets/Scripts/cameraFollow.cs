using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public bool useOffsetValue;
    public float rotateSpeed;
    public float maxViewAngle = 45f;
    public float minViewAngle = -45f;

    public Transform pivot;
    public bool invertYAxis;


    void Start()
    {
        if(!useOffsetValue)
        {
            offset = target.position - transform.position;
        }

        pivot.position = target.position;
        pivot.transform.parent = target;

        Cursor.lockState = CursorLockMode.Locked;
    }


    void LateUpdate(){

        //Get X position of mouse and rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;   
        target.Rotate(0, horizontal, 0);

        //Get Y position of mouse and rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        
        if(invertYAxis)
        {
            pivot.Rotate(vertical, 0, 0);
        } else 
        {
            pivot.Rotate(-vertical, 0, 0);
        }
        
        
        //Limit up/down camera rotation
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //Move camera based on current rotation of target and original offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);


        transform.position = target.position - (rotation * offset);

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        transform.LookAt(target);
    
    }
}
