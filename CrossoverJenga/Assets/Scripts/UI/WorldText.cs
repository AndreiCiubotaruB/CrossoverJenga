using UnityEngine;

namespace Crossover.Jenga {
    public class WorldText : MonoBehaviour {
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private TMPro.TMP_Text _text;

        public void Initialize(string text, float width, float height) {
            _canvas.sizeDelta = new Vector2(width, height);
            _text.text = text;
        }
    }
}