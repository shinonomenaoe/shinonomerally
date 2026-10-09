using System.Collections;
using UnityEngine;

public class CarSoundSys : MonoBehaviour
{
    private CarEngenAndGearBox _carEngenAndGearBox;
    [SerializeField] private AudioSource _engenAudioSource;
    [SerializeField] private AudioSource _backTurbineAudioSource;
    [SerializeField] float _volMag = 0.5f;
    [SerializeField] float _pichMag = 1.5f;
    [SerializeField] float _turbineSoundVol = 0.3f;
    public bool _turboCar = true;
    public bool _maxGus = false;
    void Start()
    {
        _carEngenAndGearBox = GetComponent<CarEngenAndGearBox>();
        _backTurbineAudioSource.volume = _turbineSoundVol;
    }

    // Update is called once per frame
    void Update()
    {
        EngenSound();
        StartCoroutine(BackTurbineSound());
    }

    void EngenSound()
    {
        float rpmRatio = Mathf.InverseLerp(_carEngenAndGearBox.idelRPM, _carEngenAndGearBox.maxRPM, _carEngenAndGearBox.nowRPM);
        _engenAudioSource.volume = rpmRatio * _volMag;
        _engenAudioSource.pitch = rpmRatio * _pichMag;
    }

    IEnumerator BackTurbineSound()
    {
        if (_carEngenAndGearBox.isHightEngenRoal && Input.GetAxis("Vertical") < 0.1f && !_maxGus && _turboCar)
        {
            _maxGus = true;
            _backTurbineAudioSource.Play();
            yield return new WaitForSeconds(8f);
            _maxGus = false;

        }
    }
}
