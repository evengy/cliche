using Assets.Scripts.Interactive;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.TV
{
    public class TVSwitchController : MonoBehaviour
    {
        [SerializeField] PickableObject tvSwitchPrefab;
        [SerializeField] Pin[] pins;
        bool instantiated;
        bool usable;
        PickableObject tvSwitch;
        void Update()
        {
            if (!instantiated && pins.Where(p=>!p.Opened).Count() == 1)
            {
                var pin = pins.First(p => !p.Opened).gameObject;
                tvSwitch = Instantiate(tvSwitchPrefab, pin.transform.localPosition, Quaternion.identity);
                instantiated = true;
                tvSwitch.Reassign(pin);
            }
            if (!usable && instantiated && pins.Where(p => !p.Opened).Count() == 0)
            {
                tvSwitch.UI.gameObject.SetActive(true);
                usable = true;
            }
        }
    }
}