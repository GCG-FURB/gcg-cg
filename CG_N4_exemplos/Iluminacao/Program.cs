/**
Fonte: https://github.com/mono/opentk/blob/master/Source/Examples/OpenGL/1.x/VertexLighting.cs
 */
using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Iluminacao
{
  class Mundo : GameWindow
  {
    private bool ligaLuz = true;
    private OpenTK.Color cor = OpenTK.Color.White;

    public Mundo(int width, int height) : base(width, height) { }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      GL.ClearColor(OpenTK.Color.Gray);
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.CullFace);

      // Enable Light 0 and set its parameters.
      GL.Light(LightName.Light0, LightParameter.Position, new float[] { 0.0f, 2.0f, 0.0f });
      GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
      GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.Light(LightName.Light0, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.LightModel(LightModelParameter.LightModelAmbient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
      GL.LightModel(LightModelParameter.LightModelTwoSide, 1);
      GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);

      // Use GL.Material to set your object's material parameters.
      GL.Material(MaterialFace.Front, MaterialParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
      GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.Material(MaterialFace.Front, MaterialParameter.Emission, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });

      //FIXME: cor só aparece nas superfícies laterais. Ter mais tipos de luz.      
      GL.Material(MaterialFace.Front, MaterialParameter.ColorIndexes, cor);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 50.0f);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadMatrix(ref projection);
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      Matrix4 modelview = Matrix4.LookAt(eye: new Vector3(5, 5, 5), target: new Vector3(0, 0, 0), up: Vector3.UnitY);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadMatrix(ref modelview);

      SRU3D();

      DesenhaCubo();

      SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.Escape)
        this.Exit();
      else
        if (e.Key == Key.L)
        ligaLuz = !ligaLuz;
      else if (e.Key == Key.W)
        cor = OpenTK.Color.White;
      else if (e.Key == Key.R)
        cor = OpenTK.Color.Red;
      else if (e.Key == Key.G)
        cor = OpenTK.Color.Green;
      else if (e.Key == Key.B)
        cor = OpenTK.Color.Blue;
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
    }

    private void DesenhaCubo()
    {
      if (ligaLuz)
      {
        GL.Enable(EnableCap.Lighting);
        GL.Enable(EnableCap.Light0);
        GL.Enable(EnableCap.ColorMaterial);
      }

      GL.Color3(cor);
      GL.Begin(PrimitiveType.Quads);

      // Face da frente
      GL.Normal3(0, 0, 1);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
      // Face do fundo
      GL.Normal3(0, 0, -1);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
      // Face de cima
      GL.Normal3(0, 1, 0);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
      // Face de baixo
      GL.Normal3(0, -1, 0);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
      // Face da direita
      GL.Normal3(1, 0, 0);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
      // Face da esquerda
      GL.Normal3(-1, 0, 0);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);

      GL.End();

      if (ligaLuz)
      {
        GL.Disable(EnableCap.Lighting);
        GL.Disable(EnableCap.Light0);
      }
    }

    private void SRU3D()
    {
      GL.LineWidth(3);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(OpenTK.Color.Red);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(OpenTK.Color.Green);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      GL.Color3(OpenTK.Color.Blue);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }

  }

  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = new Mundo(600, 600);
      window.Run(1.0 / 60.0);
    }
  }

}