using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private RaycastHit hit;
    private LineRenderer line;
    public Color color = Color.blue;
    public float maxDistance = 50.0f;
    public Transform laserMaker;

    // Start is called before the first frame update
    void Start()
    {
        CreateLineRenderer();
    }

    void CreateLineRenderer()
    {
        // LineRenderer 추가
        line = this.gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.receiveShadows = false;

        // 시작, 끝지점을 설정
        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.forward * maxDistance);

        // 라인의 폭 설정
        line.startWidth = 0.03f;
        line.endWidth = 0.005f;
        line.numCapVertices = 10;

        // 라인의 머티리얼 지정, 색상을 설정
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = this.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            line.SetPosition(1, Vector3.forward * hit.distance);
            laserMaker.position = hit.point + laserMaker.forward * 0.01f;
            // 각도를 법선벡터 방향으로 회전
            laserMaker.rotation = Quaternion.LookRotation(hit.normal);
            laserMaker.GetComponent<SpriteRenderer>().color = Color.yellow;

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                StartCoroutine(Teleport(hit.point));
            }
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Teleport(hit.point));
            }
#endif
        }
        else
        {
            line.SetPosition(1, Vector3.forward * maxDistance);
            laserMaker.position = transform.position + (transform.forward * maxDistance);
            laserMaker.rotation = Quaternion.LookRotation(transform.forward);
            laserMaker.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    IEnumerator Teleport(Vector3 pos)
    {
        OVRScreenFade.instance.fadeTime = 0.0f;
        OVRScreenFade.instance.FadeOut();

        transform.root.position = pos + (Vector3.up * 1.8f);

        yield return new WaitForSeconds(0.1f);

        OVRScreenFade.instance.fadeTime = 0.2f;
        OVRScreenFade.instance.FadeIn();
    }
}

/*
    ADB

    명령프롬프트
    Gitbash Shell
    // 근접센서 비활성
    $ adb shell am broadcast -a com.oculus.vrpowermanager.automation_disable

    // 근접센서를 활성화
    $ adb shell am broadcast -a com.oculus.vrpowermanager.prox_close

*/
