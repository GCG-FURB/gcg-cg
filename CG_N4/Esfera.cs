/**
  Autor: Dalton Solano dos Reis
**/

/// <summary>
/// fonte: https://stackoverflow.com/questions/7687148/drawing-sphere-in-opengl-without-using-glusphere
/// </summary>
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal class Esfera : ObjetoGeometria
  {
    //TODO: gerar os vetores normais, tem como fazer no link deste exemplo
    private bool exibeVetorNormal = false;
    //TODO: não precisava ter parte negativa, ter um tipo inteiro grande
    protected List<int> listaTopologia = new List<int>();

    public Esfera(char rotulo, Objeto paiRef) : base(rotulo, paiRef)
    {
      // int segments = 10; // Números mais altos melhoram a qualidade 
      // int radius = 1;    // O raio (largura) do cilindro
      // int height = 10;   // A altura do cilindro

      // for (double y = 0; y < 2; y++)
      // {
      //   for (double x = 0; x < segments; x++)
      //   {
      //     double theta = (x / (segments - 1)) * 2 * Math.PI;
      //     base.PontosAdicionar(new Ponto4D(
      //         (float)(radius * Math.Cos(theta)),
      //         (float)(height * y),
      //         (float)(radius * Math.Sin(theta))));
      //   }
      // }
      // // ponto do centro da base
      // base.PontosAdicionar(new Ponto4D(0, 0, 0));
      // // ponto do centro da topo
      // base.PontosAdicionar(new Ponto4D(0, height, 0));

      // //TODO: parce que alguams faces estão com a orientação errada.
      // for (int x = 0; x < segments - 1; x++)
      // {
      //   // base
      //   listaTopologia.Add(x);
      //   listaTopologia.Add(x + 1);
      //   listaTopologia.Add(segments - 1);
      //   // topo
      //   listaTopologia.Add(x);
      //   listaTopologia.Add(x + 1);
      //   listaTopologia.Add(segments);
      // }

    }

    protected override void DesenharObjeto()
    {
      // GL.Begin(PrimitiveType.Triangles);
      // foreach (int index in listaTopologia)
      //   GL.Vertex3(base.pontosLista[index].X, base.pontosLista[index].Y, base.pontosLista[index].Z);
      // GL.End();

      drawSphere(1,10,10);

    }

    void drawSphere(double r, int lats, int longs)
    {
      int i, j;
      for (i = 0; i <= lats; i++)
      {
        double lat0 = Math.PI * (-0.5 + (double)(i - 1) / lats);
        double z0 = Math.Sin(lat0);
        double zr0 = Math.Cos(lat0);

        double lat1 = Math.PI * (-0.5 + (double)i / lats);
        double z1 = Math.Sin(lat1);
        double zr1 = Math.Cos(lat1);

        GL.Begin(PrimitiveType.Quads);
        for (j = 0; j <= longs; j++)
        {
          double lng = 2 * Math.PI * (double)(j - 1) / longs;
          double x = Math.Cos(lng);
          double y = Math.Sin(lng);

          GL.Normal3(x * zr0, y * zr0, z0);
          GL.Vertex3(r * x * zr0, r * y * zr0, r * z0);
          GL.Normal3(x * zr1, y * zr1, z1);
          GL.Vertex3(r * x * zr1, r * y * zr1, r * z1);
        }
        GL.End();
      }
    }
//TODO: melhorar para exibir não só a lsita de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Esfera: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }

  }
}