using UnityEngine;
using UnityEngine.SceneManagement;

public class ChengeToResultPos : MonoBehaviour
{
    GameObject _playerCar;
    GameObject _mGM;
    Rigidbody _carRb;
    PlayINF _playINF;
    GameObject _resultPosOBJ;
    Transform _resultPos;
    [SerializeField] Quaternion _resultRotaion;
    [SerializeField] float[] _resultPosPuls = new float[3];
    void Start()
    {
        _mGM = GameObject.FindWithTag("MainGameManager");
        _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        _carRb = _playerCar.GetComponent<Rigidbody>();
        _playINF = _mGM.GetComponent<PlayINF>();
        _resultPosOBJ = GameObject.FindGameObjectWithTag("ResultPos");
        _resultPos = _resultPosOBJ.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChengePos();
    }

    void ChengePos()
    {
        if (_playINF._isGoal)
        {
            _carRb.linearVelocity = Vector3.zero;
            _carRb.angularVelocity = Vector3.zero;
            _playerCar.transform.position = new Vector3(_resultPos.position.x + _resultPosPuls[0], _resultPos.position.y + _resultPosPuls[1], _resultPos.position.z + _resultPosPuls[2]);
            _playerCar.transform.rotation = _resultRotaion;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetResultPos;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetResultPos;
    }

    private void SetResultPos(Scene nowScene, LoadSceneMode mode)
    {
        _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        _resultPosOBJ = GameObject.FindGameObjectWithTag("ResultPos");
        _resultPos = _resultPosOBJ.GetComponent<Transform>();
    }
}
