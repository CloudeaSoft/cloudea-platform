using Cloudea.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyService;
using MySqlX.XDevAPI.Common;

namespace Cloudea.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        private readonly My1Service service;
        public MyController(My1Service service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<int> Reee()
        {
            return service.Send();
        }

        [HttpGet("{a}/{b}")]
        public ActionResult<int> Add(int a, int b)
        {

            return Ok(a + b);
        }

        [HttpGet]
        public async Task<ActionResult> MyJsts()
        {
            return Ok(new List<MyJst>
            {
                new() {
                        BS_OBJ_NAME= "DDDD",
                        CGO_MBL_NO= "DDDD22110033",
                        CGO_MBL_TYPE= "1",
                        CGO_ATD_POL= "2023-10-01 00:00",
                        BADNAME_d1= "XM小明",
                        BADNAME_e1= "2023-10-01 23:00",
                        BADNAME_f1= "XH小华",
                        BADNAME_h1= "1",
                        BADNAME_i1= "XF小芳"
                },
                new() {
                        BS_OBJ_NAME= "DDDD",
                        CGO_MBL_NO= "DDDD22110032",
                        CGO_MBL_TYPE= "1",
                        CGO_ATD_POL= "2023-10-01 00:00",
                        BADNAME_d1= "XM小明",
                        BADNAME_e1= "2023-10-01 23:00",
                        BADNAME_f1= "XH小华",
                        BADNAME_h1= "0",
                        BADNAME_i1= ""
                },
                new()  {
                        BS_OBJ_NAME="DDDD",
                        CGO_MBL_NO="DDDD22110035",
                        CGO_MBL_TYPE="2",
                        CGO_ATD_POL="2023-10-02 00:00",
                        BADNAME_d1="XM小明",
                        BADNAME_e1="2023-10-02 23:00",
                        BADNAME_f1="XH小华",
                        BADNAME_h1="1",
                        BADNAME_i1="系统"
                },
                new()  {
                        BS_OBJ_NAME="DDDD",
                        CGO_MBL_NO="DDDD22110039",
                        CGO_MBL_TYPE="3",
                        CGO_ATD_POL="2023-10-05 00:00",
                        BADNAME_d1="XM小明",
                        BADNAME_e1="2023-10-05 23:00",
                        BADNAME_f1="XH小华",
                        BADNAME_h1="1",
                        BADNAME_i1="系统"
                },
                new() {
                        BS_OBJ_NAME="DDDD",
                        CGO_MBL_NO="DDDD22110039",
                        CGO_MBL_TYPE="4",
                        CGO_ATD_POL="2023-10-05 00:00",
                        BADNAME_d1="XM小明",
                        BADNAME_e1="2023-10-05 23:00",
                        BADNAME_f1="XH小华",
                        BADNAME_h1="1",
                        BADNAME_i1="系统"
                },
                new()  {
                        BS_OBJ_NAME="DDDD",
                        CGO_MBL_NO="DDDD22110039",
                        CGO_MBL_TYPE="5",
                        CGO_ATD_POL="2023-10-05 00:00",
                        BADNAME_d1="XM小明",
                        BADNAME_e1="2023-10-05 23:00",
                        BADNAME_f1="XH小华",
                        BADNAME_h1="1",
                        BADNAME_i1="系统"
                }
            });
        }

    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MyJst
    {
        public string? BS_OBJ_NAME { get; set; }
        public string? CGO_MBL_NO { get; set; }
        public string? CGO_MBL_TYPE { get; set; }
        public string? CGO_ATD_POL { get; set; }
        public string? BADNAME_d1 { get; set; }
        public string? BADNAME_e1 { get; set; }
        public string? BADNAME_f1 { get; set; }
        public string? BADNAME_h1 { get; set; }
        public string? BADNAME_i1 { get; set; }
    }

}
