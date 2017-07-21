using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory ("Spine Playmaker")]
	[Tooltip ("Adds an animation to be played after the current or last queued animation for a track.")]
	public class SpineAddEmptyAnimation : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent (typeof(SkeletonAnimation))]
		[Tooltip ("The GameObject holding your animated character")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		public FsmInt mixDuration;
		
		[RequiredField]
		[Tooltip ("Track index. Leave a 0 if you don't know")]
		public FsmInt trackIndex;
		
		[RequiredField]
		[Tooltip ("Seconds to begin this animation after the start of the previous animation. May be less than zero to use the animation duration of the previous track minus any mix duration plus the delay.")]
		public FsmInt delay;	
		
		// Spine
		private Spine.AnimationState state;
		private SkeletonAnimation _skeletonAnimation;

		public override void Reset ()
		{
			gameObject = null;
			mixDuration = 0;
			trackIndex = 0;
			delay = 0;
		}

		public override void OnEnter ()
		{
			var go = Fsm.GetOwnerDefaultTarget (gameObject);
			_skeletonAnimation = go.GetComponent<SkeletonAnimation>();
			
			if (go = null) 
			{
				Finish ();
			}
			
			_skeletonAnimation.state.AddEmptyAnimation(trackIndex.Value, mixDuration.Value, delay.Value);
			Finish();
			
			}

	}
}