using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory ("Spine Playmaker")]
	[Tooltip ("Set spine skeleton animator time scale.")]
	public class SpineSetTimeScale : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent (typeof(SkeletonAnimation))]
		[Tooltip ("The GameObject holding your animated character")]
		public FsmOwnerDefault gameObject;

		[Tooltip ("Set animation time scale")]
		public FsmFloat timeScale;
		
		public FsmBool everyframe;
		
		// Spine

		private SkeletonAnimation _skeletonAnimation;

		public override void Reset ()
		{
			gameObject = null;
			timeScale = 1;
			everyframe = false;
		}

		public override void OnEnter ()
		{
			var go = Fsm.GetOwnerDefaultTarget (gameObject);
			_skeletonAnimation = go.GetComponent<SkeletonAnimation>();
			
			if (go == null) 
			{
				Finish ();
			}
			
			DoSpineChange();
			
			if (!everyframe.Value)
			{
				Finish();
			}

		}
		
		public override void OnUpdate()
		{
			DoSpineChange();
		}
		
		void DoSpineChange()
		{
			_skeletonAnimation.timeScale = timeScale.Value;
			
		}

	}
}