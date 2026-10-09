using UnityEngine;

public class ResetPos : MonoBehaviour
{
    GameObject _resetPosOBJ;
    Rigidbody _rb;
    WheelsController _wheelsController;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _wheelsController = GetComponent<WheelsController>();

    }
    private void OnTriggerEnter(Collider resetpos)
    {
        if (resetpos.tag == "CourseLine")
        {
            _resetPosOBJ = resetpos.gameObject;
        }
    }

    public void ResetButton()//場所をリセットし前や後ろに進まなくした
    {
        if(_resetPosOBJ == null)return;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        Wheel[] wheels = _wheelsController.wheels;
        foreach (Wheel wheel in wheels)
        {
            wheel.collider.motorTorque = 0;
            wheel.collider.brakeTorque = 100000;
        }
        transform.position = _resetPosOBJ.transform.position;
        transform.rotation = _resetPosOBJ.transform.rotation;
        foreach (Wheel wheel in wheels) wheel.collider.brakeTorque = 0;
    }
}
