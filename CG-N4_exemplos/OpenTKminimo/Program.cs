/*
@startuml
  participant "OnLoad()" as A
  participant "OnUpdateFrame()" as B
  participant "OnRenderFrame()" as C
activate A
  A -> B:  __inicializa
deactivate A
activate B
  B -> C: __desenhar
  activate C
    C --> B: __ajustes
  deactivate B
deactivate C
@enduml
*/

// #define CG_Gizmo

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Mundo
{
  class Mundo : GameWindow
  {
    public Mundo(int width, int height) : base(width, height) { }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      GL.ClearColor(Color.Gray);
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(-300, 300, -300, 300, -1, 1);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();
#if CG_Gizmo      
      Sru3D();
#endif      
      GL.Color3(Color.Yellow);
      GL.Begin(PrimitiveType.Lines);
      GL.Vertex2(0, 0); GL.Vertex2(100, 100);
      GL.End();
      this.SwapBuffers();
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
      window.Title = "OpenTKminimo";
      window.Run(1.0 / 60.0);
    }
  }
}
