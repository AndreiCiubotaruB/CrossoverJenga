using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Crossover.Jenga {
    public class GameplayManager : BaseManager {
        [Serializable]
        private struct MasteryMaterial {
            public DataManager.Mastery Mastery;
            public Material Material;
        }

        [SerializeField] private int _blocksPerLayer;
        [SerializeField] private float _stackSpacing;
        [SerializeField] private float _blockSpacing;
        [SerializeField] private float _baseSize;
        [SerializeField] private float _blockHeight;
        [SerializeField] private float _blockOverBaseAmmount;

        [Header("StackBlock")]
        [SerializeField] private StackBlock _blockPrefab;
        [SerializeField] private GameObject _base;
        [SerializeField] private MasteryMaterial[] _masteryMaterialData;
        [Header("Movement")]
        [SerializeField] private Transform _camera;
        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _wheelMultiplier;

        private Vector3 _lastMousePosition;
        private float _distance;

        private Dictionary<DataManager.Mastery, Material> _masteryMaterials;
        private Dictionary<string, StackBlockInfo> _stacks;

        private int _stackIndex;
        private StackBlockInfo[] _stackBlocks;

        private bool _hasStacks;

        #region Initialization
        public override void Initialize() {
            //ease of use add materials to dictionary to enable multiple materials in the future
            _masteryMaterials = new Dictionary<DataManager.Mastery, Material>();
            foreach(var pair in _masteryMaterialData) {
                _masteryMaterials.Add(pair.Mastery, pair.Material);
            }

            CreateStacks();
        }

        public override void Uninitialize() {
            DestroyStacks();
        }
        #endregion

        #region StackManagement
        private void CreateStacks() {
            var data = Managers.Instance.DataManager.Data;
            _stacks = new Dictionary<string, StackBlockInfo>();

            var blockSize = new Vector3(
                 (_baseSize - (_blocksPerLayer - 1) * _blockSpacing) / _blocksPerLayer,
                 _blockHeight,
                 _baseSize + _blockOverBaseAmmount
                );

            foreach (var block in data) {
                if (!_stacks.ContainsKey(block.grade)) {
                    _stacks.Add(
                        block.grade,
                        new StackBlockInfo(
                            block.grade,
                            Vector3.right *
                            (_stacks.Count * (_stackSpacing + (blockSize.x + _blockSpacing) * _blocksPerLayer - _blockSpacing)),
                            _blocksPerLayer,
                            _blockSpacing,
                            blockSize,
                            _base,
                            Managers.Instance.UiManager.CreateWorldText()
                            )
                        );
                }

                var blockInstance = Instantiate(_blockPrefab);
                blockInstance.Initialize(block, blockSize, _masteryMaterials[block.mastery]);

                _stacks[block.grade].AddBlock(blockInstance);
            }

            _stackBlocks = _stacks.Values.ToArray();
            _stackIndex = _stackBlocks.Length / 2;

            _distance = (_maxDistance - _minDistance) / 2 + _minDistance;
            _camera.position = _stackBlocks[_stackIndex].CenterPosition - _distance * Vector3.forward;
            _camera.transform.LookAt(_stackBlocks[_stackIndex].CenterPosition);
            _hasStacks = true;
        }

        private void DestroyStacks() {
            foreach (var stack in _stacks) {
                stack.Value.DestroyStack();
            }
            _stackBlocks = null;
            _stacks = null;
            _hasStacks = false;
        }

        public void ResetStacks() {
            DestroyStacks();
            CreateStacks();
        }

        public void EnableStackPhysics(bool value) {
            foreach (var stack in _stackBlocks) {
                stack.EnablePhysics(value);
            }
        }
        #endregion

        #region GameModes
        public void TestMyStack() {
            ResetStacks();
            foreach(var stack in _stackBlocks){
                stack.EliminateGlass();
                stack.EnablePhysics(true);
            }
        }
        #endregion

        #region Input
        private void Update() {
            if (!_hasStacks)
                return;

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                _stackIndex = (_stackIndex - 1) % _stackBlocks.Length;
                _camera.position = _stackBlocks[_stackIndex].CenterPosition - _distance * Vector3.forward;
                _camera.transform.LookAt(_stackBlocks[_stackIndex].CenterPosition);
            } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                _stackIndex = (_stackIndex + 1) % _stackBlocks.Length;
                _camera.position = _stackBlocks[_stackIndex].CenterPosition - _distance * Vector3.forward;
                _camera.transform.LookAt(_stackBlocks[_stackIndex].CenterPosition);
            }

            if (Input.mouseScrollDelta.y != 0) {
                var lookCenter = _stackBlocks[_stackIndex].CenterPosition;
                _distance -= Input.mouseScrollDelta.y * _wheelMultiplier * Time.deltaTime;
                if (_distance > _maxDistance)
                    _distance = _maxDistance;
                if(_distance < _minDistance)
                    _distance = _minDistance;
                _camera.position = lookCenter + (_camera.transform.position - lookCenter).normalized * _distance;
            } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                var lookCenter = _stackBlocks[_stackIndex].CenterPosition;
                _distance -= _moveSpeed * Time.deltaTime;
                if (_distance < _minDistance)
                    _distance = _minDistance;
                _camera.position = lookCenter + (_camera.transform.position - lookCenter).normalized * _distance;
            } else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                var lookCenter = _stackBlocks[_stackIndex].CenterPosition;
                _distance += _moveSpeed * Time.deltaTime;
                if (_distance > _maxDistance)
                    _distance = _maxDistance;
                _camera.position = lookCenter + (_camera.transform.position - lookCenter).normalized * _distance;
            }

            if (Input.GetMouseButtonDown(1)) {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out var hit, _maxDistance) && hit.transform.gameObject.TryGetComponent<StackBlock>(out var block))
                    Managers.Instance.UiManager.CreatePopup("Stack Info", block.InfoText);

            }

            if (Input.GetMouseButtonDown(0))
                _lastMousePosition = Input.mousePosition;

            if (Input.GetMouseButton(0)) {
                var delta = Input.mousePosition - _lastMousePosition;
                delta *= _rotationSpeed * Time.deltaTime;
                _camera.Translate(delta.x, delta.y, 0);

                var lookCenter = _stackBlocks[_stackIndex].CenterPosition;
                _camera.transform.LookAt(lookCenter);
                _camera.position = lookCenter + (_camera.transform.position - lookCenter).normalized * _distance;

                _lastMousePosition = Input.mousePosition;
            }
        }
        #endregion
    }
}
