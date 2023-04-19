using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Crossover.Jenga {
    public class UiManager : BaseManager {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Popup _popupPrefab;
        [SerializeField] private WorldText _worldTextPrefab;

        private Popup _activePopup;

        public void CreatePopup(string title, string text) {
            if (_activePopup != null)
               ClosePopup(_activePopup);

            var popup = Instantiate(_popupPrefab, _canvas.transform);
            popup.Initialize(title, text);
            _activePopup = popup;
        }

        public void ClosePopup(Popup popup) {
            Destroy(popup.gameObject);
        }

        public WorldText CreateWorldText() {
            return Instantiate(_worldTextPrefab);
        }

        public override void Initialize() {

        }

        public override void Uninitialize() {

        }
    }
}