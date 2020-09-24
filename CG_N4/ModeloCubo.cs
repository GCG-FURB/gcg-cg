/**
  Autor: Dalton Solano dos Reis
**/
/// <summary>
/// Poderia isolar a parte do modelo do objeto tendo sempre ModeloCubo, ModeloEsfera
/// No modelo só ter a definição de geometria e topologia
/// Os outros atributos estar em objeto 
/// 
/// Na classe Objeto só ter atributos/métodos relacionados com o grafo de cena e as transformações
/// No ObjetoMalha ter atributos de cor/iluminação/textura (aparência)
/// No ObjetoMalhaCubo só a geometria e topologia ,, pareceido com a estrutura de leitura do formato de arquivo OBJ
/// 
/// </summary>
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal class ModeloCubo
  {
    protected List<Ponto4D> pontosLista = new List<Ponto4D>();

    public void Geometria()
    {
      pontosLista.Add(new Ponto4D(-1, -1, 1)); // PtoA listaPto[0]
      pontosLista.Add(new Ponto4D(1, -1, 1)); // PtoB listaPto[1]
      pontosLista.Add(new Ponto4D(1, 1, 1)); // PtoC listaPto[2]
      pontosLista.Add(new Ponto4D(-1, 1, 1)); // PtoD listaPto[3]
      pontosLista.Add(new Ponto4D(-1, -1, -1)); // PtoE listaPto[4]
      pontosLista.Add(new Ponto4D(1, -1, -1)); // PtoF listaPto[5]
      pontosLista.Add(new Ponto4D(1, 1, -1)); // PtoG listaPto[6]
      pontosLista.Add(new Ponto4D(-1, 1, -1)); // PtoH listaPto[7]
    }

    // protected override void DesenharObjeto()
    // {       // Sentido anti-horário
    //   GL.Begin(PrimitiveType.Quads);
    //   // Face da frente
    //   GL.Color3(1.0, 0.0, 0.0);
    //   GL.Normal3(0, 0, 1);
    //   GL.Vertex3(pontosLista[0].X, pontosLista[0].Y, pontosLista[0].Z);    // PtoA
    //   GL.Vertex3(pontosLista[1].X, pontosLista[1].Y, pontosLista[1].Z);    // PtoB
    //   GL.Vertex3(pontosLista[2].X, pontosLista[2].Y, pontosLista[2].Z);    // PtoC
    //   GL.Vertex3(pontosLista[3].X, pontosLista[3].Y, pontosLista[3].Z);    // PtoD
    //                                                                                       // Face do fundo
    //   GL.Color3(0.0, 1.0, 0.0);
    //   GL.Normal3(0, 0, -1);
    //   GL.Vertex3(pontosLista[4].X, pontosLista[4].Y, pontosLista[4].Z);    // PtoE
    //   GL.Vertex3(pontosLista[7].X, pontosLista[7].Y, pontosLista[7].Z);    // PtoH
    //   GL.Vertex3(pontosLista[6].X, pontosLista[6].Y, pontosLista[6].Z);    // PtoG
    //   GL.Vertex3(pontosLista[5].X, pontosLista[5].Y, pontosLista[5].Z);    // PtoF
    //                                                                                       // Face de cima
    //   GL.Color3(0.0, 0.0, 1.0);
    //   GL.Normal3(0, 1, 0);
    //   GL.Vertex3(pontosLista[3].X, pontosLista[3].Y, pontosLista[3].Z);    // PtoD
    //   GL.Vertex3(pontosLista[2].X, pontosLista[2].Y, pontosLista[2].Z);    // PtoC
    //   GL.Vertex3(pontosLista[6].X, pontosLista[6].Y, pontosLista[6].Z);    // PtoG
    //   GL.Vertex3(pontosLista[7].X, pontosLista[7].Y, pontosLista[7].Z);    // PtoH
    //                                                                                       // Face de baixo
    //   GL.Color3(1.0, 1.0, 0.0);
    //   GL.Normal3(0, -1, 0);
    //   GL.Vertex3(pontosLista[0].X, pontosLista[0].Y, pontosLista[0].Z);    // PtoA
    //   GL.Vertex3(pontosLista[4].X, pontosLista[4].Y, pontosLista[4].Z);    // PtoE
    //   GL.Vertex3(pontosLista[5].X, pontosLista[5].Y, pontosLista[5].Z);    // PtoF
    //   GL.Vertex3(pontosLista[1].X, pontosLista[1].Y, pontosLista[1].Z);    // PtoB
    //                                                                                       // Face da direita
    //   GL.Color3(0.0, 1.0, 1.0);
    //   GL.Normal3(1, 0, 0);
    //   GL.Vertex3(pontosLista[1].X, pontosLista[1].Y, pontosLista[1].Z);    // PtoB
    //   GL.Vertex3(pontosLista[5].X, pontosLista[5].Y, pontosLista[5].Z);    // PtoF
    //   GL.Vertex3(pontosLista[6].X, pontosLista[6].Y, pontosLista[6].Z);    // PtoG
    //   GL.Vertex3(pontosLista[2].X, pontosLista[2].Y, pontosLista[2].Z);    // PtoC
    //                                                                                       // Face da esquerda
    //   GL.Color3(1.0, 0.0, 1.0);
    //   GL.Normal3(-1, 0, 0);
    //   GL.Vertex3(pontosLista[0].X, pontosLista[0].Y, pontosLista[0].Z);    // PtoA
    //   GL.Vertex3(pontosLista[3].X, pontosLista[3].Y, pontosLista[3].Z);    // PtoD
    //   GL.Vertex3(pontosLista[7].X, pontosLista[7].Y, pontosLista[7].Z);    // PtoH
    //   GL.Vertex3(pontosLista[4].X, pontosLista[4].Y, pontosLista[4].Z);    // PtoE
    //   GL.End();
    // }
    
  }
}