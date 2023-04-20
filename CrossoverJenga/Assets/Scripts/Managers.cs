using UnityEngine;

namespace Crossover.Jenga {
    public class Managers : MonoBehaviour {
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private GameplayManager _gameplayManager;
        [SerializeField] private UiManager _uiManager;

        private bool _initialized;

        public DataManager DataManager => _dataManager;
        public GameplayManager GameplayManager => _gameplayManager;
        public UiManager UiManager => _uiManager;


        private static Managers _instance;
        public static Managers Instance => _instance;

        public void Awake() {
            if (_instance != null) {
                DestroyImmediate(this.gameObject);
                return;
            }

            _instance = this;

            _dataManager.Initialize();
            _gameplayManager.Initialize();
            _uiManager.Initialize();
            _initialized = true;
        }

        private void OnApplicationQuit() {
            if (!_initialized)
                return;

            _uiManager.Uninitialize();
            _gameplayManager.Uninitialize();
            _dataManager.Uninitialize();
            _initialized = false;
        }
    }
}