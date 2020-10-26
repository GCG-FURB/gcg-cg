/**
  Autor: Dalton Solano dos Reis
**/

using System.Collections.Generic;
using CG_Biblioteca;

//FIXME: trazer a lista de Topologia da Classe Cilindro para esta classe. pontosLista deveria ser listaGeometria. Esta classe deveria mudar de nome para representar um objeto solido, quem sabe mudar a atual classe Objeto para ObjetoTransformacao (deixar só a parte das matrizes) e esta classe ser Objeto.
namespace gcgcg
{
  internal abstract class ObjetoGeometria : Objeto
  {
    protected List<Ponto4D> pontosLista = new List<Ponto4D>();

    public ObjetoGeometria(char rotulo, Objeto paiRef) : base(rotulo, paiRef) { }

    protected override void DesenharGeometria()
    {
      DesenharObjeto();
    }
    protected abstract void DesenharObjeto();
    public void PontosAdicionar(Ponto4D pto)
    {
      pontosLista.Add(pto);
      if (pontosLista.Count.Equals(1))
        base.BBox.Atribuir(pto);
      else
        base.BBox.Atualizar(pto);
      base.BBox.ProcessarCentro();
    }

    public void PontosRemoverUltimo()
    {
      pontosLista.RemoveAt(pontosLista.Count - 1);
    }

    protected void PontosRemoverTodos()
    {
      pontosLista.Clear();
    }

    public Ponto4D PontosUltimo()
    {
      return pontosLista[pontosLista.Count - 1];
    }

    public void PontosAlterar(Ponto4D pto, int posicao)
    {
      pontosLista[posicao] = pto;
    }

    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }
  }
}