using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CarSpecINF", menuName = "Scriptable Objects/CarSpecINF")]
public class CarSpecINF : ScriptableObject
{
    public CarSpec carspec;
}
[Serializable]
public struct CarSpec
{
    [Header("基本情報")]
    public string carName;
    public float mass;
    [Header("エンジン情報")]
    public AnimationCurve torqueCurve;
    public float moreTorque;
    public float idleRPM;
    public float maxRPM;
    public float enginqResponse;
    public float fRR;
    [Header("足回りセッティング")]
    public float brakeTorque;
    public float maxSteerAngle;
    public float suspensionSpring;
    public float suspensionDamper;
    public float suspensionDistance;
    public float targetPosition;
    [Header("その他セッティング")]
    public float downForce;
}
