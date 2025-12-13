using UnityEngine;
using UnityEngine.InputSystem;

public class BWController : MonoBehaviour
{
    public Material mat;

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            bool bw = mat.GetFloat("_BlackWhite") > 0.5f;
            mat.SetFloat("_BlackWhite", bw ? 0 : 1);
        }
    }
}
