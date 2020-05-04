using ChessApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessApp.Pages {
    public partial class Index : ChessClient {

        public void NotifyMove() {
            InvokeAsync(() => {
                StateHasChanged();
            });
        }

    }
}
