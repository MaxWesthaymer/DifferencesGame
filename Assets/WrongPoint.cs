using UnityEngine;

public class WrongPoint : MonoBehaviour
{
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
    public void Show(Vector3 position)
    {
        transform.position = position;
        _animator.Play("Idle");
        _animator.SetTrigger("Click");
    }

    public void IncrementWrongScore()  //Call from animation
    {
        GameController.Instance.ClickOnWrongObject();
    }
    #endregion
}
