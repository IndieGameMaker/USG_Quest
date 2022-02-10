using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
    public OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    public OVRInput.Controller rightController = OVRInput.Controller.RTouch;
    public Image img;

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

            float value = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, rightController);
            img.fillAmount = value;
        }

        // 조이스틱 터치 여부
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, rightController))
        {
            Debug.Log("조이스틱 터치");
            Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, rightController);
            Debug.Log($"pos = ({axis.x}/{axis.y})");
        }

        // 오른손 그랩 - 진동
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, rightController))
        {
            StartCoroutine(Haptic(0.3f));
        }

    }

    IEnumerator Haptic(float duration)
    {
        OVRInput.SetControllerVibration(0.8f, 0.9f, rightController);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, rightController);
    }
}
