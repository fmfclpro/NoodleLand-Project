using System;
using FMFCLPRO.Animations;
using NoodleLand.Enums;
using Unity.VisualScripting;
using UnityEngine;

/*
MIT License

Copyright (c) 2023 Filipe Lopes | FMFCLPRO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace NoodleLand.Entities.Living
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(Rigidbody2D))]
    public class LivingEntity : Entity
    {
        [SerializeField] protected Facing facing;
        [SerializeField] private LivingEntityProperties _livingEntityProperties;


        private Rigidbody2D rigidbody;
        private AnimationController _animationController;
        private SpriteRenderer _renderer;
        private Animator _animator;


        public const string AnimationWalkHorizontalTag = "Walk_Horizontal";
        public const string AnimationIdleTag = "Idle";
        public const string AnimationWalkUpTag = "Walk_Up";
        public const string AnimationWalkDownTag = "Walk_Down";


        public Facing FacingDirection => facing;


        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _animationController = new AnimationController(_animator);
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rigidbody.gravityScale = 0;
        }

        protected void SetVelocity(Vector2 velocity, float multiplier = 1)
        {
            rigidbody.velocity = velocity * _livingEntityProperties.Speed * multiplier;
        }

        protected void AddToVelocity(Vector2 velocity, float multiplier = 1)
        {
            rigidbody.velocity += velocity * _livingEntityProperties.Speed * multiplier;
        }

        private void ChangeAxis(Facing facing)
        {
            switch (facing)
            {
                case Facing.Left:
                    _renderer.flipX = false;
                    return;
                case Facing.Right:
                    _renderer.flipX = true;
                    return;
            }
        }

        protected void PlayAnimation(string animationTag)
        {
            _animationController.PlayAnimation(animationTag);
        }

        protected void SwitchFacing(Facing facing)
        {
            ChangeAxis(facing);
            this.facing = facing;
        }
    }
}