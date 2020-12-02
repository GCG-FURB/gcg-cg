using System;
using OpenTK;
using OpenTK.Input;

namespace CG_Biblioteca
{
  public class CameraPerspective
  {
    private float fovy, aspect, near, far;
    private Vector3 eye, at, up;
    private enum menuCameraEnum { eye, at, near, far, fovy }
    private menuCameraEnum menuCameraOpcao;

    public CameraPerspective(float fovy = (float)Math.PI / 4, float aspect = 1.0f, float near = 1.0f, float far = 50.0f)
    {
      this.fovy = fovy;
      this.aspect = aspect;
      this.near = near;
      this.far = far;

      eye = Vector3.Zero; eye.Z = 15;   // ( 0, 0,15)
      at = Vector3.Zero;                // ( 0, 0, 0)
      up = Vector3.UnitY;               // ( 0, 1, 0)

      menuCameraOpcao = menuCameraEnum.eye;
    }

    public float Fovy { get => fovy; set => fovy = value; }
    public float Aspect { get => aspect; set => aspect = value; }
    public float Near { get => near; set => near = value; }
    public float Far { get => far; set => far = value; }
    public Vector3 Eye { get => eye; set => eye = value; }
    public Vector3 At { get => at; set => at = value; }
    public Vector3 Up { get => up; }

    private void FovyDes(float deslocamento) { fovy += deslocamento; }
    private void NearDes(float deslocamento) { near += deslocamento; }
    private void FarDes(float deslocamento) { far += deslocamento; }

    private void EyeDes(float deslocamento, char eixo)
    {
      switch (eixo)
      {
        case 'x': eye.X += deslocamento; break;
        case 'y': eye.Y += deslocamento; break;
        case 'z': eye.Z += deslocamento; break;
      }
    }

    private void AtDes(float deslocamento, char eixo)
    {
      switch (eixo)
      {
        case 'x': at.X += deslocamento; break;
        case 'y': at.Y += deslocamento; break;
        case 'z': at.Z += deslocamento; break;
      }
    }

    public void MenuTecla(OpenTK.Input.Key tecla, char eixo, float deslocamento)
    {
      if (tecla == Key.P) Console.WriteLine(this);
      else if (tecla == Key.R)
      {
        fovy = (float)Math.PI / 4;   //TODO: existe algo do tipo this(); em java
        aspect = 1.0f;
        near = 1.0f;
        far = 50.0f;
        eye = Vector3.Zero; eye.Z = 15;   // ( 0, 0,15)
        at = Vector3.Zero;                // ( 0, 0, 0)
        up = Vector3.UnitY;               // ( 0, 1, 0)
      }
      else if (tecla == Key.Up) menuCameraOpcao++;
      else if (tecla == Key.Down) menuCameraOpcao--; //TODO: qdo chega indice 0 não vai para o final
      else if (tecla == Key.Left) deslocamento = -deslocamento;

      if (!Enum.IsDefined(typeof(menuCameraEnum), menuCameraOpcao))
        menuCameraOpcao = menuCameraEnum.eye;

      Console.WriteLine("__ Câmera (" + menuCameraOpcao + "," + eixo + "," + deslocamento + ")");
      if ((tecla == Key.Left) || (tecla == Key.Right))
      {
        switch (menuCameraOpcao)
        {
          case menuCameraEnum.eye:
            EyeDes(deslocamento, eixo);
            break;
          case menuCameraEnum.at:
            AtDes(deslocamento, eixo);
            break;
          case menuCameraEnum.near:   //TODO: tem de forçar um OnResize
            NearDes(deslocamento);
            break;
          case menuCameraEnum.far:    //TODO: tem de forçar um OnResize
            FarDes(deslocamento);
            break;
          case menuCameraEnum.fovy:   //TODO: tem de forçar um OnResize
            FovyDes(deslocamento);
            break;
        }
      }
    }

    public override string ToString()
    {
      string retorno;
      retorno = "__ CameraPerspective: " + "\n";
      retorno += "eye [" + eye.X + "," + eye.Y + "," + eye.Z + "]" + "\n";
      retorno += "at [" + at.X + "," + at.Y + "," + at.Z + "]" + "\n";
      retorno += "up [" + up.X + "," + up.Y + "," + up.Z + "]" + "\n";
      retorno += "near: " + near + "\n";
      retorno += "far: " + far + "\n";
      retorno += "fovy: " + fovy + "\n";
      retorno += "aspect: " + aspect + "\n";
      return (retorno);
    }

  }
}