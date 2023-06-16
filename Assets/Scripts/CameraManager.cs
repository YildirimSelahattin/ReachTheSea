using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Vector3 touchPosition;
    public Rigidbody cameraRigidbody;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            //For ray purposes
            Vector3 curTouchPosition = Input.GetTouch(0).position;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)// This is actions when finger/cursor hit screen
            {
                touchPosition = curTouchPosition;
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 difference = curTouchPosition-touchPosition;
               
                float lateralPower = difference.x;
                Vector3 addForce = Vector3.zero;
                addForce.x = lateralPower;
                if((transform.position - addForce * 0.01f).x < 9)
                {
                    transform.position = new Vector3(9, transform.position.y, transform.position.z);
                }
                else if ((transform.position - addForce * 0.01f).x >40)
                {
                    transform.position = new Vector3(40, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = transform.position - addForce * 0.01f;
                }
                touchPosition = curTouchPosition;

            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

            }
        }
    }


}
