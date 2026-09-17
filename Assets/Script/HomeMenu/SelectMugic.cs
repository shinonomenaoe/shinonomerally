using UnityEngine;
using UnityEngine.UI;

public class SelectMugic : MonoBehaviour
{
    [SerializeField] Text _nowPlayMugic;
    GameObject _mGM;
    AudioSource _playerAudioSouce;
    [SerializeField] AudioClip[] _playMugic;
    [SerializeField] string[] _playMugicName;
    int _playMugicNam = 0;
    void Start()
    {
        _mGM = GameObject.FindGameObjectWithTag("MainGameManager");
        _playerAudioSouce = _mGM.GetComponent<AudioSource>();
        _playerAudioSouce.loop = true;
        _nowPlayMugic.text = _playMugicName[0];
        _playerAudioSouce.clip = _playMugic[0];
    }
    void Update()
    {

    }

    public void NextMugic()
    {
        if (_playMugicNam < _playMugic.Length)
        {
            _playMugicNam++;
            _nowPlayMugic.text = _playMugicName[_playMugicNam];
            _playerAudioSouce.clip = _playMugic[_playMugicNam];
            _playerAudioSouce.Play();
        }
    }

    public void BackMugic()
    {
        if(_playMugicNam > 0)
        {
            _playMugicNam--;
            _nowPlayMugic.text = _playMugicName[_playMugicNam];
            _playerAudioSouce.clip = _playMugic[_playMugicNam];
            _playerAudioSouce.Play();
        }
    }
}
