using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetByName(string UserNombre)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezCineContext context = new DL.JsanchezCineContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{UserNombre}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.UserName = obj.UserName;
                            usuario.Password = obj.Password;

                            result.Object = usuario;
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}