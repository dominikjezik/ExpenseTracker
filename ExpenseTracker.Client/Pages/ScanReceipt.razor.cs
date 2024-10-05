using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ReactorBlazorQRCodeScanner;

namespace ExpenseTracker.Client.Pages;

public partial class ScanReceipt : IAsyncDisposable
{
    [Inject] 
    private IJSRuntime JS { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }
    
    // Code snippet used from documentation
    // https://github.com/YannVasseur35/ReactorBlazorQRCodeScanner
    private QRCodeScannerJsInterop? _qrCodeScannerJsInterop;
    private Action<string>? _onQrCodeScanAction; 

    protected override async Task OnInitializedAsync()
    {
        _onQrCodeScanAction = (code) => OnQrCodeScan(code);  

        _qrCodeScannerJsInterop = new QRCodeScannerJsInterop(JS);
        await _qrCodeScannerJsInterop.Init(_onQrCodeScanAction);
    }

    private void OnQrCodeScan(string code)
    {
        NavigationManager.NavigateTo($"/expenses/create?receiptCode={code}");
    }

    public async ValueTask DisposeAsync()
    {
        await _qrCodeScannerJsInterop!.StopRecording();
    }
}
