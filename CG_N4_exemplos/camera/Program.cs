#define CG_Gizmo

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Mundo
{
  class Mundo : GameWindow
  {
    private float fovy, aspect, near, far;
    private Vector3 eye, at, up;
    
    public Mundo(int width, int height) : base(width, height) { }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.CullFace);
      
      // ___ parâmetros da câmera sintética
      fovy = (float)Math.PI / 4;
      aspect =  Width / (float)Height;
      near = 1.0f;
      far = 50.0f;
      eye = new Vector3(10, 10, 10);
      at = new Vector3(0, 0, 0);
      up = new Vector3(0, 1, 0);
    }
  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);

    GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
    Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, near, far);
    GL.MatrixMode(MatrixMode.Projection);
    GL.LoadMatrix(ref projection);
  }
  protected override void OnUpdateFrame(FrameEventArgs e)
  {
    base.OnUpdateFrame(e);
  }
  protected override void OnRenderFrame(FrameEventArgs e)
  {
    base.OnRenderFrame(e);
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

    Matrix4 modelview = Matrix4.LookAt(eye, at, up);
    GL.MatrixMode(MatrixMode.Modelview);
    GL.LoadMatrix(ref modelview);

#if CG_Gizmo
      Sru3D();
#endif
    DesenhaCubo();

    this.SwapBuffers();
  }

  private void DesenhaCubo()
  {
    GL.Color3(1.0f,1.0f,1.0f);
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
  }

#if CG_Gizmo
private void Sru3D()
    {
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(1.0f,0.0f,0.0f);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(0.0f,1.0f,0.0f);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      GL.Color3(0.0f,0.0f,1.0f);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }
#endif
}
class Program
{
  static void Main(string[] args)
  {
    GameWindow window = new Mundo(600, 600);
    window.Title = "camera";
    window.Run(1.0 / 60.0);
  }
}
}
