﻿// #define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
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

    private Camera camera = new Camera();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private ObjetoAramado objetoSelecionado = null;
    private bool bBoxDesenhar = false;
    private ObjetoAramado objetoNovo = null;
    private String objetoId = "A";
    int mouseX, mouseY;   //FIXME: achar método MouseDown para não ter variável Global

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
      GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();
      Sru3D();
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
      if (bBoxDesenhar)
        objetoSelecionado.BBox.Desenhar();
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {   
      // N3-Exe2: usar o arquivo docs/umlClasses.wsd
      // N3-Exe3: usar o arquivo bin/documentação.XML -> ver exemplo CG_Biblioteca/bin/documentação.XML
      if (e.Key == Key.Escape)
        Exit();
      else if (e.Key == Key.E)    // N3-Exe4: ajuda a conferir se os poligonos e vértices estão certos
      {
        for (var i = 0; i < objetosLista.Count; i++)
        {
          objetosLista[i].PontosExibirObjeto();
        }
      }
      else if (e.Key == Key.B)
        bBoxDesenhar = !bBoxDesenhar;   // N3-Exe9: exibe a BBox
      else if (e.Key == Key.M)
        objetoSelecionado.ExibeMatriz();
      else if (e.Key == Key.P)
        objetoSelecionado.PontosExibirObjeto();
      else if (e.Key == Key.I)
        objetoSelecionado.AtribuirIdentidade();
//FIXME: não está atualizando a BBox com as transformações geométricas
      else if (e.Key == Key.Left)
        objetoSelecionado.TranslacaoXY(-10, 0);     // N3-Exe10: translação
      else if (e.Key == Key.Right)
        objetoSelecionado.TranslacaoXY(10, 0);      // N3-Exe10: translação
      else if (e.Key == Key.Up)
        objetoSelecionado.TranslacaoXY(0, 10);      // N3-Exe10: translação
      else if (e.Key == Key.Down)
        objetoSelecionado.TranslacaoXY(0, -10);     // N3-Exe10: translação
      else if (e.Key == Key.PageUp)
        objetoSelecionado.EscalaXY(2, 2);
      else if (e.Key == Key.PageDown)
        objetoSelecionado.EscalaXY(0.5, 0.5);
      else if (e.Key == Key.Home)
        objetoSelecionado.EscalaXYBBox(0.5);        // N3-Exe11: escala
      else if (e.Key == Key.End)
        objetoSelecionado.EscalaXYBBox(2);          // N3-Exe11: escala
      else if (e.Key == Key.Number1)
        objetoSelecionado.RotacaoZ(10);
      else if (e.Key == Key.Number2)
        objetoSelecionado.RotacaoZ(-10);
      else if (e.Key == Key.Number3)
        objetoSelecionado.RotacaoZBBox(10);         // N3-Exe12: rotação
      else if (e.Key == Key.Number4)
        objetoSelecionado.RotacaoZBBox(-10);        // N3-Exe12: rotação
      else if (e.Key == Key.Enter)
      {
        objetoSelecionado = objetoNovo;
        objetoNovo.PontosRemoverUltimo();   // N3-Exe6: "troque" para deixar o rastro
        objetoNovo = null;
      }
      else if (e.Key == Key.Space) {
        if (objetoNovo == null)
        {
          objetoNovo = new ObjetoAramado(objetoId+1);
          // if (objetoSelecionado == null) //TODO: remover está tecla e atribuir o null qdo não selecionar um poligono
            objetosLista.Add(objetoNovo);
          // else 
          //   objetoSelecionado.FilhoAdicionar(tmpObjeto); //TODO: remover está tecla e atribuir o null qdo não selecionar um poligono
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, 600 - mouseY));
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, 600 - mouseY));  // N3-Exe6: "troque" para deixar o rastro
        }
        else
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, 600 - mouseY));
      }
      else if (e.Key == Key.Number9)      
        objetoSelecionado = null;   //TODO: remover está tecla e atribuir o null qdo não selecionar um poligono
    }

    //FIXME: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = e.Position.Y;
      if (objetoNovo != null)
      {
        objetoNovo.PontosUltimo().X = e.Position.X;             // N3-Exe5: movendo um vértice de um poligono específico
        objetoNovo.PontosUltimo().Y = 600 - e.Position.Y;
      }
    }

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
