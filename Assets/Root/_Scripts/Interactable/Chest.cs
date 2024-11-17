using System.Collections.Generic;
using UnityEngine;

namespace Root.Interactable
{
    public class Chest : Interactable
    {
        [SerializeField] private List<ParticleSystem> _particles;

        protected override void Init()
        {
            base.Init();

            var trigger = GameObject.Find("CollectorTrigger").GetComponent<Collider2D>();

            _particles.ForEach(p => p.trigger.SetCollider(0, trigger));

            IsReady = true;
        }

        public override void Interact()
        {
            if (!IsReady) return;

            IsReady = false;

            _particles.ForEach(p => p.Play());
        }
    }
}
