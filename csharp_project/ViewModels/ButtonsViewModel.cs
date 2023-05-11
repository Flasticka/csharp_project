using System.Collections.Generic;
using System.IO;

namespace csharp_project.ViewModels;

public class ButtonsViewModel : ViewModelBase
{
    public List<Op> ButtonsItems => new List<Op>()
    {
        new Op(Path.Combine("..", "Assets", "cross.svg")), new Op(Path.Combine("..", "Assets", "check_mark.svg")),
        new Op(Path.Combine("..", "Assets", "plus.svg"))
    };
}