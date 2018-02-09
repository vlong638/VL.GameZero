using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VL.GameZero.ClientConsole.Utilities
{
    public class HTTPHelper
    {
        public static async Task<string> GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var response = await request.GetResponseAsync();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
        public static async Task<string> POST(string url, string postString)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            byte[] data = Encoding.UTF8.GetBytes(postString);
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            var response = await request.GetResponseAsync();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
