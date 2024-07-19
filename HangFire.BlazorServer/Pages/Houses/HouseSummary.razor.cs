using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HangFire.BlazorServer.Pages.Houses
{
    public partial class HouseSummary
    {
        [Inject]
        protected IHouseService? Service { get; set; }

        [Inject]
        private IDialogService? DialogService { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        public List<House>? Entities { get; set; }

        public string? _searchString;

        private string State = "Message box hasn't been opened yet";

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
            Entities = Response != null ? Response.ToList() : new List<House>();
            StateHasChanged();
        }

        public Func<House, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Name!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Description!.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public async void OnDeleteClicked(House item)
        {
            if (DialogService == null)
            {
                throw new Exception($"{nameof(DialogService)}  is null!");
            }

            bool? result = await DialogService.ShowMessageBox(
                "Warning",
                "Deleting can not be undone!",
                yesText:"Delete!", cancelText:"Cancel"
            );

            State = result == null ? "Canceled" : "Deleted!";

            if (State != "Canceled")
            {
                await Delete(item);
            }
        }

        private async Task<bool> Delete(House Item)
        {
            if (Service == null)
            {
                throw new Exception($"{nameof(Service)} is null!");
            }

            try
            {
                var result = Service.Delete(Item);

                if (result)
                {
                    ShowSnackbarMessage($"Deleted {Item.Name}", Color.Success);
                    await GetData();
                }
                else
                {
                    ShowSnackbarMessage($"Could Not Delete {Item.Name}", Color.Error);
                }

                return result;
            }
            catch (Exception ex)
            {
                ShowSnackbarMessage($"Could Not Delete {Item.Name}: {ex}", Color.Error);
                return false;
            }
        }

        private void ShowSnackbarMessage(string Message, Color Color)
        {
            if (SnackbarService == null)
            {
                throw new ArgumentNullException(nameof(SnackbarService));
            }

            SnackbarService.Add<MudChip>(new Dictionary<string, object>() {
                { "Text", $"{Message}" },
                { "Color", Color }
            });
        }

        public void Edit(House item, string route)
        {
            if (NavigationManager == null)
            {
                return;
            }
            NavigationManager.NavigateTo($"/house/{item.Id}");
        }

        public void Add(string route)
        {
            if (NavigationManager == null)
            {
                return;
            }
            NavigationManager.NavigateTo($"/house");
        }
    }
}
