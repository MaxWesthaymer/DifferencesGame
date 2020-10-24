using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField] private WrongPoint wrongPointPrefab;
    private WrongPoint _wrongPoint;
    // Start is called before the first frame update
    void Start()
    {
        _wrongPoint = Instantiate(wrongPointPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.transform.parent.GetComponent<ClickableObject>() != null)
            {
                GameController.Instance.ClickOnCorrectObject(hit.transform.parent.GetComponent<ClickableObject>().ArrayIndex);
            }
            else
            {
                Vector2 mouseToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _wrongPoint.transform.position = mouseToWorldPoint;
                _wrongPoint.RunAnimation();
            }
        }
    }
}
