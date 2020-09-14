/****
    GL.GenLists: https://docs.microsoft.com/en-us/windows/win32/opengl/glgenlists
    GL.NewList: https://docs.microsoft.com/en-us/windows/win32/opengl/glnewlist
    GL.EndList: https://docs.microsoft.com/en-us/windows/win32/opengl/glendlist    
    GL.IsList: https://docs.microsoft.com/en-us/windows/win32/opengl/glislist
    GL.CallList: https://docs.microsoft.com/en-us/windows/win32/opengl/glcalllist
    GL.CallLists: https://docs.microsoft.com/en-us/windows/win32/opengl/glcalllists
    GL.DeleteLists: https://docs.microsoft.com/en-us/windows/win32/opengl/gldeletelists
*/

#define CG_Gizmo

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

//TODO: ter uma rotina que mostre a taxa de FPS para mostrar a diferença com e sem o uso da DisplayList

namespace Mundo
{
  class Mundo : GameWindow
  {
    private float fovy, aspect, near, far;
    private Vector3 eye, at, up;
    private int displayLista;
    private byte clones = 1;
    private bool comDisplayList = true;

    public Mundo(int width, int height) : base(width, height) { }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      GL.ClearColor(Color.Gray);
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.CullFace);

      // ___ parâmetros da câmera sintética
      fovy = (float)Math.PI / 4;
      aspect = Width / (float)Height;
      near = 1.0f;
      far = 400.0f;
      eye = new Vector3(200, 200, 200);
      at = new Vector3(0, 0, 0);
      up = new Vector3(0, 1, 0);

      displayLista = GL.GenLists(1);
      if (displayLista != 0)
      {
        GL.NewList(displayLista, ListMode.Compile);
        DesenhaMundoCubo();
        GL.EndList();
      }

    }

    protected override void OnUnload(EventArgs e)
    {
      GL.DeleteLists(1, 1);
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
      // GL.PushMatrix();
      // GL.Translate(25, 25, 25);  // ATENÇÃO: não usar, usar classe Transformacao4D.cs em CG_Biblioteca
      // GL.Scale(50, 50, 50);  // ATENÇÃO: não usar, usar classe Transformacao4D.cs em CG_Biblioteca
      // DesenhaCubo();
      // GL.PopMatrix();

      if (comDisplayList)
      {
        GL.CallList(1);
      }
      else
      {
        DesenhaMundoCubo();
      }

      this.SwapBuffers();
    }

    private void DesenhaMundoCubo()
    {
      for (int qtd = 0; qtd < clones; qtd++)
      {
        for (int eixoY = 0; eixoY < 100; eixoY += 2)
        {
          for (int eixoX = 0; eixoX < 100; eixoX += 2)
          {
            for (int eixoZ = 0; eixoZ < 100; eixoZ += 2)
            {
              GL.PushMatrix();
              GL.Translate(eixoX, eixoY, eixoZ);  // ATENÇÃO: não usar, usar classe Transformacao4D.cs em CG_Biblioteca
              DesenhaCubo();
              GL.PopMatrix();
            }
          }
        }
      }
    }

    private void DesenhaCubo()
    {
      GL.Begin(PrimitiveType.Quads);
      // Face da frente
      GL.Color3(1.0, 0.0, 0.0);
      GL.Normal3(0, 0, 1);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
      // Face do fundo
      GL.Color3(0.0, 1.0, 0.0);
      GL.Normal3(0, 0, -1);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
      // Face de cima
      GL.Color3(0.0, 0.0, 1.0);
      GL.Normal3(0, 1, 0);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
      // Face de baixo
      GL.Color3(1.0, 1.0, 0.0);
      GL.Normal3(0, -1, 0);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
      // Face da direita
      GL.Color3(0.0, 1.0, 1.0);
      GL.Normal3(1, 0, 0);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
      // Face da esquerda
      GL.Color3(1.0, 0.0, 1.0);
      GL.Normal3(-1, 0, 0);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);

      GL.End();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.D)
      {
        comDisplayList = !comDisplayList;
      }
      else if (e.Key == Key.Minus && clones > 1)
        clones -= 1;
      else if (e.Key == Key.Plus)
        clones += 1;
      if (comDisplayList)
        Console.WriteLine("COM display: " + clones);
      else
        Console.WriteLine("SEM display: " + clones);
    }

#if CG_Gizmo
    private void Sru3D()
    {
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(Color.Red);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(Color.Green);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      GL.Color3(Color.Blue);
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
      window.Title = "DisplayList";
      window.Run(1.0 / 60.0);
    }
  }
}
