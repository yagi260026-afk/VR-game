using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("移動・回転設定")]
    public float speed = 3f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float turnAngle = 45f; // 右スティックによる回転角度

    [Header("コンポーネント・参照")]
    public CharacterController controller;
    public Transform cameraTransform; // VR Camera (CenterEyeAnchor)

    private Vector3 velocity;
    private bool canTurn = true; // スナップ回転の二重入力防止

    void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }

        // Meta Questではカーソルロックは不要
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        RotatePlayer();
    }

    void Move()
    {
        // 左スティックで移動入力取得
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // HMD（VRカメラ）の向いている方向を基準に前後左右を計算
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * input.y + right * input.x;
        controller.Move(move * speed * Time.deltaTime);

        // 接地判定と重力
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Aボタンでジャンプ
        if (OVRInput.GetDown(OVRInput.Button.One) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 重力適用
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        // 右スティックの左右で45度ずつ回転（スナップターン）
        Vector2 rightStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (Mathf.Abs(rightStick.x) > 0.6f)
        {
            if (canTurn)
            {
                float turnDir = Mathf.Sign(rightStick.x) * turnAngle;
                transform.Rotate(Vector3.up, turnDir);
                canTurn = false;
            }
        }
        else
        {
            canTurn = true;
        }
    }
}