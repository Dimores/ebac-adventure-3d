using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float dist = .2f;
    public float speed = 3f;

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) > dist)
        {
            speed += 0.1f;
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);
        }
    }
}
