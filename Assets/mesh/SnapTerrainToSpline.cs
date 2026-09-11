using UnityEngine;
using UnityEditor;
using UnityEngine.Splines;
using Unity.Mathematics;

public class SnapTerrainToSplineWindow : EditorWindow
{
    private SplineContainer splineContainer;
    private Terrain targetTerrain;
    private float roadWidth = 8.0f;       // 道路の全幅（メートル）
    private float falloffWidth = 3.0f;    // 道路脇の斜面余白幅（メートル）
    private float heightOffset = -0.15f;  // 道路上面より何m下げるか（土かぶり防止）
    private float sampleStep = 1.0f;      // サンプリング間隔（メートル）

    [MenuItem("Tools/Snap Terrain To Spline")]
    public static void ShowWindow()
    {
        GetWindow<SnapTerrainToSplineWindow>("Snap Terrain to Spline");
    }

    private void OnGUI()
    {
        GUILayout.Label("スプライン道路をTerrainに自動吸着", EditorStyles.boldLabel);

        splineContainer = (SplineContainer)EditorGUILayout.ObjectField("道路スプライン", splineContainer, typeof(SplineContainer), true);
        targetTerrain = (Terrain)EditorGUILayout.ObjectField("対象Terrain", targetTerrain, typeof(Terrain), true);

        EditorGUILayout.Space();
        roadWidth = EditorGUILayout.FloatField("道路の幅 (m)", roadWidth);
        falloffWidth = EditorGUILayout.FloatField("両脇ののり面幅 (m)", falloffWidth);
        heightOffset = EditorGUILayout.FloatField("高さオフセット (m)", heightOffset);
        sampleStep = EditorGUILayout.FloatField("サンプリング間隔 (m)", sampleStep);

        EditorGUILayout.Space();
        if (GUILayout.Button("吸着を実行する (Bake)", GUILayout.Height(35)))
        {
            if (splineContainer == null || targetTerrain == null)
            {
                EditorUtility.DisplayDialog("エラー", "SplineContainerとTerrainの両方を指定してください。", "OK");
                return;
            }

            if (EditorUtility.DisplayDialog("確認", "Terrainのハイトマップを書き換えます。よろしいですか？\n（アンドゥはできません）", "実行", "キャンセル"))
            {
                ApplySplineToTerrain();
            }
        }
    }

    private void ApplySplineToTerrain()
    {
        TerrainData tData = targetTerrain.terrainData;
        int res = tData.heightmapResolution;
        Vector3 tSize = tData.size;
        Vector3 tPos = targetTerrain.transform.position;

        float[,] heights = tData.GetHeights(0, 0, res, res);
        var spline = splineContainer.Spline;
        float totalLength = spline.GetLength();
        int stepCount = Mathf.CeilToInt(totalLength / sampleStep);

        float halfRoad = roadWidth * 0.5f;
        float totalRadius = halfRoad + falloffWidth;

        for (int i = 0; i <= stepCount; i++)
        {
            float t = (float)i / stepCount;
            spline.Evaluate(t, out float3 posLocal, out float3 tangentLocal, out float3 upLocal);

            Vector3 worldPos = splineContainer.transform.TransformPoint((Vector3)posLocal);
            Vector3 worldTangent = splineContainer.transform.TransformDirection((Vector3)tangentLocal).normalized;
            Vector3 worldNormal = Vector3.Cross(worldTangent, Vector3.up).normalized;

            float targetY = worldPos.y + heightOffset;

            // スプラインの左右（法線方向）にサンプリングしてTerrainを更新
            int stepsSide = Mathf.CeilToInt(totalRadius * 2f);
            for (int s = -stepsSide; s <= stepsSide; s++)
            {
                float dist = (s / (float)stepsSide) * totalRadius;
                Vector3 checkPos = worldPos + worldNormal * dist;

                // Terrain上のハイトマップインデックスに変換
                float normX = (checkPos.x - tPos.x) / tSize.x;
                float normZ = (checkPos.z - tPos.z) / tSize.z;

                if (normX < 0 || normX >= 1 || normZ < 0 || normZ >= 1) continue;

                int hX = Mathf.RoundToInt(normX * (res - 1));
                int hZ = Mathf.RoundToInt(normZ * (res - 1));

                float normTargetHeight = (targetY - tPos.y) / tSize.y;
                float absDist = Mathf.Abs(dist);

                if (absDist <= halfRoad)
                {
                    // 道路直下：指定した高さに完全に合わせる
                    heights[hZ, hX] = normTargetHeight;
                }
                else
                {
                    // 道路両脇（のり面）：外側に向けて滑らかに元の地面とブレンド
                    float factor = (absDist - halfRoad) / falloffWidth; // 0 (道路端) -> 1 (外側)
                    factor = Mathf.Clamp01(factor);
                    float currentH = heights[hZ, hX];
                    heights[hZ, hX] = Mathf.Lerp(normTargetHeight, currentH, factor);
                }
            }
        }

        tData.SetHeights(0, 0, heights);
        targetTerrain.Flush();
        EditorUtility.DisplayDialog("完了", "Terrainへの道路吸着が完了しました！", "OK");
    }
}