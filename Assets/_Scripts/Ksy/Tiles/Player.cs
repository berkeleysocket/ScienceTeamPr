using KSY.Manager;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace KSY.Tile
{
    public class Player : TileObject
    {
        public Action<sbyte, sbyte> HandleMoveKeyPressed;
        public override void Magnetization(int xDir, int yDir, TileObject presser)
        {
            //Debug.LogError($"{gameObject.name}의 Magnetization는 오버라이드되지 않았습니다.");
        }

        private void OnEnable()
        {
            CinemachineCamera cam = GameObject.Find("PlayerCam").GetComponent<CinemachineCamera>();
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

