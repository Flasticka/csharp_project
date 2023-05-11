using System;
using System.Collections.Generic;

namespace csharp_project.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string AppName => "TODO LIST";
    public List<Op> SelectedItems => new List<Op>() {new Op("rfrgrg"),new Op("kokot"), new Op("oko")};

}


