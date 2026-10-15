using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    // オブジェクトの参照
    public Transform Target;
    public Transform CameraJoint;
    public Camera Camera;

    // プレイヤー入力の参照
    public PlayerInput PlayerInput;

    // パラメータとなる変数
    public Parameters parameter;

    [System.Serializable]
    public class Parameters
    {
        // パラメータとなる変数
        public Vector3 Position;
        public Vector3 Angles;
        public Vector3 Distance;

        public float TiltMin = -90;   // チルト最小値
        public float TiltMax = 90;   // チルト最大値
        public float CameraIntencity = 1.5f;  // カメラ感度
    }
    /*
    // パラメータとなる変数
    public Vector3 Position;
    public Vector3 Angles;
    public Vector3 Distance;

    public float TiltMin = -90;   // チルト最小値
    public float TiltMax = 90;   // チルト最大値
    public float CameraIntencity = 1.5f;  // カメラ感度
    */
    void Start()
    {
        // 入力アクションを登録
        InputAction();
    }

    void Update()
    {
        // カメラの移動や入力などの計算を行う
        parameter.Position = Vector3.Lerp(parameter.Position, Target.position, Time.deltaTime);    // 注視点の位置をカメラの位置にする

        // スクロールで距離変更
        var scroll = Mouse.current.scroll.value;
        parameter.Distance.z += scroll.y;
        parameter.Distance.z = Mathf.Clamp(parameter.Angles.z, -5, 0);

        // カメラのチルト(X軸回転)に制限を掛ける
        parameter.Angles.x = Mathf.Clamp(parameter.Angles.x, parameter.TiltMin, parameter.TiltMax);
    }

    // Updateよりもあとに実行される
    private void LateUpdate()
    {
        // Updateで計算した結果を反映させる

        // カメラの位置と回転を更新する
        transform.position = parameter.Position;
        transform.eulerAngles = parameter.Angles;

        // 注視点からの距離を更新する
        CameraJoint.localPosition = parameter.Distance;
    }

    private void InputAction()
    {
        if(PlayerInput == null)
        {
            Debug.LogError("PlayerInputが見つかりません");
            return;   // メソッドを強制終了
        }

        // Lookアクションが実行されたときの処理を登録する
        PlayerInput.actions["Look"].performed += context =>
        {
            var value = context.ReadValue<Vector2>();     // 入力値を取得
            parameter.Angles.x += value.y * -1 * parameter.CameraIntencity;     // 上下回転を加算
            parameter.Angles.y += value.x * parameter.CameraIntencity;     // 左右回転を加算
        };
        PlayerInput.actions["Look"].Enable();     // Lookアクションを有効化する
    }
}
