using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
  internal class Objeto
  {
    private Ponto4D ptoA = new Ponto4D(200, 200);
    private Ponto4D ptoB = new Ponto4D(100, 100);
    private BBox bBox = new BBox();

    public Objeto()
    {
      bBox.atribuirBBox(ptoA);
      bBox.atualizarBBox(ptoB);
      bBox.processarCentroBBox();
    }

    public void Desenha()
    {
      GL.LineWidth(1);
      GL.Color3(Color.Yellow);
      GL.Begin(PrimitiveType.Lines);
      GL.Vertex2(ptoB.X, ptoB.Y);
      GL.Vertex2(ptoA.X, ptoA.Y);
      GL.End();

      bBox.desenhaBBox();

    }

    public void Move(int x, int y)
    {
      ptoA.X = x;
      ptoA.Y = -y;

    }
  }



}