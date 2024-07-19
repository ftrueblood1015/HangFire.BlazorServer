using HangFire.Domain.Interfaces.Services;
using HangFire.Domain.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HangFire.BlazorServer.Pages.Houses
{
    public partial class HouseDetail
    {
        [Parameter]
        public int EntityId { get; set; }

        [Inject]
        private IHouseService? Service { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Inject]
        private ISnackbar? SnackbarService { get; set; }

        public House? House { get; set; } = new House();

        public bool success;

        public MudForm? Form;

        protected override async Task OnInitializedAsync()
        {
            House = await GetViewModel();
            await base.OnInitializedAsync();
        }

        public async Task<House> GetViewModel()
        {
            if (Service == null)
            {
                return new House();
            }

            if (EntityId == 0)
            {
                return new House();
            }

            return Service.GetById(EntityId)!;
        }

        public async Task Update()
        {
            await Form!.Validate();
            if (Service == null)
            {
                return;
            }

            if (EntityId == 0)
            {
                return;
            }

            if (Form!.IsValid)
            {
                House = Service.Update(House!);

                success = true;
                StateHasChanged();

                ShowSnackbarMessage("Successfully Updated", Color.Success);
            }
            else
            {
                ShowSnackbarMessage("Could Not Update", Color.Error);
            }
        }

        public void Cancel(string route)
        {
            if (NavigationManager != null)
            {
                NavigationManager.NavigateTo(route);
            }
        }

        public void ShowSnackbarMessage(string Message, Color Color)
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
    }
}
