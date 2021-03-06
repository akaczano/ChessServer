#pragma checksum "C:\Users\thelo\source\repos\ChessApp\ChessApp\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c8bd640a2bc497f05b87d001b46c97180e191a6c"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace ChessApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using ChessApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\thelo\source\repos\ChessApp\ChessApp\_Imports.razor"
using ChessApp.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Pages\Index.razor"
using ChessModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Pages\Index.razor"
using ChessApp.Data;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 17 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Pages\Index.razor"
       

    ChessPosition pos = new ChessPosition();
    ChessBoard Board;

    private void GoBack() {
        pos.UndoLastMove();
    }
    private void GoForward() {
        pos.RedoMove();
    }


    protected override async Task OnInitializedAsync() {
        pos = await Service.GetPositionAsync();
        Service.JoinParty((ChessClient)this);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private PositionService Service { get; set; }
    }
}
#pragma warning restore 1591
