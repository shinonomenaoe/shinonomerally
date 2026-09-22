using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("参照オブジェクト")]
    [SerializeField] private Transform _raicingLine;
    CarEngenAndGearBox _carEngenAndGearBox;
    WheelsController _wheelsController;
    Rigidbody _rb;

    [Header("AI設定")]
    [SerializeField]  float _reachTherShold = 5f;//到着したと認識するまでの距離
    [SerializeField]  int _lookPointIndex = 3;//どんなコーナーかを認識する現在位置からどれくらい先のポイント
    [SerializeField, Range(-1, 0)] float _brakePow = -0.4f;
    [SerializeField, Range(0, 1)] float _lowThrottolePow = 0.4f;
    [SerializeField]  float _fullThrottleSteerLimit = 0.25f;//全開で踏んでいける基準のステアの割合
    [SerializeField]  float _brakeAngle = 35;//ブレーキを踏む角度
    [SerializeField]  float _steerPow = 1.8f;//ステアリングを切るサイン値にかける倍率

    [Header("スタックチェック")]
    [SerializeField] private float _stuckCheckTime = 0.25f;//これ以上動けなかった場合スタック判定に入る
    float _stuckTimer = 0f;
    bool  _isRecover = false;
    float _recoverTime = 0;

    private readonly List<Transform> _wayPoints = new List<Transform>();
    int _nowWayPointNam = 0;
    int _maxWayPointNam => _wayPoints.Count -1;
    void Start()
    {
        _carEngenAndGearBox = GetComponent<CarEngenAndGearBox>();
        _wheelsController = GetComponent<WheelsController>();
        _rb = GetComponent<Rigidbody>();
        foreach (Transform child in _raicingLine)
        {
            _wayPoints.Add(child);
        }
    }

    void Update()
    {
        if (_wayPoints.Count == 0) return;
        if (_isRecover)
        {
            ExecuteRecovery();
            return;
        }
        Drive();
        CheckStuck();
    }

    void Drive()
    {
        //waypointの切り替え
        Transform nowTargetWayPoint = _wayPoints[_nowWayPointNam];
        float distToTarget = Vector3.Distance(transform.position, nowTargetWayPoint.position);
        if (distToTarget < _reachTherShold)
        {
            _nowWayPointNam++;
            nowTargetWayPoint = _wayPoints[_nowWayPointNam];
        }
        
        //ステアリング操作
        Vector3 localtarget = transform.InverseTransformPoint(nowTargetWayPoint.position);
        float targetAngle = localtarget.x / localtarget.magnitude;
        float steer = Mathf.Clamp(targetAngle * _steerPow, -1, 1);
        
        //コーナー角度の計算
        int lookAheadIdx = Mathf.Min(_lookPointIndex + _nowWayPointNam, _maxWayPointNam);
        Transform farTarget = _wayPoints[_lookPointIndex];
        Vector3 dirTargetVectle = (nowTargetWayPoint.position - transform.position).normalized;
        Vector3 dirTargetToFarTargetVectle = (farTarget.position - nowTargetWayPoint.position).normalized;
        float nowAngle = Vector3.Angle(dirTargetVectle, dirTargetToFarTargetVectle);

        //ブレーキアクセル操作
        float throttle;
        float steerAds = Mathf.Abs(steer);
        if(nowAngle > _brakeAngle ||  steerAds < 0.75)
        {
            throttle = _brakePow;
        }
        else if(_fullThrottleSteerLimit < steerAds)
        {
            throttle = _lowThrottolePow;
        }
        else
        {
            throttle = 1f;
        }
        _wheelsController._verticalInput = throttle;
        _wheelsController._horizontalInput = steer;
        _carEngenAndGearBox._verticalInput = throttle;
        _carEngenAndGearBox._horizontalInput = steer;
    }

    void CheckStuck()
    {
        float speedKmh = _rb.linearVelocity.magnitude *3.6f;
        if(speedKmh < 3.0 && _wheelsController._verticalInput > 0.3f)
        {
            _stuckTimer += Time.deltaTime;
            if(_stuckTimer >= _stuckCheckTime)
            {
                _isRecover = true;
                _stuckTimer = 0f;
                _recoverTime = 1.5f;
            }
        }
        else
        {
            _stuckTimer = 0;
        }
    }

    void ExecuteRecovery()
    {
        _recoverTime -= Time.deltaTime;
        _carEngenAndGearBox._isRevers = true;
        _carEngenAndGearBox._verticalInput = 0.8f;
        _wheelsController._verticalInput = 0.8f;
        _wheelsController._horizontalInput = -_wheelsController._horizontalInput;
        if(_recoverTime <= 0f)
        {
            _isRecover = false;
            _carEngenAndGearBox._isRevers = false;
        }
    }
}
