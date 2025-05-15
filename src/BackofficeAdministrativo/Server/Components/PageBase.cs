using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BackofficeAdministrativo.Server.Components
{
    public class PageBase : ComponentBase
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected async Task ShowToast(string message, string type = "success")
        {
            await JSRuntime.InvokeVoidAsync("showToast", message, type);
        }

        protected async Task<bool> ConfirmAction(string message)
        {
            return await JSRuntime.InvokeAsync<bool>("confirmAction", message);
        }

        protected string FormatCurrency(decimal amount)
        {
            return $"₡{amount:N2}";
        }

        protected void NavigateTo(string url)
        {
            NavigationManager.NavigateTo(url);
        }

        protected async Task InitializeDataTable(string tableId)
        {
            await JSRuntime.InvokeVoidAsync("initializeDataTable", tableId);
        }
    }
}
