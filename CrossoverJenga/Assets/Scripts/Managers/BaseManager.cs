using UnityEngine;

namespace Crossover.Jenga {
    public abstract class BaseManager: MonoBehaviour {
        public abstract void Initialize();
        public abstract void Uninitialize();
    }

}