using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 10f;         // �J�����ړ��̑���
    public float rotationSpeed = 100f;    // �}�E�X��]�̑���

    void Update()
    {
        // �L�[�{�[�h�ɂ��ړ�
        float horizontal = Input.GetAxis("Horizontal"); // A/D�L�[
        float vertical = Input.GetAxis("Vertical");       // W/S�L�[

        // Q�L�[�ŉ��AE�L�[�ŏ�Ɉړ������
        float ascend = 0f;
        if (Input.GetKey(KeyCode.Space))
            ascend = 1f;
        else if (Input.GetKey(KeyCode.LeftShift))
            ascend = -1f;

        // ���[�J�����W�ňړ�����������
        Vector3 direction = transform.right * horizontal + transform.forward * vertical + transform.up * ascend;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // �}�E�X�ɂ���]
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // �}�E�X�̈ړ��ʂɉ�������]���v�Z
        Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed * Time.deltaTime;
        transform.eulerAngles += rotation;
    }
}
