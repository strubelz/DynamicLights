using Duality;
using Duality.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class KeyControler : Component, ICmpUpdatable
    {
        Vector2 position;

        public void OnInit(InitContext context)
        {
            if (context != InitContext.Activate)
                return;

            position = new Vector2(0, 0);
        }

        public void OnUpdate()
        {
            var transformComponent = this.GameObj.GetComponent<Transform>();
            position.X = 0; position.Y = 0;

            if (DualityApp.Keyboard.KeyPressed(Duality.Input.Key.W))
                position.Y -= 10f;
            if (DualityApp.Keyboard.KeyPressed(Duality.Input.Key.A))
                position.X -= 10f;
            if (DualityApp.Keyboard.KeyPressed(Duality.Input.Key.S))
                position.Y += 10f;
            if (DualityApp.Keyboard.KeyPressed(Duality.Input.Key.D))
                position.X += 10f;

            transformComponent.MoveBy(position);
        }
    }
}
