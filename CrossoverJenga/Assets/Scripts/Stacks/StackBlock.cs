using UnityEngine;

namespace Crossover.Jenga {
    public class StackBlock: MonoBehaviour {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private MeshRenderer _renderer;

        private DataManager.StackData _data;
        public DataManager.Mastery Mastery => _data.mastery;
        public string InfoText => $"{_data.grade}:{_data.domain}\n{_data.cluster}\n{_data.standarddescription}";

        public void Initialize(DataManager.StackData data, Vector3 size, Material material) {
            _data = data;
            transform.localScale = size;
            _rigidBody.isKinematic = true;
            _renderer.material = material;
        }

        public void Release(bool value) {
            _rigidBody.isKinematic = !value;
            _rigidBody.useGravity = value;
            if(!value)
                _rigidBody.velocity = Vector3.zero;
        }
    }
}
