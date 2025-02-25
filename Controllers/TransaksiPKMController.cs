using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using sistem_informasi_lembaga_pengabdian_masyarakat_backend.Helper;
using System.Data.SqlClient;
using System.Data;

namespace sistem_informasi_lembaga_pengabdian_masyarakat_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransaksiPKMController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataPKM([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getDataPKM", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(dt.ToString());
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataPKMByIdTransaksi([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getDataPKMByIdTransaksi", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(dt.ToString());
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataPKMByUsername([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getDataPKMByUsername", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(dt.ToString());
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataMahasiswaProdiByNim([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getListMahasiswaByProdi", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataJabatanTerstrukturByKaryawanId([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getLisJabatanTerstrukturByKaryawanId", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }


        [HttpPost]
        public IActionResult GetDataJabatanTerstrukturByKaryawanUsername([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getLisJabatanTerstrukturByKaryawanUsername", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult CreateDataPKM([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createTrPKM", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult UpdateDataPKM([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_editDataPKM", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult UpdateDataLaporanAkhirPKM([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_updateLaporanAkhirPKM", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest();
            }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult CreatePkmMahasiswaDetail([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createPkmMahasiswaDetail", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CreatePkmWaktuPelaksanaanDetail([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createPkmWaktuPelaksanaanDetail", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CreatePengajuanProposalMahasiswaDetailPKM([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createProposalMahasiswaDetailPKM", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult CreatePkmKaryawanDetail([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createPkmKaryawanDetail", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetPkmKaryawanDetail([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getPkmKaryawanDetail", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetPkmWaktuPelaksanaanDetail([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getPkmWaktuPelaksanaanDetail", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetPkmMahasiswaDetail([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getPkmMahasiswaDetail", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CreatePengajuanProposalKaryawanDetailPKM([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createProposalKaryawanDetailPKM", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult SentTrPKM([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_sentTrPKM", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DownloadFile([FromBody] DownloadRequestPKM request)
        {
            if (request == null || request.Id <= 0 || string.IsNullOrEmpty(request.JenisDokumen))
                return BadRequest("ID file atau jenis dokumen tidak valid.");

            string fileName = GetFileNameFromDatabase(request.Id, request.JenisDokumen);

            if (string.IsNullOrEmpty(fileName))
                return NotFound("File tidak ditemukan di database.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File tidak ditemukan di server.");

            var contentType = GetContentType(filePath);
            return PhysicalFile(filePath, contentType, fileName);
        }

        private string GetFileNameFromDatabase(int id, string jenisDokumen)
        {
            string fileName = null;
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("lpm_getDokumenPKM", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p1", id);
                    cmd.Parameters.AddWithValue("@p2", jenisDokumen);

                    for (int i = 3; i <= 50; i++)
                    {
                        cmd.Parameters.AddWithValue($"@p{i}", DBNull.Value);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fileName = reader.GetValue(0)?.ToString();
                        }
                    }
                }
            }

            return fileName;
        }


        public class DownloadRequestPKM
        {
            public int Id { get; set; }
            public string JenisDokumen { get; set; }
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

        [HttpPost]
        public IActionResult DeleteDataPKM([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_DeleteDataPKM", EncodeData.HtmlEncodeObject(value));
                Console.WriteLine(dt.ToString());
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
    }
}
