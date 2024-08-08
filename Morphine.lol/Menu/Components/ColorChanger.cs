using UnityEngine;

namespace Morphine.Framework.Components
{
    public class ColorChanger : TimedBehaviour
    {
        public override void Start()
        {
            base.Start();
            gameObjectRenderer = base.GetComponent<Renderer>();
            Update();
        }

        public override void Update()
        {
            base.Update();
            if (colors != null)
            {
                if (timeBased)
                {
                    color = colors.Evaluate(progress);
                }
                gameObjectRenderer.material.color = color;
            }
        }

        public Renderer gameObjectRenderer;
        public Gradient colors = null;
        public Color32 color;
        public bool timeBased = true;
    }
}
