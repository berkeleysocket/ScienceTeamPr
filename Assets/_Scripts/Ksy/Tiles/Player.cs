using KSY.Manager;
using Unity.Cinemachine;
using UnityEngine;

namespace KSY.Tile
{
    public class Player : TileObject
    {
        public override void Magnetization(sbyte xDir, sbyte yDir, TileObject presser)
        {
            throw new System.NotImplementedException();
        }

        private void OnEnable()
        {
            CinemachineCamera cam = GameObject.Find("PlayerCam").GetComponent<CinemachineCamera>();
            CameraTarget target = new CameraTarget();
            target.TrackingTarget = transform;
            cam.Target = target;
        }
        private void Start()
        {
            GameManager.Instance.InputManager.MoveKeyPressed += (x,y)=> Move(x,y);
        }
    }
}

