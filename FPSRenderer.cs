using Duality;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    [RequiredComponent(typeof(TextRenderer))]
    public class FPSRenderer : Component, ICmpUpdatable
    {
        public void OnUpdate()
        {
            this.GameObj.GetComponent<TextRenderer>().Text = new FormattedText(Duality.Time.Fps.ToString());
        }
    }
}
