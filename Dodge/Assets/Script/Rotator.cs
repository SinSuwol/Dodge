using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 0f; // �ʱ� �ӵ� 0f�� ����
    private float maxSpeed = 60f; // �ִ� �ӵ�
    private float speedIncreaseAmount = 1f; // �ʴ� �ӵ� ������

    void Update()
    {
        // �ӵ��� �ִ� �ӵ��� �������� �ʾ����� �ӵ��� ������Ŵ
        if (rotationSpeed < maxSpeed)
        {
            rotationSpeed += speedIncreaseAmount * Time.deltaTime; // �ʴ� 0.5f�� ����
            rotationSpeed = Mathf.Min(rotationSpeed, maxSpeed); // �ӵ��� �ִ� �ӵ��� �ʰ����� �ʵ��� ����
        }

        // ȸ�� ����
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
