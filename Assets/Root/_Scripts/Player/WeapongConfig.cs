using System;
using UnityEngine;

namespace Root.Player
{
    [Serializable]
    public class WeapongConfig
    {
        public Vector2 OriginOffset => _originOffset;

        public float OverlapRadius => _overlapRadius;

        public Color Color => _color;

        public float Slowdown => _slowdown;

        public int Damage => _damage;

        [SerializeField] private Vector2 _originOffset;

        [SerializeField] private float _overlapRadius;

        [SerializeField] private Color _color;

        [SerializeField][Range(0f, 1f)] private float _slowdown;

        [SerializeField] private int _damage;
    }
}
