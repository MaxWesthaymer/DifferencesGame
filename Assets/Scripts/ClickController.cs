using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.transform.GetComponent<ClickableObject>() != null)
            {
                GameController.Instance.ClinckOnCurrectObject(hit.transform.GetComponent<ClickableObject>().ArrayIndex);
            }
            else
            {
                Debug.Log("ДУРАК ЕБАТЬ");
            }
        }
    }
}
