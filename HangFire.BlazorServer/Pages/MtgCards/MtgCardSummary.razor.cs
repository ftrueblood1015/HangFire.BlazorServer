using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HangFire.BlazorServer.Pages.MtgCards
{
    public partial class MtgCardSummary
    {
        [Inject]
        protected IMtgCardService? Service { get; set; }

        [Inject]
        private IDialogService? DialogService { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        public List<MtgCard>? Entities { get; set; }

        public string? _searchString;

        protected override async Task OnInitializedAsync()
        {
            await GetData();
        }

        public async Task GetData()
        {
            if (Service == null)
            {
                throw new Exception($"{nameof(Service)}  is null!");
            }

            var Response = Service.GetAll();
            Entities = Response != null ? Response.ToList() : new List<MtgCard>();
            StateHasChanged();
        }

        public Func<MtgCard, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Rarity!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Type!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.ConvertedManaCost.ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.PennyRank.ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.EdhrecRank.ToString()!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public void View(MtgCard item)
        {
            if (NavigationManager == null)
            {
                return;
            }
            NavigationManager.NavigateTo($"{item.ScryfallUri}");
        }
    }
}
