/**
  Autor: Dalton Solano dos Reis
**/

namespace CG_Biblioteca
{
  /// <summary>
  /// Classe para controlar a câmera sintética.
  /// </summary>
  public class CameraOrtho
  {
    private double xMin, xMax, yMin, yMax, zMin, zMax;
//                                         near, far
    /// <summary>
    /// Construtor da classe inicializando com valores padrões
    /// </summary>
    /// <param name="xMin"></param>
    /// <param name="xMax"></param>
    /// <param name="yMin"></param>
    /// <param name="yMax"></param>
    /// <param name="zMin"></param>
    /// <param name="zMax"></param>
    public CameraOrtho(double xMin = 0, double xMax = 600, double yMin = 0, double yMax = 600, double zMin = -1, double zMax = 1)
    {
      this.xMin = xMin; this.xMax = xMax;
      this.yMin = yMin; this.yMax = yMax;
      this.zMin = zMin; this.zMax = zMax;
    }
    public double xmin { get => xMin; set => xMin = value; }
    public double xmax { get => xMax; set => xMax = value; }
    public double ymin { get => yMin; set => yMin = value; }
    public double ymax { get => yMax; set => yMax = value; }
    public double zmin { get => zMin; set => zMin = value; }
    public double zmax { get => zMax; set => zMax = value; }

    public void PanEsquerda() { xMin += 2; xMax += 2; }
    public void PanDireita() { xMin -= 2; xMax -= 2; }
    public void PanCima() { yMin -= 2; yMax -= 2; }
    public void PanBaixo() { yMin += 2; yMax += 2; }
    //TODO: falta testar os limites de zoom    
    public void ZoomIn()
    {
      xMin += 2; xMax -= 2; yMin += 2; yMax -= 2;
    }
    //TODO: falta testar os limites de zoom    
    public void ZoomOut()
    {
      xMin -= 2; xMax += 2; yMin -= 2; yMax += 2;
    }

    //TODO: melhorar para exibir não só a lsita de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
    public override string ToString()
    {
      string retorno;
      retorno = "__ CameraOrtho: " + "\n";
      retorno += "xMin: " + xMin + " - xMax: " + xMax + "\n";
      retorno += "yMin: " + yMin + " - yMax: " + yMax + "\n";
      retorno += "zMin: " + zMin + " - zMax: " + zMax + "\n";
      return (retorno);
    }

  }
}