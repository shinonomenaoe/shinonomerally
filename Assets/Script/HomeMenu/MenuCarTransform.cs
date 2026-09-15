using UnityEngine;

public class MenuCarTransform : MonoBehaviour
{
    [SerializeField] Transform _PlayerCarPos;
    GameObject _playerCar;
    Rigidbody _rb;
    Transform _transform;
    
    void Start()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _rb = _playerCar.GetComponent<Rigidbody>();
        _transform = _playerCar.GetComponent<Transform>();
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _transform.position = _PlayerCarPos.position;
        _transform.rotation = _PlayerCarPos.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = _PlayerCarPos.position;
        _transform.rotation = _PlayerCarPos.rotation;
    }
}
