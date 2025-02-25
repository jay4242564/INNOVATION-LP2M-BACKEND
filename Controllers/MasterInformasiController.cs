using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sistem_informasi_lembaga_pengabdian_masyarakat_backend.Helper;
using System.Data;
using System.Data.SqlClient;

namespace sistem_informasi_lembaga_pengabdian_masyarakat_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterInformasiController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataInformasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getDataInformasi", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataInformasiById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getInformasiByID", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }


        //[Authorize]
        [HttpPost]
        public IActionResult CreateInformasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createInformasi", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult EditInformasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_editInformasi", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DeleteInformasi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_deleteInformasi", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost] 
        public IActionResult DownloadFile([FromBody] DownloadRequestInformasi request)
        {
            if (request == null || request.Id <= 0)
                return BadRequest("ID file tidak valid.");

            string fileName = GetFileNameFromDatabase(request.Id);

            if (string.IsNullOrEmpty(fileName))
                return NotFound("File tidak ditemukan di database.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound("File tidak ditemukan di server.");

            var contentType = GetContentType(filePath);
            return PhysicalFile(filePath, contentType, fileName);
        }
        public class DownloadRequestInformasi
        {
            public int Id { get; set; }
        }
        private string GetFileNameFromDatabase(int id)
        {
            string fileName = null;
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("lpm_getdownloadInformasi", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p1", id.ToString());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fileName = reader["inf_dokumen_informasi"].ToString();
                        }
                    }
                }
            }
            return fileName;
        }

        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/msword" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".zip", "application/zip" },
                { ".rar", "application/x-rar-compressed" }
            };
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }
    }
}

