using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Orb : MonoBehaviour
{
    public static event Action<Orb> Collected;

    [SerializeField] private int value = 1;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float collectDistance = 0.2f;

    public int Value => value;

    private Transform target;
    private Collider2D col;
    private float collectDistanceSqr;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        collectDistanceSqr = collectDistance * collectDistance;

        enabled = false; // idle orbs run no Update at all
    }

    public void Attract(Transform newTarget)
    {
        if (enabled) return; // already attracted

        target = newTarget;
        col.enabled = false; // detected once, no more physics needed
        enabled = true;      // Update starts running now

        Debug.Log($"{name}: attracted");
    }

    private void Update()
    {
        Vector2 current = transform.position;
        Vector2 destination = target.position;

        if ((destination - current).sqrMagnitude <= collectDistanceSqr)
        {
            Collect();
            return;
        }

        transform.position = Vector2.MoveTowards(current, target.position, speed * Time.deltaTime);
    }

    private void Collect()
    {
        enabled = false; // prevents a second collect
        Debug.Log($"{name}: collected, value {value}");

        // TO DO: Subscribe this event for the XP
        //        OBject Pool

        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}