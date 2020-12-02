using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal abstract class Objeto
  {
    protected char rotulo;
    private Cor objetoCor = new Cor(255, 255, 255, 255);
    public Cor ObjetoCor { get => objetoCor; set => objetoCor = value; }
    private PrimitiveType primitivaTipo = PrimitiveType.LineLoop;
    public PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    private float primitivaTamanho = 1;
    public float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }
    private BBox bBox = new BBox();
    public BBox BBox { get => bBox; set => bBox = value; }
    private List<Objeto> objetosLista = new List<Objeto>();

    private Transformacao4D matriz = new Transformacao4D();
    public Transformacao4D Matriz { get => matriz; }

    /// Matrizes temporarias que sempre sao inicializadas com matriz Identidade entao podem ser "static".
    private static Transformacao4D matrizTmpTranslacao = new Transformacao4D();
    private static Transformacao4D matrizTmpTranslacaoInversa = new Transformacao4D();
    private static Transformacao4D matrizTmpEscala = new Transformacao4D();
    private static Transformacao4D matrizTmpRotacao = new Transformacao4D();
    private static Transformacao4D matrizGlobal = new Transformacao4D();

    private enum menuObjetoEnum { Translacao, Escala, Rotacao }
    private menuObjetoEnum menuObjetoOpcao;

    public Objeto(char rotulo, Objeto paiRef)
    {
      this.rotulo = rotulo;
    }

    public void Desenhar()
    {
      GL.PushMatrix();                                    // N3-Exe14: grafo de cena
      GL.MultMatrix(matriz.ObterDados());
      GL.Color3(objetoCor.CorR, objetoCor.CorG, objetoCor.CorB);
      GL.LineWidth(primitivaTamanho);
      GL.PointSize(primitivaTamanho);
      DesenharGeometria();
      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar();
      }
      GL.PopMatrix();                                     // N3-Exe14: grafo de cena
    }
    protected abstract void DesenharGeometria();
    public void FilhoAdicionar(Objeto filho)
    {
      this.objetosLista.Add(filho);
    }
    public void FilhoRemover(Objeto filho)
    {
      this.objetosLista.Remove(filho);
    }
    public void AtribuirIdentidade()
    {
      matriz.AtribuirIdentidade();
    }
    public void TranslacaoEixo(double deslocamento, char eixoTranslacao)
    {
      switch (eixoTranslacao)
      {
        case 'x':
          matrizTmpTranslacao.AtribuirTranslacao(deslocamento, 0, 0);
          break;
        case 'y':
          matrizTmpTranslacao.AtribuirTranslacao(0, deslocamento, 0);
          break;
        case 'z':
          matrizTmpTranslacao.AtribuirTranslacao(0, 0, deslocamento);
          break;
      }
    }
    public void Translacao(double deslocamento, char eixoTranslacao)
    {
      TranslacaoEixo(deslocamento, eixoTranslacao);
      matriz = matrizTmpTranslacao.MultiplicarMatriz(matriz);
    }
    public void Escala(double fator)
    {
      Transformacao4D matrizScale = new Transformacao4D();
      matrizScale.AtribuirEscala(fator, fator, fator);
      matriz = matrizScale.MultiplicarMatriz(matriz);
    }

    public void EscalaXYZBBox(double Sx, double Sy, double Sz)
    {
      matrizGlobal.AtribuirIdentidade();
      Ponto4D pontoPivo = bBox.obterCentro;

      matrizTmpTranslacao.AtribuirTranslacao(-pontoPivo.X, -pontoPivo.Y, -pontoPivo.Z); // Inverter sinal
      matrizGlobal = matrizTmpTranslacao.MultiplicarMatriz(matrizGlobal);

      matrizTmpEscala.AtribuirEscala(Sx, Sy, Sz);
      matrizGlobal = matrizTmpEscala.MultiplicarMatriz(matrizGlobal);

      matrizTmpTranslacaoInversa.AtribuirTranslacao(pontoPivo.X, pontoPivo.Y, pontoPivo.Z);
      matrizGlobal = matrizTmpTranslacaoInversa.MultiplicarMatriz(matrizGlobal);

      matriz = matriz.MultiplicarMatriz(matrizGlobal);
    }
    public void RotacaoEixo(double angulo, char eixoRotacao)
    {
      switch (eixoRotacao)
      {
        case 'x':
          matrizTmpRotacao.AtribuirRotacaoX(Transformacao4D.DEG_TO_RAD * angulo);
          break;
        case 'y':
          matrizTmpRotacao.AtribuirRotacaoY(Transformacao4D.DEG_TO_RAD * angulo);
          break;
        case 'z':
          matrizTmpRotacao.AtribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * angulo);
          break;
      }
    }
    public void Rotacao(double angulo, char eixoRotacao, bool bBoxPivo = false)
    {
      matrizGlobal.AtribuirIdentidade();
      Ponto4D pontoPivo = bBox.obterCentro;

      if (bBoxPivo)
      {
        matrizTmpTranslacao.AtribuirTranslacao(-pontoPivo.X, -pontoPivo.Y, -pontoPivo.Z); // Inverter sinal
        matrizGlobal = matrizTmpTranslacao.MultiplicarMatriz(matrizGlobal);
      }

      RotacaoEixo(angulo, eixoRotacao);
      matrizGlobal = matrizTmpRotacao.MultiplicarMatriz(matrizGlobal);

      if (bBoxPivo)
      {
        matrizTmpTranslacao.AtribuirTranslacao(-pontoPivo.X, -pontoPivo.Y, -pontoPivo.Z); // Inverter sinal
        matrizGlobal = matrizTmpTranslacao.MultiplicarMatriz(matrizGlobal);
      }

      matriz = matriz.MultiplicarMatriz(matrizGlobal);
    }

    public void MenuTecla(OpenTK.Input.Key tecla, char eixo, float deslocamento, bool bBox)
    {
      if (tecla == Key.P) {
        Console.WriteLine(this);
        if (bBox) {
          Console.Write(BBox);
        }
      }
      else if (tecla == Key.M) Console.WriteLine(this.Matriz);
      else if (tecla == Key.R)
      {
        this.AtribuirIdentidade();
      }
      else if (tecla == Key.Up) menuObjetoOpcao++;
      else if (tecla == Key.Down) menuObjetoOpcao--; //TODO: qdo chega indice 0 não vai para o final

      if (!Enum.IsDefined(typeof(menuObjetoEnum), menuObjetoOpcao))
        menuObjetoOpcao = menuObjetoEnum.Translacao;

      Console.WriteLine("__ Objeto (" + menuObjetoOpcao + "," + eixo + "," + deslocamento + ")");
      if ((tecla == Key.Left) || (tecla == Key.Right))
      {
        switch (menuObjetoOpcao)
        {
          case menuObjetoEnum.Translacao:
            if (tecla == Key.Left)
              deslocamento = -deslocamento;
            this.Translacao(deslocamento, eixo);
            break;
          case menuObjetoEnum.Escala:
            if (deslocamento > 1)
            {
              if (tecla == Key.Left)
                deslocamento = 1 / deslocamento;
              this.Escala(deslocamento);
            }
            break;
          case menuObjetoEnum.Rotacao:
            if (tecla == Key.Left)
              deslocamento = -deslocamento;
            this.Rotacao(deslocamento, eixo, bBox); //TODO: deslocamento (float) .. angulo (double)
            break;
        }
      }
    }

  }
}