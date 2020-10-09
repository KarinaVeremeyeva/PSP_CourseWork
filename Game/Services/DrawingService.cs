using OpenTK.Graphics.OpenGL;
using OpenTK;
using Game.Models;
using Game.Models.Base;

namespace Game.Services
{
    /// <summary>
    /// класс сервиса отриска
    /// </summary>
    public class DrawingService
    {
        /// <summary>
        /// рисование игрового объекта
        /// </summary>
        /// <param name="gameObject"></param>
        public void DrawObject(GameObject gameObject)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            var modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.LoadMatrix(ref modelview);

            const int i = 1;

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(gameObject.Color.X, gameObject.Color.Y, gameObject.Color.Z, gameObject.Color.W);
            GL.Vertex3(gameObject.Position.X, gameObject.Position.Y, 3.0f);
            GL.Vertex3(gameObject.Position.X, -gameObject.Size / i + gameObject.Position.Y, 3.0f);
            GL.Vertex3(-gameObject.Size / i + gameObject.Position.X, -gameObject.Size / i + gameObject.Position.Y, 3.0f);
            GL.Vertex3(-gameObject.Size / i + gameObject.Position.X, gameObject.Position.Y, 3.0f);

            GL.End();
        }
    }
}
