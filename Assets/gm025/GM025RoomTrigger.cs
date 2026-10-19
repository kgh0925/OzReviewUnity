using UnityEngine;

namespace GM025
{
    public class GM025RoomTrigger : MonoBehaviour
    {
        public int roomIndex;

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GM025Player _))
                GM025Game.Instance.EnterRoom(roomIndex);
        }
    }
}
