using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AnimationDisplayer : Displayer
    {
        private const string _showClipName = "Show";
        private const string _hideClipName = "Hide";
        
        [SerializeField] 
        private Animation _animation;
        [SerializeField] 
        private AnimationClip _showAnimation;
        [SerializeField] 
        private AnimationClip _hideAnimation;

        private async void Awake()
        {
            _animation.AddClip(_showAnimation, _showClipName);
            _animation.AddClip(_hideAnimation, _hideClipName);
            await Hide(true);
        }

        private void Reset()
        {
            _animation = GetComponent<Animation>();
        }

        public override async Task Show(bool immediately)
        {
            await PlayAnimation(immediately, _showClipName, _showAnimation.length);
        }

        public override async Task Hide(bool immediately)
        {
            await PlayAnimation(immediately, _hideClipName, _hideAnimation.length);
        }

        private async Task PlayAnimation(bool immediately, string clipName, float clipLength)
        {
            _animation.Play(clipName, PlayMode.StopAll);
            if (immediately)
            {
                _animation[clipName].time = clipLength;
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(clipLength));
                _animation[clipName].time = clipLength;
            }
        }
    }
}