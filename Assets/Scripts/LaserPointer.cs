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
        }
        else
        {
            line.SetPosition(1, Vector3.forward * maxDistance);
        }
    }
}
