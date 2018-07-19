using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using App.Audit.Domain.Model;
using Newtonsoft.Json;

namespace App.Audit.FunctionTest
{
    public class AuditRestClient: BaseRestClient
    {
        const string BaseUrl = "http://localhost:6559/api";

       
        public async Task<HttpResponseMessage> Post(AuditEvent postObj)
        {
            
                var jsonString = JsonConvert.SerializeObject(postObj);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await PostService(string.Format("{0}/Audit", BaseUrl), content);

               return response;
        }
    }
}