using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJointDrop : MonoBehaviour
{

    private SpringJoint spring;
    public float dropForce;

    // Start is called before the first frame update
    void Start()
    {
        spring = GetComponent<SpringJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(spring.currentForce.x) > dropForce | Mathf.Abs(spring.currentForce.y) > dropForce | Mathf.Abs(spring.currentForce.z) > dropForce && transform.childCount > 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                HeldObject heldObject = transform.GetChild(i).GetComponent<HeldObject>();
                heldObject.CurrentState.ObjectDropped(heldObject);
            }
        }
    }
}
