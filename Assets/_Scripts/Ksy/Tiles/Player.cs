using KSY.Manager;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace KSY.Tile
{
    public class Player : TileObject
    {
        public Action<sbyte, sbyte> HandleMoveKeyPressed;
        public CinemachineCamera cam;
        public override void Magnetization(int xDir, int yDir, TileObject presser)
        {
            //Debug.LogError($"{gameObject.name}�� Magnetization�� �������̵���� �ʾҽ��ϴ�.");
        }

        private void OnEnable()
        {
            CameraTarget target = new CameraTarget();
            target.TrackingTarget = transform;
            cam.Target = target;
        }
        private void OnDisable()
        {
            GameManager.Instance.InputManager.MoveKeyPressed -= HandleMoveKeyPressed;
        }
        private void Start()
        {
            HandleMoveKeyPressed = (x, y) => Move(x, y);
            GameManager.Instance.InputManager.MoveKeyPressed += HandleMoveKeyPressed;
        }
    }
}

