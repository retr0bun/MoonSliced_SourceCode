using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public Gun ammo;

    public Text ammoText;
    

    void Update()
    {
        ammoText.text = ammo.currentAmmo.ToString();
    }
}
