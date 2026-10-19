using UnityEngine;

namespace GM025
{
    public class GM025Door : MonoBehaviour
    {
        bool opening;
        Vector3 target;

        public void Open()
        {
            target = transform.position + Vector3.up * 5f;
            opening = true;
        }

        void Update()
        {
            if (!opening) return;
            transform.position = Vector3.MoveTowards(transform.position, target, 5f * Time.deltaTime);
            if (transform.position == target)
            {
                opening = false;
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
