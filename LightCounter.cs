using Duality;
using Duality.Components;
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
            this.GameObj.GetComponent<TextRenderer>().Text = new Duality.Drawing.FormattedText(Scene.Current.FindComponents<Camera>().Where(c => c.Active).Count().ToString());
        }
    }
}
