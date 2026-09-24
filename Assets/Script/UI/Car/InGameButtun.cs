using UnityEngine;

public class InGameButtun : MonoBehaviour
{
    GameObject _resetPos;
    GameObject _playerCar;
    CarEngenAndGearBox _carEngenBox;
    WheelsController _wheelsController;
    SideBrake _sideBrake;
    void Start()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
        _sideBrake = _playerCar.GetComponent<SideBrake>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPos()
    {
        GameObject inGameManager = GameObject.FindGameObjectWithTag("GameManager");
        //ReadySystem readySystem = inGameManager.GetComponent<ReadySystem>();
        //readySystem.ResetButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CourseLine")
        {
            _resetPos = other.GetComponent<GameObject>();
        }
    }

    public void AcselPedal()
    {
        _carEngenBox._verticalInput = 1;
        _wheelsController._verticalInput = 1;
    }

    public void BrakePedal()
    {
        _wheelsController.isBraking = true;
    }

    public void ChengeGear(bool isUp)
    {
        if (isUp && _carEngenBox._mission == CarEngenAndGearBox.Mission.MT) _carEngenBox._nowGearNam++;
        if (!isUp && _carEngenBox._mission == CarEngenAndGearBox.Mission.MT) _carEngenBox._nowGearNam--;
        if (isUp && _carEngenBox._mission == CarEngenAndGearBox.Mission.AT) _carEngenBox._isRevers = true;
        if (!isUp && _carEngenBox._mission == CarEngenAndGearBox.Mission.AT) _carEngenBox._isRevers = false;
    }

    public void Steer(bool isRight)
    {
        if (isRight)
        {
            _carEngenBox._horizontalInput = 1;
            _wheelsController._horizontalInput = 1;
        }
        if (!isRight)
        {
            _carEngenBox._horizontalInput = -1;
            _wheelsController._horizontalInput = -1;
        }
    }

    public void HandBrake()
    {
        _sideBrake.OnBrales();
    }

    private void OnEnable()
    {
        _playerCar = GameObject.FindWithTag("PlayerCar");
        _carEngenBox = _playerCar.GetComponent<CarEngenAndGearBox>();
        _wheelsController = _playerCar.GetComponent<WheelsController>();
        _sideBrake = _playerCar.GetComponent<SideBrake>();
    }
}
