using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField, Range(0,10)] float rotationSpeed = 1;
    
    public int Value {get => value; }
    void Update()
    {
        transform.Rotate(0,360*rotationSpeed*Time.deltaTime,0);
    }
}
