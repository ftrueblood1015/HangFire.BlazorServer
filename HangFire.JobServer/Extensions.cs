using HangFire.Domain.Entities;
using HangFire.Domain.Models;
using HangFire.Services.Services;
using Newtonsoft.Json;

namespace HangFire.JobServer
{
    public static class Extensions
    {
        public static MtgCard ScryfallTransform(this ScryfallMtgCard scryfallMtgCard)
        {
            scryfallMtgCard.VerifyNotNull();

            return new MtgCard()
            {
                Id = 0,
                Name = scryfallMtgCard.Name,
                Description = scryfallMtgCard.Name,
                OracleId = new Guid(scryfallMtgCard.oracle_id!),
                ScryfallUri = scryfallMtgCard.scryfall_uri,
                ColorIdentity = String.Join(",", scryfallMtgCard.color_identity!),
                manaCost = scryfallMtgCard.mana_cost,
                ConvertedManaCost = (int)(decimal.Parse(scryfallMtgCard.cmc)),
                Type = scryfallMtgCard.type_line,
                OracleText = scryfallMtgCard.oracle_text,
                Power = int.TryParse(scryfallMtgCard.power, out int x) ? x : 0,
                Toughness = int.TryParse(scryfallMtgCard.toughness, out int y) ? y : 0,
                Rarity = scryfallMtgCard.rarity,
                EdhrecRank = scryfallMtgCard.edhrec_rank,
                PennyRank = scryfallMtgCard.penny_rank,
                ProducesMana = scryfallMtgCard.produced_mana != null ? true : false,
                Slug = scryfallMtgCard.Name!.ToUpper(),
                Keywords = String.Join(",", scryfallMtgCard.keywords!),
            };
        }

        public static MtgCard MapScryFallOnto(this MtgCard mtgCard, ScryfallMtgCard scryfallMtgCard)
        {
            mtgCard.Name = scryfallMtgCard.Name;
            mtgCard.Description = scryfallMtgCard.Name;
            mtgCard.ScryfallUri = scryfallMtgCard.scryfall_uri;
            mtgCard.ColorIdentity = String.Join(",", scryfallMtgCard.color_identity!);
            mtgCard.manaCost = scryfallMtgCard.mana_cost;
            mtgCard.ConvertedManaCost = int.Parse(scryfallMtgCard.cmc);
            mtgCard.Type = scryfallMtgCard.type_line;
            mtgCard.OracleText = scryfallMtgCard.oracle_text;
            mtgCard.Power = int.TryParse(scryfallMtgCard.power, out int x) ? x : 0;
            mtgCard.Toughness = int.TryParse(scryfallMtgCard.toughness, out int y) ? y : 0;
            mtgCard.Rarity = scryfallMtgCard.rarity;
            mtgCard.EdhrecRank = scryfallMtgCard.edhrec_rank;
            mtgCard.ProducesMana = scryfallMtgCard.produced_mana != null ? true : false;
            mtgCard.Keywords = String.Join(",", scryfallMtgCard.keywords!);

            return mtgCard;
        }

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
