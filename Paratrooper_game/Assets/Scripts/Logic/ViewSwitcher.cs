using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public enum ViewState
    {
        FirstPerson,
        ThirdPerson
    }

    [SerializeField] private Settings _settings;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _firstPersonCameraRoot;
    [SerializeField] private Transform _thirdPersonCameraRoot;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;


    public void SpawnPlayer(Vector3 position)
    {
        _player.gameObject.SetActive(true);
        _player.transform.position = position;
        SetViewState(_settings.viewState);
    }

    private void SetViewState(ViewState viewState)
    {
        _settings.viewState = viewState;
        _playerCamera.Follow =
            viewState == ViewState.FirstPerson ? _firstPersonCameraRoot : _thirdPersonCameraRoot;
        _player.GetComponent<FirstPersonController>().enabled = viewState == ViewState.FirstPerson;
        _player.GetComponent<ThirdPersonController>().enabled = viewState == ViewState.ThirdPerson;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetViewState(_settings.viewState == ViewState.FirstPerson ? ViewState.ThirdPerson : ViewState.FirstPerson);
        }
    }
}