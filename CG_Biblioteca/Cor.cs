/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
namespace CG_Biblioteca
{
  public class Cor
  {
    private byte corR, corG, corB, corA;

    public Cor(byte corR = 255, byte corG = 255, byte corB = 255, byte corA = 255)
    {
      this.corR = corR; this.corG = corG; this.corB = corB; this.corA = corA;
    }

    public override string ToString()
    {
      string retorno;
      retorno = "__ Cor: " + "\n";
      retorno += "corR: " + corR + " - corG" + corG + " - corB" + corB + " - corA" + corA + "\n";
      return (retorno);
    }

    public byte CorR { get => corR; set => corR = value; }
    public byte CorG { get => corG; set => corG = value; }
    public byte CorB { get => corB; set => corB = value; }
    public byte CorA { get => corA; set => corA = value; }

  }
}