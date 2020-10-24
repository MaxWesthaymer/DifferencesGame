using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    #region InspectorFields
    [SerializeField] private WrongPoint wrongPointPrefab;
    #endregion
    
    #region PrivateFields
    private WrongPoint _wrongPoint;
    private Camera mainCamera;
    #endregion
    
    #region UnityMethods
    private void Start()
    {
        _wrongPoint = Instantiate(wrongPointPrefab, Vector3.zero, Quaternion.identity);
        mainCamera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.transform.parent.GetComponent<ClickableObject>() != null)
            {
                GameController.Instance.ClickOnCorrectObject(hit.transform.parent.GetComponent<ClickableObject>().ArrayIndex);
            }
            else
            {
                Vector2 mouseToWorldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _wrongPoint.Show(mouseToWorldPoint);
            }
        }
    }
    #endregion
}
