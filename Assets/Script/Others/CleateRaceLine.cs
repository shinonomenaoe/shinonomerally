using System.Collections.Generic;
using UnityEngine;
using static RacingLineReCorder;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody))]
public class RacingLineReCorder : MonoBehaviour
{
    [System.Serializable]
    public struct PointData
    {
        public Vector3 position;
        public Quaternion rotation;
        public float speedKmh;
        public  PointData(Vector3 pos, Quaternion rot, float speed)
        {
            position = pos;
            rotation = rot;
            speedKmh = speed;
        }
    }
}

public class CleateRaceLine : MonoBehaviour
{
    [Header("記録設定")]
    [Tooltip("何秒ごとにポイントを作成するか")]
    [SerializeField] private float _roadLinePointTime = 0.5f;
    [Tooltip("走行ラインを取るか")]
    [SerializeField] private bool _isRecord = false;
    [Header("走行ラインをどこに保存するか")]
    [SerializeField] private string _saveFolderPath = "Assets/RacingLines";
    private readonly List<PointData> _recordedPoints = new List<PointData>();
    private Rigidbody _rb;
    private bool _isRecordingNow;
    private float _timer = 0f;

    private float _nowSpeedKmh => _rb.linearVelocity.magnitude * 3.6f;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (_isRecord && !_isRecordingNow)
        {
            StartRecord();
        }
        else if (_isRecordingNow)
        {
            _timer += Time.deltaTime;
            if (_timer > _roadLinePointTime)
            {
                _timer = 0f;
                AddPoints();
            }
        }
        else StopRecord();
    }
    private void StartRecord()//記録を開始・既存のポイントを削除
    {
        _recordedPoints.Clear();
        _timer = 0f;
        _isRecordingNow = true;

    }
    private void StopRecord()//記録を停止・オブジェクト群を作成
    {
        if(_recordedPoints.Count == 0)return;
        AddPoints();
        _isRecord = false;
        _isRecordingNow = false;
        string lineName = $"RacingLine_{System.DateTime.Now:MMdd__HHmm}";
        GameObject parentRoot = new GameObject(lineName);
        var i = 0;
        foreach (PointData point in _recordedPoints)//ポイント分のゲームオブジェクトを作成
        {
            GameObject pointOBJ = new GameObject($"WayPoint_{i}");
            pointOBJ.transform.position = point.position;
            pointOBJ.transform.rotation = point.rotation;
            pointOBJ.transform.SetParent(parentRoot.transform);
            i++;
        }
#if UNITY_EDITOR

        if (!AssetDatabase.IsValidFolder(_saveFolderPath))
        {
            string newFolder = _saveFolderPath.Replace("Assets/", "");
            AssetDatabase.CreateFolder("Assets", newFolder);
        }
        string prefabPath = $"{_saveFolderPath}/{lineName}.prefab";
        PrefabUtility.SaveAsPrefabAsset(parentRoot, prefabPath);
        AssetDatabase.SaveAssets();
#endif
    }
    private void AddPoints()//ポイントの情報を追加
    {
        _recordedPoints.Add(new PointData(transform.position, transform.rotation, _nowSpeedKmh));
    }
}
