using UnityEngine;

public class ChengeResultCamera : MonoBehaviour
{
    GameObject _mainGameManager;
    PlayINF _playInf;
    [SerializeField] GameObject _resultCamera;
    void Start()
    {
        _mainGameManager = GameObject.FindWithTag("MainGameManager");
        _playInf = _mainGameManager.GetComponent<PlayINF>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playInf._isGoal)_resultCamera.SetActive(true);
        else _resultCamera.SetActive(false);
    }
}
