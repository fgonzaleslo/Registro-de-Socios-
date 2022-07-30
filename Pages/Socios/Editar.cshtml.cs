using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace back_socio.Pages.Socios
{
    public class EditarModel : PageModel
    {
        public InformacionSocio socioInfo =  new InformacionSocio();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id_socio =  Request.Query["id_socio"];
            try
            {
                String connectionString = "Data Source=DESKTOP-LFH832C;Initial Catalog=ALTOS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * from socio WHERE id_socio=@id_socio";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id_socio", id_socio);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                socioInfo.id_socio = "" + reader.GetInt32(0);
                                socioInfo.nombre_socio = reader.GetString(1);
                                socioInfo.apellido_socio = reader.GetString(2);
                                socioInfo.correo_socio = reader.GetString(3);
                                socioInfo.celular = reader.GetString(4);
                                socioInfo.direccion = reader.GetString(5);
                            }
                        }
                    }    
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;  
            }
        }

        public void OnPost()
        {
            socioInfo.id_socio = Request.Form["id_socio"];
            socioInfo.nombre_socio = Request.Form["nombre_socio"];
            socioInfo.apellido_socio = Request.Form["apellido_socio"];
            socioInfo.correo_socio = Request.Form["correo_socio"];
            socioInfo.celular = Request.Form["celular"];
            socioInfo.direccion = Request.Form["direccion"];


            if (socioInfo.id_socio.Length == 0 || socioInfo.nombre_socio.Length == 0 || socioInfo.apellido_socio.Length == 0 ||
                socioInfo.correo_socio.Length == 0 || socioInfo.celular.Length == 0 ||
                socioInfo.direccion.Length == 0)
            {
                errorMessage = "Requiere llenar todos los campos";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-LFH832C;Initial Catalog=ALTOS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE socio " +
                                 "SET nombre_socio=@nombre_socio, apellido_socio=@apellido_socio, correo_socio=@correo_socio, celular=@celular, direccion=@direccion " +
                                 "WHERE id_socio=@id_socio";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre_socio", socioInfo.nombre_socio);
                        command.Parameters.AddWithValue("@apellido_socio", socioInfo.apellido_socio);
                        command.Parameters.AddWithValue("@correo_socio", socioInfo.correo_socio);
                        command.Parameters.AddWithValue("@celular", socioInfo.celular);
                        command.Parameters.AddWithValue("@direccion", socioInfo.direccion);
                        command.Parameters.AddWithValue("@id_socio", socioInfo.id_socio);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Socios/Index");

        }
    }
}
