using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    public bool newFlower = true;
    public ParticleSystem flowerPS;

    public void Launch()
    {
        if (!newFlower) return;
        newFlower = false;
        flowerPS.transform.position = transform.position;
        flowerPS.Emit(20);
    }
}
