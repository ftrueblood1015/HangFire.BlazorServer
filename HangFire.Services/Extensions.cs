using HangFire.Services.Services;
using Newtonsoft.Json;

namespace HangFire.Services
{
    public static class Extensions
    {
        public static async Task<T> GetData<T>(this ScryfallApiServerClient client, Uri uri)
        {
            uri = uri.VerifyNotNull();
            client = client.VerifyNotNull();

            string rawData = await client.Client.GetStringAsync(uri);

            
            var data = JsonConvert.DeserializeObject<T>(rawData);

            if (data == null)
            {
                return Activator.CreateInstance<T>();
            }

            return data;
        }

        public static T VerifyNotNull<T>(this T? value)
        where T : class
        {
            switch (value)
            {
                case string strVal:
                    if (string.IsNullOrWhiteSpace(strVal))
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    return value;

                default:
                    return value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
