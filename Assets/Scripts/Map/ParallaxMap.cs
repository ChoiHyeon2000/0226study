using UnityEngine;

public class ParallaxMap : MonoBehaviour
{
    // 카메라의 트랜스폼.
    public Transform cam;

    // 카메라를 따라갈 비율.
    public float parallaxRatio;

    // 이전 프레임에서의 카메라의 위치.
    private Vector3 lastCamPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 시작할 때 카메라의 처음 위치를 기억.
        lastCamPos = cam.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 카메라가 이번 프레임에 얼마나 움직였는지 이동량을 계산.
        // X 좌표의 이동량만 계산함.
        float deltaX = cam.position.x - lastCamPos.x;

        // 배경이 움직여야 할 거리를 계산.(이동량 * 비율)
        float moveX = deltaX * parallaxRatio;
        float moveY = 0.0f;

        transform.Translate(moveX, moveY, 0.0f);

        lastCamPos = cam.position;
    }
}
