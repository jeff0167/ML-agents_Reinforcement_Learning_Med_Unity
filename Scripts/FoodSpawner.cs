using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public bool HasFoodSpawned;
    public void FoodHasSpawned()
    {
        HasFoodSpawned = true;
    }

    public void FoodDespawned()
    {
        HasFoodSpawned = false;
    }
}
