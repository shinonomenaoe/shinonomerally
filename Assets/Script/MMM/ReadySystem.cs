using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadySystem : MonoBehaviour
{
    GameObject _playerCar;
    GameObject _playerStartPosOBJ;
    Transform _playerStartPos;
    void Start()
    {
        _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        _playerStartPosOBJ = GameObject.FindGameObjectWithTag("PlayerStartPos");
        _playerStartPos = _playerStartPosOBJ.GetComponent<Transform>();
        _playerCar.transform.position = _playerStartPos.position;
        _playerCar.transform.rotation = _playerStartPos.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartReady;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartReady;
    }

    private void StartReady(Scene nowScene,LoadSceneMode mode)
    {
        _playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        _playerStartPosOBJ = GameObject.FindGameObjectWithTag("PlayerStartPos");
        _playerStartPos = _playerStartPosOBJ.GetComponent<Transform>();
        _playerCar.transform.position = _playerStartPos.position;
        _playerCar.transform.rotation = _playerStartPos.rotation;
    }
}
