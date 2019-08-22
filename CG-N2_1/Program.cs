﻿using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace OlaMundo
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
      GL.Ortho(0, 600, 0, 600, 0, 600);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.Color3(Color.Black);
      GL.Begin(PrimitiveType.Lines);
      GL.Vertex3(100, 100, 0); GL.Vertex3(500, 500, 0);
      GL.Vertex3(100, 100, 0); GL.Vertex3(500, 500, 0);
      GL.End();
      this.SwapBuffers();
    }
  }
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      GameWindow window = new Mundo(600, 600);
      window.Run(1.0 / 60.0);
    }
  }
}
