using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

namespace Paratrooper.Presenter
{
    public class ViewSwitcher : MonoBehaviour
    {
        public enum ViewState
        {
            FirstPerson,
            ThirdPerson
        }

        [SerializeField] private Settings settings;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform firstPersonCameraRoot;
        [SerializeField] private Transform thirdPersonCameraRoot;
        [SerializeField] private CinemachineVirtualCamera playerCamera;


        public void SpawnPlayer(Vector3 position)
        {
            player.gameObject.SetActive(true);
            player.transform.position = position;
            SetViewState(settings.viewState);
        }

        private void SetViewState(ViewState viewState)
        {
            settings.viewState = viewState;
            playerCamera.Follow =
                viewState == ViewState.FirstPerson ? firstPersonCameraRoot : thirdPersonCameraRoot;
            player.GetComponent<FirstPersonController>().enabled = viewState == ViewState.FirstPerson;
            player.GetComponent<ThirdPersonController>().enabled = viewState == ViewState.ThirdPerson;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                SetViewState(settings.viewState == ViewState.FirstPerson
                    ? ViewState.ThirdPerson
                    : ViewState.FirstPerson);
            }
        }
    }
}