using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace back_socio.Pages.Socios
{
    public class IndexModel : PageModel
    {
        public List<InformacionSocio> listaSocios = new List<InformacionSocio>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-LFH832C;Initial Catalog=ALTOS;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "exec Listar_socio";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InformacionSocio infosocio = new InformacionSocio();
                                infosocio.id_socio = "" + reader.GetInt32(0);
                                infosocio.nombre_socio = reader.GetString(1);
                                infosocio.apellido_socio = reader.GetString(2);
                                infosocio.correo_socio = reader.GetString(3);
                                infosocio.celular = reader.GetString(4);
                                infosocio.direccion = reader.GetString(5);
                                infosocio.creacion_socio =reader.GetDateTime(6).ToString();

                                listaSocios.Add(infosocio);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }


    public class InformacionSocio
    {
        public String id_socio;
        public String nombre_socio;
        public String apellido_socio;
        public String correo_socio;
        public String celular;
        public String direccion;
        public String creacion_socio;
    }
}
