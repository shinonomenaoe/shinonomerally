using UnityEngine;

public class SetFlamelateAndSetScale : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        // 画面の長辺が 1280px を超える端末の場合、1280px 基準にスケールダウンする
        int targetWidth = 1280;
        if (Screen.width > targetWidth)
        {
            float ratio = (float)Screen.height / Screen.width;
            int targetHeight = Mathf.RoundToInt(targetWidth * ratio);
            Screen.SetResolution(targetWidth, targetHeight, true);
        }
    }
}
