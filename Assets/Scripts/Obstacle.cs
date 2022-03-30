using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public const string DefaultTag = "Obstacle";

    public static Obstacle GetObstacle(Collider collider)
    {
        return collider.transform.parent.GetComponent<Obstacle>();
    }

    public void Explode()
    {
        
    }
}
