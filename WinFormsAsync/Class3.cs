using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncExample2
{
    internal class Class3
    {
        public async Task<string> GetAttachment(string filename, CancellationToken cancellationToken)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = "api.example.com";

            var Path = "/files/download";
            uriBuilder.Path = Path;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriBuilder.ToString());
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Add("authorization", access_token); //if any
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(uriBuilder.ToString(), cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    System.Net.Http.HttpContent content = response.Content;
                    Stream contentStream = await content.ReadAsStreamAsync(); // get the actual content stream


                    using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        await contentStream.CopyToAsync(fileStream, cancellationToken);
                    }

                    return filename;
                    //return File(contentStream, content_type, filename);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }



        }
    }
    internal static class Class2
    {
        public static async Task DownloadFileTaskAsync(this HttpClient client, Uri uri, string FileName)
        {
            using (var s = await client.GetStreamAsync(uri))
            {
                using (var fs = new FileStream(FileName, FileMode.CreateNew))
                {
                    await s.CopyToAsync(fs);
                }
            }
        }


    }
}
