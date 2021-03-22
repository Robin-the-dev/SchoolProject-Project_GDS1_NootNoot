using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildWithBanana : MonoBehaviour
{
    [SerializeField] private Banana banana;

    [SerializeField] private Animator animator;

    private static readonly int BananaTaken = Animator.StringToHash("BananaTaken");

    public void Activate()
    {
        //Show hud or animation?
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animator.SetBool(BananaTaken, true);
        yield return new WaitForSeconds(1);
        banana.Activate();
        yield return new WaitForSeconds(3);
        animator.SetBool(BananaTaken, false);
    }
}
