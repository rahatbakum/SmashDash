using UnityEngine.Events;

public interface IMassObject
{
    event UnityAction<float> MassChanged;
    float Mass {get; set;}
    float CurrentRadius {get;}
}