using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory ("Spine Playmaker")]
	[Tooltip ("Find the mix duration between two animations.")]
	public class SpineGetMixDuration : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent (typeof(SkeletonAnimation))]
		[Tooltip ("The GameObject holding your animated character")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip ("String name of animation one")]
		public FsmString animation1;
		
		[RequiredField]
		[Tooltip ("String name of animation two")]
		public FsmString animation2;
		
		[UIHint(UIHint.Variable)]
		[Tooltip ("Length of mix duration")]
		public FsmFloat mixDuration;
		
		// Spine
		private SkeletonAnimation _skeletonAnimation;

		public override void Reset ()
		{
			gameObject = null;
			mixDuration = null;
			animation1 = null;
			animation2 = null;
		}

		public override void OnEnter ()
		{
			var go = Fsm.GetOwnerDefaultTarget (gameObject);
			_skeletonAnimation = go.GetComponent<SkeletonAnimation>();
			
			if (go = null) 
			{
				Finish ();
			}
			
			var _animation1 = _skeletonAnimation.skeleton.data.FindAnimation(animation1.Value);
			var _animation2 = _skeletonAnimation.skeleton.data.FindAnimation(animation2.Value);
			mixDuration.Value = _skeletonAnimation.AnimationState.Data.GetMix(_animation1, _animation2);
			Finish();
			
			}

	}
}