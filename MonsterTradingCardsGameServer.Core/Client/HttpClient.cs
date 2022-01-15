using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Client
{
    internal class HttpClient : IClient
    {
        private readonly TcpClient _connection;

        public HttpClient(TcpClient connection)
        {
            _connection = connection;
        }

        public RequestContext ReceiveRequest()
        {
            var reader = new StreamReader(_connection.GetStream());

            string path = null;
            string version = null;
            var method = HttpMethod.Get;
            var isFirstLine = true;
            var header = new Dictionary<string, string>();
            var contentLength = 0;
            string payload;

            try
            {
                string line;
                while (!string.IsNullOrWhiteSpace(line = reader.ReadLine()))
                    if (isFirstLine)
                    {
                        // read HTTP method, resource path, and HTTP version
                        var info = line.Split(' ');

                        method = MethodUtilities.GetMethod(info[0]);
                        path = info[1];
                        version = info[2];

                        isFirstLine = false;
                    }
                    else
                    {
                        // read HTTP header entries
                        var info = line.Split(":", 2);
                        header.Add(info[0].Trim(), info[1].Trim());
                        if (info[0] == "Content-Length") contentLength = int.Parse(info[1]);
                    }
            }
            catch (IOException)
            {
                return null;
            }

            if (path == null) return null;

            // read HTTP body and check the payload
            if (contentLength <= 0 || !header.ContainsKey("Content-Type"))
                return new RequestContext
                {
                    Method = method,
                    ResourcePath = path,
                    HttpVersion = version,
                    Header = header,
                    Payload = null
                };
            var data = new StringBuilder(200);
            var buffer = new char[1024];
            var totalBytesRead = 0;
            while (totalBytesRead < contentLength)
                try
                {
                    var bytesRead = reader.Read(buffer, 0, 1024);
                    totalBytesRead += bytesRead;
                    if (bytesRead == 0) break;
                    data.Append(buffer, 0, bytesRead);
                }
                catch (IOException)
                {
                    return null;
                }

            payload = data.ToString();

            // TODO: maybe check the content type for the payload

            return new RequestContext
            {
                Method = method,
                ResourcePath = path,
                HttpVersion = version,
                Header = header,
                Payload = payload
            };
        }

        public void SendResponse(Response.Response response)
        {
            var writer = new StreamWriter(_connection.GetStream()) {AutoFlush = true};
            writer.Write($"HTTP/1.1 {(int) response.StatusCode} {response.StatusCode}\r\n");

            if (!string.IsNullOrEmpty(response.Payload))
            {
                var payload = Encoding.UTF8.GetBytes(response.Payload);
                writer.Write($"Content-Length: {payload.Length}\r\n");
                writer.Write("\r\n");
                writer.Write(Encoding.UTF8.GetString(payload));
                writer.Close();
            }
            else
            {
                writer.Write("\r\n");
                writer.Close();
            }
        }
    }
}