using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 10f;         // カメラ移動の速さ
    public float rotationSpeed = 100f;    // マウス回転の速さ

    void Update()
    {
        // キーボードによる移動
        float horizontal = Input.GetAxis("Horizontal"); // A/Dキー
        float vertical = Input.GetAxis("Vertical");       // W/Sキー

        // Qキーで下、Eキーで上に移動する例
        float ascend = 0f;
        if (Input.GetKey(KeyCode.Space))
            ascend = 1f;
        else if (Input.GetKey(KeyCode.LeftShift))
            ascend = -1f;

        // ローカル座標で移動方向を決定
        Vector3 direction = transform.right * horizontal + transform.forward * vertical + transform.up * ascend;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // マウスによる回転
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // マウスの移動量に応じた回転を計算
        Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed * Time.deltaTime;
        transform.eulerAngles += rotation;
    }
}
