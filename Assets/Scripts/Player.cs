using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Quaternion _rotationTarget;
    private float _angle = 0.0f;
    private float _timeCount = 0.0f;
    public Vector3 target;
    public float speed = 40f;
    public float moveSpeed = 5.0f;

    private void Start()
    {
        target = transform.position;
        _rotationTarget = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var point = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(point, out var hit))
            {
                if (hit.transform.gameObject.CompareTag("Floor"))
                {
                    target = hit.point + transform.up * 0.5f;
                    Debug.Log(target);
                    Debug.DrawLine(transform.position, target, Color.magenta, 2.0f);
                    var forwardSameMagnitude =
                        transform.forward * (transform.position - target).magnitude + transform.position;
                    Debug.DrawLine(transform.position, forwardSameMagnitude, Color.red, 2.0f);
                    _angle = Vector3.SignedAngle((forwardSameMagnitude - transform.position).normalized,
                        (target - transform.position).normalized, transform.up);
                    
                    Debug.Log(_angle);
                    Debug.Log(Mathf.Deg2Rad * _angle);

                    _rotationTarget = transform.rotation * Quaternion.Euler(0, _angle, 0);
                }
            }
        }

        if (Mathf.Abs(_rotationTarget.eulerAngles.y - transform.rotation.eulerAngles.y) <= Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotationTarget, speed * Time.deltaTime);
        }
        Debug.DrawLine(transform.position, transform.forward * 5.0f + transform.position, Color.red);
    }
}
