using UnityEngine;

public class DriftSound : MonoBehaviour
{
    [SerializeField] WheelCollider _rearLeft;
    [SerializeField] WheelCollider _rearRight;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _driftSound;
    [SerializeField] float _sideWaysSlipLimit = 0.3f;
    [SerializeField] float _addVol = 0.2f;
    WheelHit _leftHit;
    WheelHit _rightHit;
    
    void Start()
    {
        
    }
    void Update()
    {
        PlayDriftSound();
    }

    void PlayDriftSound()
    {
        if((_rearLeft.GetGroundHit(out _leftHit) || _rearRight.GetGroundHit(out _rightHit)) && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            if((Mathf.Abs(_leftHit.sidewaysSlip) > _sideWaysSlipLimit || Mathf.Abs(_rightHit.sidewaysSlip) > _sideWaysSlipLimit))
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.clip = _driftSound;
                    MakeDriftSound();
                    _audioSource.Play();
                }
            }
            else _audioSource.Stop();
        }
    }

    void MakeDriftSound()
    {
        float vol = Mathf.InverseLerp(_sideWaysSlipLimit, 1f,Mathf.Abs(_leftHit.sidewaysSlip)) + _addVol;
        _audioSource.volume = vol;
    }
}
