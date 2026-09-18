using UnityEngine;

public class MenuCarTransform : MonoBehaviour
{
    [SerializeField] Transform _playerCarPosHome;
    [SerializeField] Transform _mainCameraHomePos;
    [SerializeField] Transform _playerCarPosCostom;
    [SerializeField] Transform _mainCameraCostomPos;
    GameObject _mainCamera;
    Transform _mainCameraTrans;
    GameObject _playerCar;
    Rigidbody _rb;
    Transform _transform;
    NowPos _pos;


    void Awake()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera");
        _mainCameraTrans = _mainCamera.GetComponent<Transform>();
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _rb = _playerCar.GetComponent<Rigidbody>();
        _transform = _playerCar.GetComponent<Transform>();
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _mainCameraTrans.position = _mainCameraHomePos.position;
        _mainCameraTrans.rotation = _mainCameraHomePos.rotation;
        _transform.position = _playerCarPosHome.position;
        _transform.rotation = _playerCarPosHome.rotation;
        _pos = NowPos.Home;
    }

    // Update is called once per frame
    void Update()
    {
        if(_pos  == NowPos.Home)
        {
            _transform.position = _playerCarPosHome.position;
            _transform.rotation = _playerCarPosHome.rotation;
        }
        if (_pos == NowPos.Costom)
        {

        }
        if(_pos == NowPos.Garage)
        {
            _transform.position = _playerCarPosCostom.position;
            _transform.rotation = _playerCarPosCostom.rotation;
        }

    }

    public void CarMoveHome()
    {
        _pos = NowPos.Home;
        _transform.position = _playerCarPosHome.position;
        _transform.rotation = _playerCarPosHome.rotation;
        _mainCameraTrans.position = _mainCameraHomePos.position;
        _mainCameraTrans.rotation = _mainCameraHomePos.rotation;
    }

    public void CarMoveGarage()
    {
        _pos = NowPos.Garage;
    }

    public void CarMoveCostom()
    {
        _pos = NowPos.Costom;
        _transform.position = _playerCarPosCostom.position;
        _transform.rotation = _playerCarPosCostom.rotation;
        _mainCameraTrans.position = _mainCameraCostomPos.position;
        _mainCameraTrans.rotation = _mainCameraCostomPos.rotation;
    }

    enum NowPos
    {
        Home,
        Garage,
        Costom
    }
}
