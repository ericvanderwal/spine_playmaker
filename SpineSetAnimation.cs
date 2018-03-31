using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Spine Playmaker")]
    [Tooltip("Sets an animation to be played immediately")]
    public class SpineSetAnimation : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(SkeletonGraphic))]
        [Tooltip("The GameObject holding your animated character")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [Tooltip("Plain name of the animation you want to play. Example 'run'")]
        public FsmString animationName;
        [RequiredField]
        [Tooltip("Enable to loop this animation")]
        public FsmBool loop;
        [RequiredField]
        [Tooltip("Track index. Leave a 0 if you don't know")]
        public FsmInt trackIndex;

        [RequiredField]
        [Tooltip("Enable to immediately mark this as finished")]
        public FsmBool finishImmediately;

        // Spine
        private Spine.AnimationState state;
        private SkeletonGraphic _skeletonAnimation;

        public override void Reset()
        {
            gameObject = null;
            animationName = null;
            loop = false;
            trackIndex = 0;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            _skeletonAnimation = go.GetComponent<SkeletonGraphic>();

            if (go == null)
            {
                Finish();
            }

            _skeletonAnimation.AnimationState.SetAnimation(trackIndex.Value, animationName.Value, loop.Value);

            if (finishImmediately.Value)
            {
                Finish();
            }
            else
            {
                _skeletonAnimation.AnimationState.Complete += DoFinish;
            }
        }

        private void DoFinish(TrackEntry trackEntry)
        {
            _skeletonAnimation.AnimationState.Complete -= DoFinish;
            Finish();
        }

    }
}