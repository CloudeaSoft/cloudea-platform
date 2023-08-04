using Cloudea.Core;
using Cloudea.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyService;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System.Net;

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

        /*[HttpGet]
        public async Task<int> Reee()
        {
            return service.Send();
        }*/

        [HttpGet("{a}/{b}")]
        public ActionResult<int> Add(int a, int b)
        {

            return Ok(a + b);
        }

        [HttpGet]
        public async Task<List<MyJst>> MyJsts()
        {
            var list = new List<MyJst>();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:53812/api/TdRequest/GetListABC");
                list = await response.Content.ReadFromJsonAsync<List<MyJst>>();
            }
            /* var list = new PageResponse<MyJst>();
             using(HttpClient client = new HttpClient())
             {
                 try
                 {
                     var response = await client.GetAsync("http://localhost:53812/api/TdRequest/GetListABC");
                     string content = await response.Content.ReadAsStringAsync();
                     if (response.StatusCode == HttpStatusCode.OK)
                     {
                         list = JsonConvert.DeserializeObject<PageResponse<MyJst>>(content);
                     }
                     else
                     {
                         return list;
                     }
                 }
                 catch(Exception ex)
                 {
                     return list;
                 }
             }*/
            return list;
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MyJst
    {
        //业务编号
        public string BS_CODE { get; set; }
        //主提单号
        public string CGO_MBL_NO { get; set; }
        //签单类型
        public string CGO_MBL_TYPE { get; set; } = "1";
        //签回人员
        public string QH_EXECUTEER { get; set; }
        //签回日期
        public DateTime? QH_CRT_DATE { get; set; }
        //实际签回人员
        public string QH_REAL_EXER { get; set; }
        //扫描人员
        public string SM_EXECUTEER { get; set; }
        //扫描日期
        public DateTime? SM_CRT_DATE { get; set; }
        //实际扫描人员
        public string SM_REAL_EXER { get; set; }
        //接收状态
        public string RECEIVE_STATUS { get; set; } = "0";
        //接收人员
        public string RECEIVER { get; set; } = "CCZ蔡蔡子";
    }
}
