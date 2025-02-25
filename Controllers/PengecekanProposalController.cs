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
    public class PengecekanProposalController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataPengecekanProposal([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getDataPengecekanProposal", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult KonfirmasiPengecekanProposal([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_pengecekanAdminProposal", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult KonfirmasiPengecekanDraftProposal([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_pengecekanDraftProposal", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }


       
    }
}
