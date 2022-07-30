using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace back_socio.Pages.Socios
{
    public class Crear_socioModel : PageModel
    {
        public InformacionSocio socioInfo = new InformacionSocio();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            socioInfo.nombre_socio=Request.Form["nombre_socio"];
            socioInfo.apellido_socio=Request.Form["apellido_socio"];
            socioInfo.correo_socio = Request.Form["correo_socio"];
            socioInfo.celular = Request.Form["celular"];
            socioInfo.direccion = Request.Form["direccion"];

            if (socioInfo.nombre_socio.Length == 0 || socioInfo.apellido_socio.Length == 0 ||
                socioInfo.correo_socio.Length == 0 || socioInfo.celular.Length == 0 ||
                socioInfo.direccion.Length == 0)
            {
                errorMessage = "Requiere llenar todos los campos";
                return;
            }

            //Guardamos el nuevo socio en Base de Datos, autor Flavio Gonzáles
            try
            {
                String connectionString = "Data Source=DESKTOP-LFH832C;Initial Catalog=ALTOS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO socio " +
                                 "(nombre_socio, apellido_socio, correo_socio, celular, direccion) VALUES " +
                                 "(@nombre_socio, @apellido_socio, @correo_socio, @celular, @direccion);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre_socio", socioInfo.nombre_socio);
                        command.Parameters.AddWithValue("@apellido_socio", socioInfo.apellido_socio);
                        command.Parameters.AddWithValue("@correo_socio", socioInfo.correo_socio);
                        command.Parameters.AddWithValue("@celular", socioInfo.celular);
                        command.Parameters.AddWithValue("@direccion", socioInfo.direccion);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            socioInfo.nombre_socio = ""; socioInfo.apellido_socio = ""; socioInfo.correo_socio = ""; socioInfo.celular = ""; socioInfo.direccion = "";
            successMessage = "Se agregro el socio correctamente";

            Response.Redirect("/Socios/Index");
        }
    }
}
