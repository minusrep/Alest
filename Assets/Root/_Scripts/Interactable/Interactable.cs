using UnityEngine;


namespace Root.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool IsReady
        {
            get => _isReady;

            protected set
            {
                _isReady = value;

                _animator.SetBool("IsReady", _isReady);
            }
        }

        private Animator _animator;

        private bool _isReady;

        private void Start()
            => Init();

        protected virtual void Init()
        {
            _animator = GetComponent<Animator>();
        }

        public abstract void Interact();
    }

}
