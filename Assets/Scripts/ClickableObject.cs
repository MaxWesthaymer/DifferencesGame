using System;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class ClickableObject : MonoBehaviour
{
    public  int ArrayIndex { get; private set; }
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIndex(int index)
    {
        ArrayIndex = index;
    }

    public void RunAnimation()
    {
        _animator.Play("Popup");
    }
}
