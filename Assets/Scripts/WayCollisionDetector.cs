using UnityEngine;
using System;

public static class WayCollisionDetector
{
    public static Collider GetFirstCollision(Vector3 startPosition, Vector3 finishPosition, float radius, Predicate<Collider> condition)
    {
        Collider[] colliders = Physics.OverlapCapsule(startPosition, finishPosition, radius);
        int indexOfMinDistance = -1;
        float minDistance = int.MaxValue;
        for(int i = 0; i < colliders.Length; i++)
        {
            if(condition(colliders[i]))
            {
                float distance = Vector3.Distance(startPosition, colliders[i].transform.position);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    indexOfMinDistance = i;
                }
            }
        }

        if(indexOfMinDistance < 0)
            return null;
        return colliders[indexOfMinDistance];
    }
}
