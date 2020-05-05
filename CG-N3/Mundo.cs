/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Gizmo
#define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
  class Mundo : GameWindow
  {
    private static Mundo instanciaMundo = null;

    private Mundo(int width, int height) : base(width, height) { }

    public static Mundo GetInstance(int width, int height)
    {
      if (instanciaMundo == null)
        instanciaMundo = new Mundo(width, height);
      return instanciaMundo;
    }

    private CameraOrtho camera = new CameraOrtho();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private ObjetoGeometria objetoSelecionado = null;
    private bool bBoxDesenhar = true;
    int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
    private Poligono objetoNovo = null;
    private String objetoId = "A";
#if CG_Privado
    private Retangulo obj_Retangulo;
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      camera.xmin = 0; camera.xmax = 600; camera.ymin = 0; camera.ymax = 600;

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");


#if CG_Privado
      obj_Retangulo = new Retangulo(objetoId + 1, null, new Ponto4D(50, 50, 0), new Ponto4D(150, 150, 0));
      objetosLista.Add(obj_Retangulo);
      obj_Retangulo.PrimitivaCor = OpenTK.Color.Violet;
      objetoSelecionado = obj_Retangulo;

      obj_SegReta = new Privado_SegReta(objetoId + 1, null, new Ponto4D(50, 150), new Ponto4D(150, 250));
      objetosLista.Add(obj_SegReta);
      obj_SegReta.PrimitivaCor = OpenTK.Color.Tomato;
      objetoSelecionado = obj_SegReta;

      obj_Circulo = new Privado_Circulo(objetoId + 1, null, new Ponto4D(100, 300), 50);
      objetosLista.Add(obj_Circulo);
      obj_Circulo.PrimitivaCor = OpenTK.Color.Tan;
      objetoSelecionado = obj_Circulo;

      objetoNovo = new Poligono(objetoId + 1, null);
      objetosLista.Add(objetoNovo);
      objetoNovo.PontosAdicionar(new Ponto4D(50, 350));
      objetoNovo.PontosAdicionar(new Ponto4D(150, 350));  // N3-Exe6: "troque" para deixar o rastro
      objetoNovo.PontosAdicionar(new Ponto4D(100, 450));
      objetoSelecionado = objetoNovo;
      objetoNovo = null;
#endif
      GL.ClearColor(OpenTK.Color.Gray);
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);
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
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.H)
        Utilitario.AjudaTeclado();
      else if (e.Key == Key.Escape)
        Exit();
      else if (e.Key == Key.E)
      {
        Console.WriteLine("--- Objetos / Pontos: ");
        for (var i = 0; i < objetosLista.Count; i++)
        {
          Console.WriteLine(objetosLista[i]);
        }
      }
      else if (e.Key == Key.O)
        bBoxDesenhar = !bBoxDesenhar;
      else if (e.Key == Key.Enter)
      {
        if (objetoNovo != null)
        {
          objetoNovo.PontosRemoverUltimo();   // N3-Exe6: "truque" para deixar o rastro
          objetoSelecionado = objetoNovo;
          objetoNovo = null;
        }
      }
      else if (e.Key == Key.Space)
      {
        if (objetoNovo == null)
        {
          objetoNovo = new Poligono(objetoId + 1, null);
          objetosLista.Add(objetoNovo);
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));  // N3-Exe6: "troque" para deixar o rastro
        }
        else
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));
      }
      else if (objetoSelecionado != null)
      {
        if (e.Key == Key.M)
          Console.WriteLine(objetoSelecionado.Matriz);
        else if (e.Key == Key.P)
          Console.WriteLine(objetoSelecionado);
        else if (e.Key == Key.I)
          objetoSelecionado.AtribuirIdentidade();
        //TODO: não está atualizando a BBox com as transformações geométricas
        else if (e.Key == Key.Left)
          objetoSelecionado.TranslacaoXY(-10, 0);
        else if (e.Key == Key.Right)
          objetoSelecionado.TranslacaoXY(10, 0);
        else if (e.Key == Key.Up)
          objetoSelecionado.TranslacaoXY(0, 10);
        else if (e.Key == Key.Down)
          objetoSelecionado.TranslacaoXY(0, -10);
        else if (e.Key == Key.PageUp)
          objetoSelecionado.EscalaXY(2, 2);
        else if (e.Key == Key.PageDown)
          objetoSelecionado.EscalaXY(0.5, 0.5);
        else if (e.Key == Key.Home)
          objetoSelecionado.EscalaXYBBox(0.5);
        else if (e.Key == Key.End)
          objetoSelecionado.EscalaXYBBox(2);
        else if (e.Key == Key.Number1)
          objetoSelecionado.RotacaoZ(10);
        else if (e.Key == Key.Number2)
          objetoSelecionado.RotacaoZ(-10);
        else if (e.Key == Key.Number3)
          objetoSelecionado.RotacaoZBBox(10);
        else if (e.Key == Key.Number4)
          objetoSelecionado.RotacaoZBBox(-10);
        else if (e.Key == Key.Number9)
          objetoSelecionado = null;                     // desmacar objeto selecionado
        else
          Console.WriteLine(" __ Tecla não implementada.");
      }
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    //TODO: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 600 - e.Position.Y; // Inverti eixo Y
      if (objetoNovo != null)
      {
        objetoNovo.PontosUltimo().X = mouseX;
        objetoNovo.PontosUltimo().Y = mouseY;
      }
    }

#if CG_Gizmo
    private void Sru3D()
    {
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(OpenTK.Color.Red);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(OpenTK.Color.Green);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      GL.Color3(OpenTK.Color.Blue);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }
#endif    
  }
  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG-N3";
      window.Run(1.0 / 60.0);
    }
  }
}
