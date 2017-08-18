using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;
using Duality.Editor;
using Duality.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Duality.Components.Camera;

namespace DynamicLights
{
    [RequiredComponent(typeof(Camera), typeof(Camera))]
    public class DynamicLight : Renderer, ICmpInitializable
    {

        private int lightSize = 100;
        [DontSerialize]private Camera camera;

        [DontSerialize] private RenderTarget occlusionMapTarget;
        [DontSerialize] private ContentRef<Texture> occlusionMap;

        [DontSerialize] private ContentRef<Texture> shadowMap;
        [DontSerialize] private Canvas shadowMapCanvas;
        [DontSerialize] private RenderTarget shadowMapTarget;
        [DontSerialize] private DrawDevice shadowMapDevice;
        [DontSerialize] private BatchInfo shadowMapMaterial;

        [DontSerialize] private Canvas finalCanvas;
        [DontSerialize] private BatchInfo finalMaterial;

        private ColorRgba color = ColorRgba.White;

        public ColorRgba Color
        {
            get { return color; }
            set { this.color = value; }
        }

        public int LightSize
        {
            get { return this.lightSize; }
            set { this.lightSize = value; }
        }

        public override float BoundRadius
        {
            get { return (float)(Math.Sqrt(0.5f * this.lightSize * this.lightSize)); }
        }

        public void OnInit(InitContext context)
        {
            if (context != InitContext.Activate)
                return;

            //Setup occlusion map and render target
            this.occlusionMap = new ContentRef<Texture>(new Texture(this.lightSize, this.lightSize, TextureSizeMode.NonPowerOfTwo, TextureMagFilter.Linear, TextureMinFilter.Linear));

            this.occlusionMapTarget = new RenderTarget();
            this.occlusionMapTarget.Targets = new ContentRef<Texture>[] { this.occlusionMap };

            //Setup shadow map
            this.shadowMap = new ContentRef<Texture>(new Texture(this.lightSize, 1, TextureSizeMode.NonPowerOfTwo, TextureMagFilter.Linear, TextureMinFilter.Linear));

            this.shadowMapTarget = new RenderTarget();
            this.shadowMapTarget.Targets = new ContentRef<Texture>[] { this.shadowMap };

            this.shadowMapMaterial = new BatchInfo();
            this.shadowMapMaterial.MainColor = ColorRgba.White;
            this.shadowMapMaterial.MainTexture = this.occlusionMap;
            this.shadowMapMaterial.Technique = ContentProvider.RequestContent<DrawTechnique>(@"Data\DynamicLights\Shaders\Techniques\ShadowMapTech.DrawTechnique.res");

            this.shadowMapDevice = new DrawDevice();
            shadowMapDevice.Perspective = PerspectiveMode.Flat;
            shadowMapDevice.VisibilityMask = VisibilityFlag.AllGroups | VisibilityFlag.ScreenOverlay;
            shadowMapDevice.RenderMode = RenderMatrix.OrthoScreen;
            shadowMapDevice.Target = this.shadowMapTarget;
            shadowMapDevice.ViewportRect = new Rect(this.lightSize, 1);


            this.finalMaterial = new BatchInfo(ContentProvider.RequestContent<DrawTechnique>(@"Data\DynamicLights\Shaders\Techniques\FinalTech.DrawTechnique.res"), ColorRgba.White, this.shadowMap);

            //Setup camera
            this.camera = GameObj.GetComponent<Camera>();
            camera.FocusDist = 100;
            camera.VisibilityMask = VisibilityFlag.AllGroups & ~VisibilityFlag.Group30;

            camera.Passes.Clear();

            Pass occlusionPass = new Pass();
            occlusionPass.Output = this.occlusionMapTarget;
            occlusionPass.VisibilityMask = VisibilityFlag.AllGroups & ~VisibilityFlag.Group30;

            camera.Passes.Add(occlusionPass);
        }


        public override void Draw(IDrawDevice device)
        {
            //Reset camera Y position
            Transform transform = this.GameObj.GetComponent<Transform>();
            Vector2 pos2 = transform.Pos.Xy;
            transform.Pos = new Vector3(pos2, -100);

            this.finalMaterial.SetUniform("lightSize", this.lightSize);
            this.shadowMapMaterial.SetUniform("lightSize", this.lightSize);


            this.shadowMapDevice.PrepareForDrawcalls();

            if (this.shadowMapCanvas == null)
                this.shadowMapCanvas = new Canvas(this.shadowMapDevice);
            this.shadowMapCanvas.State.SetMaterial(this.shadowMapMaterial);
            this.shadowMapCanvas.FillRect(0, 0, this.lightSize, 1);

            this.shadowMapDevice.Render(ClearFlag.All, ColorRgba.TransparentBlack, 1.0f);


            //Render light to screen

            if (this.finalCanvas == null)
                this.finalCanvas = new Canvas(device);
            this.finalCanvas.State.SetMaterial(this.finalMaterial);
            this.finalCanvas.State.ColorTint = this.color;
            this.finalCanvas.FillRect(transform.Pos.X - this.lightSize/2, transform.Pos.Y - this.lightSize/2, 0, this.lightSize, this.lightSize);

        }

        public void OnShutdown(ShutdownContext context)
        {
        }
    }
}
