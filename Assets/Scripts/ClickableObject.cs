using UnityEngine;
[RequireComponent(typeof(Animator))]
public class ClickableObject : MonoBehaviour
{
    #region Propierties
    public  int ArrayIndex { get; private set; }
    #endregion
    
    #region PrivateFields
    private Animator _animator;
    #endregion
    
    #region UnityMethods
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    #endregion

    #region PublicMethods
    public void SetIndex(int index)
    {
        ArrayIndex = index;
    }

    public void RunAnimation()
    {
        _animator.Play("Popup");
    }
    #endregion
}
