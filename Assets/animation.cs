using UnityEngine;
using System.Collections;

public class animation : StateMachineBehaviour {

	 
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        if (stateInfo.IsName("punching")) 
        {
            animator.GetComponent<controler>().setPunchBool(true);
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.F) && !animator.GetCurrentAnimatorStateInfo(0).IsName("punching") && animator.GetAnimatorTransitionInfo(0).normalizedTime < 0.10f)
        {
            animator.SetTrigger("punch");
            //punchCollider.isTrigger = true;
        }
	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        if (!stateInfo.IsName("punching"))
        {            
            animator.GetComponent<controler>().setPunchBool(false);
        }
	
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
