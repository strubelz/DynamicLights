using Duality;
using Duality.Components.Renderers;
using Duality.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class LightCounter : Component, ICmpUpdatable
    {
        public void OnUpdate()
        {
            /*GameObject obj = new GameObject();
            Scene.Current.FindGameObject("DynamicLight").CopyTo(obj);
            Scene.Current.AddObject(obj);*/

            this.GameObj.GetComponent<TextRenderer>().Text = new Duality.Drawing.FormattedText(DynamicLights.DynamicLight.lights.ToString());
        }
    }
}
