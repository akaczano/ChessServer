#pragma checksum "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e455c030386661dbda3b16be4ec829812a5439bf"
// <auto-generated/>
#pragma warning disable 1591
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
#line 1 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
using ChessModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
using ChessApp.Data;

#line default
#line hidden
#nullable disable
    public partial class ChessBoard : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "draggable", "false");
            __builder.AddAttribute(2, "class", "board");
            __builder.AddMarkupContent(3, "\r\n\r\n");
#nullable restore
#line 7 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
     for (int i = 7; i >= 0; i--) {
        for (int j = 0; j < 8; j++) {
            int cachei = i;
            int cachej = j;

            ChessPiece p = Position.Board[j, i];

#line default
#line hidden
#nullable disable
            __builder.AddContent(4, "            ");
            __Blazor.ChessApp.Shared.ChessBoard.TypeInference.CreateCascadingValue_0(__builder, 5, 6, 
#nullable restore
#line 13 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
                                   this

#line default
#line hidden
#nullable disable
            , 7, (__builder2) => {
                __builder2.AddMarkupContent(8, "\r\n                ");
                __builder2.OpenComponent<ChessApp.Shared.Square>(9);
                __builder2.AddAttribute(10, "Occupant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<ChessModels.ChessPiece>(
#nullable restore
#line 14 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
                                   p

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(11, "I", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 14 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
                                          cachei

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(12, "J", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 14 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
                                                      cachej

#line default
#line hidden
#nullable disable
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(13, "\r\n            ");
            }
            );
            __builder.AddMarkupContent(14, "\r\n");
#nullable restore
#line 16 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
        }
    }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(15, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 23 "C:\Users\thelo\source\repos\ChessApp\ChessApp\Shared\ChessBoard.razor"
       
    [Parameter]
    public ChessPosition Position { get; set; }

    public Location UnderDrag { get; set; }

    public void ExecuteMove(Location start, Location end) {
        Console.WriteLine("({0}, {1}) -> ({2}, {3})", start.X, start.Y, end.X, end.Y);
        ChessMove move = Position.BuildMove(start, end, ChessPiece.EMPTY);
        if (Position.IsMoveLegal(move)) {
            Position.MakeMoveOnBoard(move);
        }

        //this.StateHasChanged();
        Service.TellEverybody();
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private PositionService Service { get; set; }
    }
}
namespace __Blazor.ChessApp.Shared.ChessBoard
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateCascadingValue_0<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, TValue __arg0, int __seq1, global::Microsoft.AspNetCore.Components.RenderFragment __arg1)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.CascadingValue<TValue>>(seq);
        __builder.AddAttribute(__seq0, "Value", __arg0);
        __builder.AddAttribute(__seq1, "ChildContent", __arg1);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
