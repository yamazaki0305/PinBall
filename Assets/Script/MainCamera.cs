using UnityEngine;

/// <summary>
/// メインカメラの解像度設定を提供します。
/// </summary>
public class MainCamera : MonoBehaviour
{
    /// <summary>
    /// アプリケーションの仮想解像度
    /// </summary>
    [SerializeField]
    Vector2 targetResolution = new Vector2();

    /// <summary>
    /// 端末の画面比率を適用した仮想解像度
    /// </summary>
    static Vector2 virtualResolution;

    /// <summary>
    /// メインカメラを初期化します。
    /// </summary>
    void Awake()
    {
        var myCamera = GetComponent<Camera>();
        var rawResolution = new Vector2(Screen.width, Screen.height);

        var rawAspectX = rawResolution.x / rawResolution.y;
        var rawAspectY = rawResolution.y / rawResolution.x;
        var targetAspectX = targetResolution.x / targetResolution.y;
        var targetAspectY = targetResolution.y / targetResolution.x;

        virtualResolution = targetResolution;
        if (rawAspectX > targetAspectX)
        {
            virtualResolution.x = targetResolution.y * rawAspectX;
        }
        if (rawAspectY > targetAspectY)
        {
            virtualResolution.y = targetResolution.x * rawAspectY;
        }

        var cameraRect = new Rect();
        cameraRect.width = targetResolution.x / virtualResolution.x;
        cameraRect.height = targetResolution.y / virtualResolution.y;
        cameraRect.x = (1.0f - cameraRect.width) / 2;
        cameraRect.y = (1.0f - cameraRect.height) / 2;
        myCamera.rect = cameraRect;

        Screen.SetResolution((int)virtualResolution.x, (int)virtualResolution.y, false);
    }

    /// <summary>
    /// 端末の画面比率を適用した仮想解像度を取得します。
    /// </summary>
    public static Vector2 VirtualResolution
    {
        get { return virtualResolution; }
    }
}
