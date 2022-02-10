using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    public OVRInput.Controller rightController = OVRInput.Controller.RTouch;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
            1. Combine 방식
                PrimaryIndexTrigger - 왼손 트리거
                SecondrayIndexTrigger - 오른손 트리거
            2. Individual 방
                PrimaryIndexTrigger, LTouch
                PrimaryIndexTrigger, RTouch
                
        */

        // Combine 방식
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("왼손 트리거 버튼 클릭");
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            Debug.Log("왼손 그랩버튼 클릭");
        }

        // Individual 방식
        // 정전압 방식 터치
        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, rightController))
        {
            Debug.Log("오른손 Index Trigger 터치");
        }
    }
}
