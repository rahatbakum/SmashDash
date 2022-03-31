using UnityEngine.Events;

public interface IExplodingObject
{
    event UnityAction Exploding;
    event UnityAction Exploded;
    void Explode();    
}
