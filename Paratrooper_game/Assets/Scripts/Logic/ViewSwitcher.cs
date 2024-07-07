using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _firstPersonCameraRoot;
    [SerializeField] private Transform _thirdPersonCameraRoot;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    public void SpawnPlayer(Vector3 position)
    {
        _player.gameObject.SetActive(true);
        _player.transform.position = position;
        _playerCamera.Follow = _thirdPersonCameraRoot;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _playerCamera.Follow = _playerCamera.Follow == _firstPersonCameraRoot
                ? _thirdPersonCameraRoot
                : _firstPersonCameraRoot;
            _player.GetComponent<FirstPersonController>().enabled = _playerCamera.Follow == _firstPersonCameraRoot;
            _player.GetComponent<ThirdPersonController>().enabled = _playerCamera.Follow == _thirdPersonCameraRoot;
        }
    }
}