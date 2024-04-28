using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Planner_Domain.Helpers.HttpHelpers
{
    public static class CustomHttpService
    {
        private static readonly HttpClient _httpClient;

        static CustomHttpService()
        {
            _httpClient = new HttpClient();
        }

        public static void Initialize(string domainBaseApiAddress)
        {
            _httpClient.BaseAddress = new Uri(domainBaseApiAddress);
        }

        public static CustomResponseMessage<TResponse> Get<TResponse>(object? requestMessage,
            string apiAddress)
        {
            var httpRequestMessage = HttpRequestMessageCreator(HttpMethod.Get, requestMessage, apiAddress);
            var httpResponseMessage = Send(httpRequestMessage);
            return HttpResponseHandler<TResponse>(httpResponseMessage);
        }

        public static async Task<CustomResponseMessage<TResponse>> GetAsync<TResponse>(object? requestMessage,
            string apiAddress)
        {
            var httpRequestMessage = HttpRequestMessageCreator(HttpMethod.Get, requestMessage, apiAddress);
            var httpResponseMessage = await SendAsync(httpRequestMessage);
            return await HttpResponseHandlerAsync<TResponse>(httpResponseMessage);
        }

        public static CustomResponseMessage<TResponse> Post<TResponse>(object? requestMessage,
    string apiAddress)
        {
            var httpRequestMessage = HttpRequestMessageCreator(HttpMethod.Post, requestMessage, apiAddress);
            var httpResponseMessage = Send(httpRequestMessage);
            return HttpResponseHandler<TResponse>(httpResponseMessage);
        }

        public static async Task<CustomResponseMessage<TResponse>> PostAsync<TResponse>(object? requestMessage,
            string apiAddress)
        {
            var httpRequestMessage = HttpRequestMessageCreator(HttpMethod.Post, requestMessage, apiAddress);
            var httpResponseMessage = await SendAsync(httpRequestMessage);
            return await HttpResponseHandlerAsync<TResponse>(httpResponseMessage);
        }

        public static CustomResponseMessage<TResponse> Delete<TResponse>(object? requestMessage,
    string apiAddress)
        {
            var httpRequestMessage = HttpRequestMessageCreator(HttpMethod.Delete, requestMessage, apiAddress);
            var httpResponseMessage = Send(httpRequestMessage);
            return HttpResponseHandler<TResponse>(httpResponseMessage);
        }

        public static async Task<CustomResponseMessage<TResponse>> DeleteAsync<TResponse>(object? requestMessage,
            string apiAddress)
        {
            var httpRequestMessage = HttpRequestMessageCreator(HttpMethod.Delete, requestMessage, apiAddress);
            var httpResponseMessage = await SendAsync(httpRequestMessage);
            return await HttpResponseHandlerAsync<TResponse>(httpResponseMessage);
        }

        private static HttpRequestMessage HttpRequestMessageCreator(HttpMethod httpMethod,
            object? requestMessage, string apiAddress)
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, apiAddress);
            if (requestMessage != null)
            {
                httpRequestMessage.Content = new StringContent(
                    JsonSerializer.Serialize(requestMessage,
                        options: new JsonSerializerOptions()
                        {
                            ReferenceHandler = new CustomReferenceHandler(),
                            PropertyNameCaseInsensitive = true
                        }), Encoding.UTF8, "application/json");
            }

            return httpRequestMessage;
        }

        private static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
        {
            return await _httpClient.SendAsync(httpRequestMessage);
        }

        private static HttpResponseMessage Send(HttpRequestMessage httpRequestMessage)
        {
            return _httpClient.Send(httpRequestMessage);
        }

        private static TResponse HttpResponseContentReader<TResponse>(HttpContent httpContent)
        {
            using var reader = new StreamReader(httpContent.ReadAsStream());
            var contentStr = reader.ReadToEnd();
            var content = JsonSerializer.Deserialize<TResponse>(contentStr,
                new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                });
            return content;
        }

        private static Task<TResponse> HttpResponseContentReaderAsync<TResponse>(HttpContent httpContent)
        {
            var content = httpContent.ReadFromJsonAsync<TResponse>(new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            });
            return content;
        }

        private static async Task<CustomResponseMessage<TResponse>> HttpResponseHandlerAsync<TResponse>(
            HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var customResponseMessage =
                    await HttpResponseContentReaderAsync<CustomResponseMessage<TResponse>>(httpResponseMessage.Content);
                if (customResponseMessage.IsSuccess)
                {
                    return customResponseMessage;
                }

                throw new CustomException(customResponseMessage.MessageForUser);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomException("رکورد مورد نظر یافت نشد", httpResponseMessage.StatusCode);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new CustomException("تمامی دیتا ها به درستی وارد نشده است.", httpResponseMessage.StatusCode);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new CustomException("کاربر احراز هویت نشده است", httpResponseMessage.StatusCode);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new CustomException("کاربر دسترسی ندارد", httpResponseMessage.StatusCode);
            }
            else
            {
                throw new CustomException("بروز خطای ارتباطی", httpResponseMessage.StatusCode);
            }
            //these status codes are not handled in server
        }

        private static CustomResponseMessage<TResponse> HttpResponseHandler<TResponse>(
            HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var customResponseMessage =
                    HttpResponseContentReader<CustomResponseMessage<TResponse>>(httpResponseMessage.Content);
                if (customResponseMessage.IsSuccess)
                {
                    return customResponseMessage;
                }

                throw new CustomException(customResponseMessage.MessageForUser);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomException("رکورد مورد نظر یافت نشد", httpResponseMessage.StatusCode);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new CustomException("تمامی دیتا ها به درستی وارد نشده است.", httpResponseMessage.StatusCode);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new CustomException("کاربر احراز هویت نشده است", httpResponseMessage.StatusCode);
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new CustomException("کاربر دسترسی ندارد", httpResponseMessage.StatusCode);
            }
            else
            {
                throw new CustomException("بروز خطای ارتباطی", httpResponseMessage.StatusCode);
            }
            //these status codes are not handled in server
        }
    }
}