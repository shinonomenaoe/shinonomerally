using System.Collections;
using UnityEngine;

public class CarSoundSys : MonoBehaviour
{
    private CarEngenAndGearBox _carEngenAndGearBox;
    private AudioSource _audioSource;
    [SerializeField] float _volMag = 0.5f;
    [SerializeField] float _pichMag = 1.5f;
    [SerializeField] AudioClip _turbo;
    public bool _isBackTurbin = false;
    void Start()
    {
        _carEngenAndGearBox = GetComponent<CarEngenAndGearBox>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        EngenSound();
        StartCoroutine(BackTurbineSound());
    }

    void EngenSound()
    {
        float rpmRatio = Mathf.InverseLerp(_carEngenAndGearBox._idelRPM, _carEngenAndGearBox._maxRPM, _carEngenAndGearBox._nowRPM);
        _audioSource.volume = rpmRatio * _volMag;
        _audioSource.pitch = rpmRatio * _pichMag;
    }

    IEnumerator BackTurbineSound()
    {
        if (_carEngenAndGearBox._isHightEngenRoal && Input.GetAxis("Vertical") < 0.1f && !_isBackTurbin)
        {
            _isBackTurbin = true;
            _audioSource.PlayOneShot(_turbo, _volMag);
            yield return new WaitForSeconds(8f);
            _isBackTurbin = false;

        }
    }
}
