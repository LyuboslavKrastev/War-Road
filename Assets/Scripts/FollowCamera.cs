﻿using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void Update()
    {
        transform.position = _target.position;
    }
}
