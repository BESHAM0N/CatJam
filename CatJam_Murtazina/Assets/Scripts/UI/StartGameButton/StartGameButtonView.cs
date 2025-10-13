using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Models.MainMenu;

namespace UI.StartGameButton
{
    [RequireComponent(typeof(Button))]
    public class StartGameButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        private void Awake()
        {
            if (_button == null) 
                _button = GetComponent<Button>();
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _signalBus.Fire<StartGameSignal>();
        }
    }
}