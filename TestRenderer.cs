using Duality.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duality.Drawing;
using Duality;
using Duality.Resources;

namespace DynamicLights
{
     public class TestRenderer : Renderer
    {
        public override float BoundRadius
        {
            get { return 100.0f; }
        }

        public override void Draw(IDrawDevice device)
        {

            Canvas canvas = new Canvas(device);
            Vector3 vec = this.GameObj.Transform.Pos;

            canvas.State.ColorTint = ColorRgba.Green;
            canvas.State.SetMaterial(ContentProvider.RequestContent<Material>(@"Data\DynamicLights\DualityIcon.Material.res"));
            canvas.FillCircle(vec.X, vec.Y, vec.Z, this.BoundRadius);


            /*RenderTarget target = new RenderTarget();
            Texture tex = new Texture();
            tex.Size = new Vector2(100, 100);

            ContentProvider.AddContent(@"Data\Temp\tex.Texture.res", tex);
            target.Targets = new ContentRef<Texture>[] { ContentProvider.RequestContent<Texture>(@"Data\Temp\tex.Texture.res") };

            RenderTarget target2 = new RenderTarget();
            Texture tex2 = new Texture();
            tex2.Size = new Vector2(100, 100);

            ContentProvider.AddContent(@"Data\Temp\tex2.Texture.res", tex2);
            target2.Targets = new ContentRef<Texture>[] { ContentProvider.RequestContent<Texture>(@"Data\Temp\tex2.Texture.res") };

            BatchInfo batch = new BatchInfo();
            batch.Technique = ContentProvider.RequestContent<DrawTechnique>(@"Data/ExampleTechnique");
            //this wont work, because batch.Textures is a IEnumerable<KeyValuePair<string, Texture>>()
            batch.Textures = tex2;

            ContentProvider.AddContent(@"Data\Temp\target2.RenderTarget.res", target2);
            
            DrawDevice dev = new DrawDevice();
            dev.Target = ContentProvider.RequestContent<RenderTarget>(@"Data\Temp\target2.RenderTarget.res");

            Canvas canv = new Canvas(dev);
            canv.State.SetMaterial(batch);
            canv.FillRect(0, 0, 100, 100); */

        }
    }
}
