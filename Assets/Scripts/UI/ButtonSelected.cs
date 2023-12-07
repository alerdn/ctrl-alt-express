using UnityEngine;
using UnityEngine.UI;

public class ButtonSelected : MonoBehaviour
{
    private void Start() {
        GetComponent<Button>().Select();
    }
}
