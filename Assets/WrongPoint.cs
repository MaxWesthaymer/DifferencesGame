using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongPoint : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    

    public void RunAnimation()
    {
        _animator.Play("Idle");
        _animator.SetTrigger("Click");
    }

    public void IncrementWrongScore()  //Call from animation
    {
        GameController.Instance.ClickOnWrongObject();
    }
}
