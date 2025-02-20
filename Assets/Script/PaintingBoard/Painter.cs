using UnityEngine;

namespace PaintingBoard
{
    public class Painter : MonoBehaviour, IPainting, IPickable
    {
        [field: SerializeField] public BoardConfig.PaintColor Color { get; private set; }
        public GameObject GameObject => gameObject;

        private Vector3 _originPos;
        private Quaternion _originRotate;

        public bool Usable { get; private set; }

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _originPos = transform.position;
            _originRotate = transform.rotation;
            _rigidbody = GetComponent<Rigidbody>();
            Usable = true;
        }

        public void UsePaint()
        {
            Usable = false;
            gameObject.SetActive(false);
        }

        public void ResetPainter()
        {
            gameObject.SetActive(true);
            _rigidbody.position = _originPos;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = false;
            transform.rotation = _originRotate;
            Usable = true;
        }

        public void Pickup()
        {
            _rigidbody.isKinematic = true;
        }

        public void Drop()
        {
            _rigidbody.isKinematic = false;
        }
    }
}