using UnityEngine;

public class HomeMugic : MonoBehaviour
{
    GameObject _mainGameManager;
    AudioSource _audioSource;
    AudioSource _mGMAudioSource;
    [SerializeField] AudioClip[] _homeMugic;
    void Start()
    {
        _mainGameManager = GameObject.FindWithTag("MainGameManager");
        _mGMAudioSource = _mainGameManager.GetComponent<AudioSource>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.clip = _homeMugic[Random.Range(0, _homeMugic.Length)];
        _audioSource.Play();
    }
    public void StopMugic()
    {
        _audioSource.Stop();
    }

    public void BackToHomeMugic()
    {
        _mGMAudioSource.Stop();
        _audioSource.clip = _homeMugic[Random.Range(0, _homeMugic.Length)];
        _audioSource.Play();
    }
    private void OnEnable()
    {
        _mainGameManager = GameObject.FindWithTag("MainGameManager");
        _audioSource = _mainGameManager.GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.clip = _homeMugic[Random.Range(0, _homeMugic.Length)];
        _audioSource.Play();
    }
}
