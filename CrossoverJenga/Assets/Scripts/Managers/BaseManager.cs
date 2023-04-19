using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Crossover.Jenga {
    public abstract class BaseManager: MonoBehaviour {
        public abstract void Initialize();
        public abstract void Uninitialize();
    }

}