using System;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class ClickableObject : MonoBehaviour
{
    public  int ArrayIndex { get; private set; }
    public void Setup(int index)
    {
        ArrayIndex = index;
    }

    public void RunAnimation()
    {
        
    }
}
