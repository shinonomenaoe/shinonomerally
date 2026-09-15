using Unity.Cinemachine;
using UnityEngine;

public class ChengeCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _1thPCam;
    [SerializeField] private CinemachineCamera _3thPMoveCam;
    [SerializeField] private CinemachineCamera _3thPCam;
    ActiveCamera _activeCamera = ActiveCamera._1thCam;
    enum ActiveCamera
    {
        _1thCam,
        _3thPMoveCam,
        _3thPCam,
    }
    void Start()
    {
        _activeCamera = ActiveCamera._1thCam;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {

            switch (_activeCamera)
            {
                case ActiveCamera._1thCam:
                    _1thPCam.Priority = 0;
                    _3thPCam.Priority = 10;
                    _3thPMoveCam.Priority = 0;
                    _activeCamera = ActiveCamera._3thPMoveCam;
                    break;

                case ActiveCamera._3thPMoveCam:
                    _3thPMoveCam.Priority = 0;
                    _3thPCam.Priority = 10;
                    _1thPCam.Priority = 0;
                    _activeCamera = ActiveCamera._3thPCam;
                    break;

                case ActiveCamera._3thPCam:
                    _3thPCam.Priority = 0;
                    _1thPCam.Priority = 10;
                    _1thPCam.Priority = 0;
                    _activeCamera = ActiveCamera._1thCam;
                    break;
            }
        }
    }
}
