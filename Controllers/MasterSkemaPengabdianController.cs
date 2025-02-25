using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sistem_informasi_lembaga_pengabdian_masyarakat_backend.Helper;
using System.Data;

namespace sistem_informasi_lembaga_pengabdian_masyarakat_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterSkemaPengabdianController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        //[Authorize]
        [HttpPost]
        public IActionResult GetDataSkemaPengabdian([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getDataSkemaPengabdian", EncodeData.HtmlEncodeObject(value)); 
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetDataSkemaPengabdianById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getSkemaPengabdianByID", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }


        //[Authorize]
        [HttpPost]
        public IActionResult CreateSkemaPengabdian([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_createSkemaPengabdian", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult EditSkemaPengabdian([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_editSkemaPengabdian", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult DeleteSkemaPengabdian([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_deleteSkemaPengabdian", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult GetListDataSkemaPengabdian([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("lpm_getListSkemaPengabdian", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
    

