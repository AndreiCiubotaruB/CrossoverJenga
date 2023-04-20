using UnityEngine;

namespace Crossover.Jenga {
    public class Popup : MonoBehaviour {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private TMPro.TMP_Text _title;
        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private float _maxWidth = 1000;
        public void Initialize(string title, string text) {
            _title.text = title;
            _text.text = text;
        }

        public void ClosePopup() {
            Managers.Instance.UiManager.ClosePopup(this);
        }
    }
}
