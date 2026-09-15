using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadySystem : MonoBehaviour
{
    GameObject _tPFCam;
    CinemachineThirdPersonFollow _tPF;
    GameObject _playerCar;
    Rigidbody _carRb;
    GameObject _playerStartPosOBJ;
    Transform _playerStartPos;
    void Start()
    {
        _tPFCam = GameObject.FindGameObjectWithTag("VirtualCamera");
        _tPF = _tPFCam.GetComponent<CinemachineThirdPersonFollow>();
        _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        _carRb = _playerCar.GetComponent<Rigidbody>();
        _playerStartPosOBJ = GameObject.FindGameObjectWithTag("PlayerStartPos");
        _playerStartPos = _playerStartPosOBJ.GetComponent<Transform>();
        _playerCar.transform.position = _playerStartPos.position;
        _playerCar.transform.rotation = _playerStartPos.rotation;
    }
    public void ResetButton()
    {
        StartCoroutine(ResetPos());
    }

    IEnumerator ResetPos()
    {
        _carRb.linearVelocity = Vector3.zero;
        _carRb.angularVelocity = Vector3.zero;
        _playerCar.transform.position = _playerStartPos.position;
        _playerCar.transform.rotation = _playerStartPos.rotation;
        float originalX = _tPF.Damping.x;
        float originalY = _tPF.Damping.y;
        float originalZ = _tPF.Damping.z;
        _tPF.Damping = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        _tPF.Damping = new Vector3(originalX, originalY, originalZ);

    }
    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += StartReady;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= StartReady;
    //}

    //private void StartReady(Scene nowScene, LoadSceneMode mode)
    //{
    //    _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
    //    _playerStartPosOBJ = GameObject.FindGameObjectWithTag("PlayerStartPos");
    //    _playerStartPos = _playerStartPosOBJ.GetComponent<Transform>();
    //    _playerCar.transform.position = _playerStartPos.position;
    //    _playerCar.transform.rotation = _playerStartPos.rotation;
    //}
}
