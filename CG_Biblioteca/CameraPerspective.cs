/**
  Autor: Dalton Solano dos Reis
**/

using System;
using OpenTK;

namespace CG_Biblioteca
{
  /// <summary>
  /// Classe para controlar a câmera sintética.
  /// </summary>
  public class CameraPerspective
  {
    private float fovy, aspect, near, far;
    private Vector3 eye, at, up;


    public CameraPerspective(float fovy = (float)Math.PI / 4, float aspect = 1.0f, float near = 1.0f, float far = 50.0f)
    {
      this.fovy = fovy;
      this.aspect = aspect;
      this.near = near;
      this.far = far;

      eye = Vector3.Zero;
      eye.Z = 15;
      at = Vector3.Zero;
      up = Vector3.UnitY;   // 0, 1, 0
    }

    public float Fovy { get => fovy; set => fovy = value; }
    public float Aspect { get => aspect; set => aspect = value; }
    public float Near { get => near; set => near = value; }
    public float Far { get => far; set => far = value; }
    public Vector3 Eye { get => eye; set => eye = value; }
    public Vector3 At { get => at; set => at = value; }
    public Vector3 Up { get => up; }

    //TODO: melhorar para exibir não só a lsita de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
    public override string ToString()
    {
      string retorno;
      retorno = "__ CameraPerspective: " + "\n";
      retorno += "fovy: " + fovy + "\n";
      retorno += "aspect: " + aspect + "\n";
      retorno += "near: " + near + "\n";
      retorno += "far: " + far + "\n";
      retorno += "eye [" + eye.X + "," + eye.Y + "," + eye.Z + "]" + "\n";
      retorno += "at [" + at.X + "," + at.Y + "," + at.Z + "]" + "\n";
      retorno += "up [" + up.X + "," + up.Y + "," + up.Z + "]" + "\n";
      return (retorno);
    }

  }
}