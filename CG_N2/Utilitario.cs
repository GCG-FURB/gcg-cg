/**
  Autor: Dalton Solano dos Reis
**/

using System;

namespace gcgcg
{
  public abstract class Utilitario
  {
    public static char charProximo(char atual) {
      return Convert.ToChar(atual + 1);
    }
    public static void AjudaTeclado()
    {
      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra está ajuda. ");
      Console.WriteLine(" [Escape  ] sair. ");
      Console.WriteLine(" [  E     ] N3-Exe04: listas polígonos e vértices. ");
      Console.WriteLine(" [  O     ] N3-Exe08: exibe bBox do polígono selecionado. ");
      Console.WriteLine(" [Enter   ] N3-Exe09: termina adição e mover de pontos, desseleciona polígono. ");
      Console.WriteLine(" [Espaço  ] N3-Exe06: adiciona vértice ao polígono. ");
      Console.WriteLine(" [  A     ] N3-Exe09: seleção do Polígono. ");
      Console.WriteLine(" [  M     ]         : exibe matriz de transformação do polígono selecionado. ");
      Console.WriteLine(" [  P     ]         : exibe os vértices do polígono selecionado. ");
      Console.WriteLine(" [  I     ]         : aplica a matriz Identidade no polígono selecionado. ");
      Console.WriteLine(" [Left    ] N3-Exe10: move o polígono selecionado para eixo X positivo. ");
      Console.WriteLine(" [Right   ] N3-Exe10: move o polígono selecionado para eixo X negativo. ");
      Console.WriteLine(" [Up      ] N3-Exe10: move o polígono selecionado para eixo Y positivo. ");
      Console.WriteLine(" [Down    ] N3-Exe10: move o polígono selecionado para eixo Y negativo. ");
      // [TODO:] poderia usar a escolha do eixo X,Y,Z para além de saber qual eixo da rotação também qual eixo faz a translação. Assim as seta esq/dir move o objeto de acordo com o eixo, e as setas para cima e para baixo aumentam e diminuiem o passo da translação
      Console.WriteLine(" [PageUp  ]         : reduz o polígono selecionado em relação a origem. ");
      Console.WriteLine(" [PageDown]         : amplia o polígono selecionado em relação a origem. ");
      Console.WriteLine(" [Home    ] N3-Exe11: reduz o polígono selecionado em relação ao centro da bBox. ");
      Console.WriteLine(" [End     ] N3-Exe11: amplia o polígono selecionado em relação ao centro da bBox. ");
      Console.WriteLine(" [  1     ]         : rotação anti-horária do polígono selecionado em relação a origem. ");
      Console.WriteLine(" [  2     ]         : rotação horária do polígono selecionado em relação a origem. ");
      Console.WriteLine(" [  3     ] N3-Exe12: rotação anti-horária do polígono selecionado em relação ao centro da bBox. ");
      Console.WriteLine(" [  4     ] N3-Exe12: rotação horária do polígono selecionado em relação ao centro da bBox. ");
      Console.WriteLine(" [  R     ] N3-Exe08: atribui a cor vermelha ao polígono selecionado. ");
      Console.WriteLine(" [  G     ] N3-Exe08: atribui a cor verde ao polígono selecionado. ");
      Console.WriteLine(" [  B     ] N3-Exe08: atribui a cor azul ao polígono selecionado. ");
      Console.WriteLine(" [  S     ] N3-Exe07: alterna entre aberto e fechado o polígono selecionado. ");
      Console.WriteLine(" [  D     ] N3-Exe05: remove o vértice do polígono selecionado que estiver mais perto do mouse. ");
      Console.WriteLine(" [  V     ] N3-Exe05: move o vértice do polígono selecionado que estiver mais perto do mouse. ");
      Console.WriteLine(" [  C     ] N3-Exe04: remove o polígono selecionado. ");
      Console.WriteLine(" [  X     ]         : rotação entorno do eixo X. ");
      Console.WriteLine(" [  Y     ]         : rotação entorno do eixo Y. ");
      Console.WriteLine(" [  Z     ]         : rotação entorno do eixo Z. ");
      Console.WriteLine("  --- ");
      Console.WriteLine(" Se tiver objeto selecionado adiciona novo objeto como filho dele. ");
      Console.WriteLine(" Senão tiver objeto selecionado adiciona novo objeto no mundo. ");
    }

  }
}