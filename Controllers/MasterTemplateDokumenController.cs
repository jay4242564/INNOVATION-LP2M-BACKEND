using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sistem_informasi_lembaga_pengabdian_masyarakat_backend.Helper;
using System.Data;
using System.Data.SqlClient;

namespace sistem_informasi_lembaga_pengabdian_masyarakat_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterTemplateDokumenController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataTemplateDokumen([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getDataTemplateDokumen", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataTemplateDokumenById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getTemplateDokumenByID", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }


        //[Authorize]
        [HttpPost]
        public IActionResult CreateTemplateDokumen([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createTemplateDokumen", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult EditTemplateDokumen([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_editTemplateDokumen", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DeleteTemplateDokumen([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_deleteTemplateDokumen", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DownloadFile([FromBody] DownloadRequest request)
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

        public class DownloadRequest
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
                using (SqlCommand cmd = new SqlCommand("lpm_getdownloadTemplateDokumen", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p1", id.ToString());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fileName = reader["tem_upload_file"].ToString();
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
