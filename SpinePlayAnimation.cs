using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory ("Spine Playmaker")]
	[Tooltip ("Run a animation for spine by name.")]
	public class SpinePlayAnimation : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent (typeof(SkeletonAnimation))]
		[Tooltip ("The GameObject holding your animated character")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip ("Plain name of the animation you want to play. Example 'run'")]
		public FsmString animationName;
		[RequiredField]
		[Tooltip ("Enable to loop this animation")]
		public FsmBool loop;
		[RequiredField]
		[Tooltip ("Track index. Leave a 0 if you don't know")]
		public FsmInt trackIndex;
		
		[Tooltip ("Optionally set a time scale")]
		public FsmFloat timeScale;
		[Tooltip("Optional event to send when the animation is completed. If no event is used, this action will finish immediately.")]
		public FsmEvent finishEvent;
		
		// Spine
		private Spine.AnimationState state;
		private SkeletonAnimation _skeletonAnimation;

		public override void Reset ()
		{
			gameObject = null;
			animationName = null;
			loop = false;
			trackIndex = 0;
			finishEvent = null;
			timeScale = new FsmFloat {UseVariable = true};
		}

		public override void OnEnter ()
		{
			var go = Fsm.GetOwnerDefaultTarget (gameObject);
			_skeletonAnimation = go.GetComponent<SkeletonAnimation>();
			
			if (go == null) 
			{
				Finish ();
			}
			
			if(finishEvent != null)
			{
				if(timeScale.Value != 0)
				{
					_skeletonAnimation.state.TimeScale = timeScale.Value;
					
				}
				
				_skeletonAnimation.AnimationState.Complete += CompleteEvent;
				_skeletonAnimation.state.SetAnimation(trackIndex.Value, animationName.Value, loop.Value);
			}
			
			else
			{
				if(timeScale.Value != 0)
				{
					_skeletonAnimation.state.TimeScale = timeScale.Value;

				}
				
				_skeletonAnimation.state.SetAnimation(trackIndex.Value, animationName.Value, loop.Value);
				Finish();
			}

		}

		void CompleteEvent (Spine.TrackEntry trackEntry) 
		{
			Fsm.Event(finishEvent);
		}

	}
}