#pragma checksum "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\Square.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1e65ef69031df34bb0bb2d9175af97102219ff77"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace ChessApp.Shared
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
#line 1 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\Square.razor"
using System.Diagnostics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\Square.razor"
using ChessModels;

#line default
#line hidden
#nullable disable
    public partial class Square : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 15 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\Square.razor"
       
    [CascadingParameter]
    public ChessBoard Board { get; set; }

    [Parameter]
    public ChessPiece Occupant { get; set; }

    [Parameter]
    public int I { get; set; }

    [Parameter]
    public int J { get; set; }

    public void HandleDrag() {
        Board.UnderDrag = new Location(J, I);
    }

    public void HandleDrop() {
        Board.ExecuteMove(Board.UnderDrag, new Location(J, I));
    }

    public void HandleDragEnter() {

    }

    public void HandleDragLeave() { }


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591