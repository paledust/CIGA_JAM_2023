using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_IK : MonoBehaviour
{
[Header("Joints")]
    [SerializeField] private Transform joint_a;
    [SerializeField] private Transform joint_b;
    [SerializeField] private Transform joint_hand;
[Header("Target")]
    [SerializeField] private Transform target;
    private float length0;
    private float length1;
    private float length0_pow;
    private float length1_pow;
    void Start()
    {
        length0 = Vector2.Distance(joint_a.position, joint_b.position);
        length1 = Vector2.Distance(joint_b.position, joint_hand.position);

        length0_pow = length0 * length0;
        length1_pow = length1 * length1;
    }

    void Update()
    {
        float length2 = Vector2.Distance(joint_a.position, target.position);
        float length2_pow = length2 * length2;

        Vector2 diff = target.position - joint_a.position;
        float atan = Mathf.Atan2(-diff.x, diff.y) * Mathf.Rad2Deg;

        float jointAngle_a = 0;
        float jointAngle_b = 0;

        if(length0 + length1 < length2){
            jointAngle_a = atan;
            jointAngle_b = 0f;
        }
        else{
            float cos_a = (length2_pow + length0_pow - length1_pow)/(2*length2*length0);
            float angle_a = Mathf.Acos(cos_a) * Mathf.Rad2Deg;

            float cos_b = (length1_pow + length0_pow - length2_pow)/(2 * length1 * length0);
            float angle_b = Mathf.Acos(cos_b) * Mathf.Rad2Deg;

            jointAngle_a = atan - angle_a;
            jointAngle_b = 180f - angle_b;
        }

        Vector3 euler_a = joint_a.transform.localEulerAngles;
        euler_a.z = jointAngle_a;
        joint_a.transform.localEulerAngles = euler_a;

        Vector3 euler_b = joint_b.transform.localEulerAngles;
        euler_b.z = jointAngle_b;
        joint_b.transform.localEulerAngles = euler_b;
    }
}
