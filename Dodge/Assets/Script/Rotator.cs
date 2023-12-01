using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 0f; // 초기 속도 0f로 시작
    private float maxSpeed = 60f; // 최대 속도
    private float speedIncreaseAmount = 1f; // 초당 속도 증가량

    void Update()
    {
        // 속도가 최대 속도에 도달하지 않았으면 속도를 증가시킴
        if (rotationSpeed < maxSpeed)
        {
            rotationSpeed += speedIncreaseAmount * Time.deltaTime; // 초당 0.5f씩 증가
            rotationSpeed = Mathf.Min(rotationSpeed, maxSpeed); // 속도가 최대 속도를 초과하지 않도록 조정
        }

        // 회전 적용
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
