using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoutubeSubtitleExplorer.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppDataTemplates : ResourceDictionary
    {
        public AppDataTemplates()
        {
            InitializeComponent();
        }
    }
}