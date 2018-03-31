using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory ("Spine Playmaker")]
	[Tooltip ("Flip a spine animation along the X axis, based on a target.")]
	public class SpineFlipAnimation : FsmStateAction
	{
		[RequiredField]
        [CheckForComponent (typeof(SkeletonGraphic))]
        [CheckForComponent(typeof(RectTransform))]
		[Tooltip ("The GameObject holding your animated character")]
		public FsmOwnerDefault gameObject;

        [RequiredField]
        [Tooltip("The target the character should be looking at")]
        public FsmVector2 target;

		// Spine
        private SkeletonGraphic _skeletonGraphic;
		private Spine.AnimationState state;
		
		public override void Reset ()
		{
			gameObject = null;
            target = null;
		}

		public override void OnEnter ()
        {
			var go = Fsm.GetOwnerDefaultTarget (gameObject);

            if (go == null)
            {
                Finish();
                return;
            }

            _skeletonGraphic = go.GetComponent<SkeletonGraphic>();
            var rectTransform = go.GetComponent<RectTransform>();
			
            _skeletonGraphic.Skeleton.FlipX = target.Value.x > rectTransform.anchoredPosition.x;
			Finish();
		}

	}
}