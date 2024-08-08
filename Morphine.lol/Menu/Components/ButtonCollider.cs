using UnityEngine;

namespace Morphine.Framework.Components
{
    public class ButtonCollider : MonoBehaviour
    {
        public Elements.ButtonInfo btn;
        public string relatedText;

        private void OnTriggerEnter(Collider collider)
        {
            if (Time.frameCount >= MenuBase.framePressCooldown + 20
                && MenuBase.buttonCollider == MenuBase.reference.GetComponent<BoxCollider>())
            {
                MenuBase.framePressCooldown = Time.frameCount;
                transform.localScale = new Vector3(transform.localScale.x / 3, transform.localScale.y, transform.localScale.z);
                GorillaTagger.Instance.StartVibration(false, 1f, .1f);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, false, .7f);
                MenuBase.Toggle(btn);
            }
        }
    }
}
