using System.Windows.Controls;

namespace EmuMoviesMetadata
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class EmuMoviesMetadataSettingsView : UserControl
    {
        private readonly EmuMoviesMetadataPlugin _plugin;

        public EmuMoviesMetadataSettingsView()
        {
            InitializeComponent();
        }

        public EmuMoviesMetadataSettingsView(EmuMoviesMetadataPlugin plugin)
        {
            _plugin = plugin;
            InitializeComponent();
        }

    }
}
