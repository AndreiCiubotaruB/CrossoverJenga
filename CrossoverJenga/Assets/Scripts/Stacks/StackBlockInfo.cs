using System.Collections.Generic;
using UnityEngine;

namespace Crossover.Jenga {

    //ease of use to enable structure and mutliple stacks if required
    public class StackBlockInfo {
        private List<StackBlock> _blocks;
        private string _grade;
        private Transform _holder;
        private int _blocksPerLayer;
        private float _blockSpacing;
        private Vector3 _blockSize;
        private Vector3 _blockStartX;
        private Vector3 _blockStartZ;
        private Vector3 _centerOffset;

        private Vector3 _center;

        public Vector3 CenterPosition => _center;

        public StackBlockInfo(string grade, Vector3 position, int blocksPerLayer, float blockSpacing, Vector3 blockSize, GameObject ground, WorldText text) {
            _grade = grade;
            _blocks = new List<StackBlock>();
            var holder = new GameObject(grade);
            holder.transform.position = position;
            _holder = holder.transform;
            _blocksPerLayer = blocksPerLayer;
            _blockSpacing = blockSpacing;
            _blockSize = blockSize;

            var maxSize = (_blockSize.x + _blockSpacing) * _blocksPerLayer - _blockSpacing;
            _centerOffset = Vector3.right * ((maxSize - _blockSize.x) / 2) - (_blockSize.y / 2) * Vector3.up;

            _center = _holder.position + _centerOffset;

            _blockStartX = _center + new Vector3((-maxSize + _blockSize.x) / 2, 0, 0);
            _blockStartZ = _center + new Vector3(0, 0, (-maxSize + _blockSize.x) / 2);

            var groundObj = GameObject.Instantiate(ground, _holder);
            groundObj.transform.position = _center - Vector3.up * (blockSize.y / 2);
            groundObj.transform.localScale = new Vector3(blockSize.z + blockSpacing * 2, 1, blockSize.z + blockSpacing * 2);

            text.transform.SetParent(_holder);
            text.transform.position = _center - Vector3.forward * (blockSize.z / 2 + blockSpacing);
            text.Initialize(grade, blockSize.z , blockSize.y);
        }

        public void AddBlock(StackBlock block) {
            block.transform.SetParent(_holder);

            int layerCount = _blocks.Count / _blocksPerLayer;

            if (layerCount % 2 == 1)
                block.transform.Rotate(0, 90, 0);

            _center = _holder.position + _centerOffset + (layerCount * _blockSize.y / 2) * Vector3.up;

            //position block
            block.transform.position =
                Vector3.up * (layerCount * _blockSize.y) +
                (layerCount % 2 == 0 ? Vector3.right : Vector3.forward) * ((_blocks.Count % _blocksPerLayer) * (_blockSize.x + _blockSpacing)) +
                (layerCount % 2 == 0 ? _blockStartX : _blockStartZ);
            _blocks.Add(block);
        }

        public void DestroyStack() {
            foreach (var block in _blocks)
                GameObject.Destroy(block.gameObject);
            GameObject.Destroy(_holder.gameObject);
        }

        public void EliminateGlass() {
            foreach (var stack in _blocks) {
                stack.gameObject.SetActive(stack.Mastery != DataManager.Mastery.Glass);
            }
        }

        public void EnablePhysics(bool value) {
            foreach (var stack in _blocks) {
                stack.Release(value);
            }
        }
    }
}
