using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Helper
{
    public static class IJSRuntimeExtension
    {

        public static async ValueTask ToasterSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("toastr.success", "success",message);
        }
        public static async ValueTask ToasterError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("toastr.error", "error", message);
        }

        public static async ValueTask ShowSweet(this IJSRuntime jSRuntime, string message)
        {
             await jSRuntime.InvokeVoidAsync("Swal.fire", "Good job!", message, "success");
        }

    }
}
