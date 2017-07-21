using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory ("Spine Playmaker")]
	[Tooltip ("Flip a spine animation along the X or Y axis.")]
	public class SpineFlipAnimation : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent (typeof(SkeletonAnimation))]
		[Tooltip ("The GameObject holding your animated character")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip ("Flip animation along the X axis")]
		public FsmBool flipX;
		
		[Tooltip ("Flip animation along the Y axis")]
		public FsmBool flipY;

		// Spine
		private SkeletonAnimation _skeletonAnimation;
		private Spine.Skeleton _skeleton;
		private Spine.AnimationState state;
		
		public override void Reset ()
		{
			gameObject = null;
			flipX = false;
			flipY = false;
		}

		public override void OnEnter ()
		{
			var go = Fsm.GetOwnerDefaultTarget (gameObject);
			_skeletonAnimation = go.GetComponent<SkeletonAnimation>();
			_skeleton = _skeletonAnimation.Skeleton;
			
			if (go = null) 
			{
				Finish ();
			}
			
			if(flipX.Value)
			{
				_skeleton.FlipX = true;
			}
			if(flipY.Value)
			{
				_skeleton.FlipY = true;
			}
			Finish();
		}

	}
}