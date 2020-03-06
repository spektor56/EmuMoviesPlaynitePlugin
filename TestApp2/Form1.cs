using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using EmuMoviesApi.Models;

namespace TestApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var api = new EmuMoviesApi.Api("","");
            
            var media = await api.GetMedia();
            foreach (var name in media.Select(med => med.Name))
            {
                Debug.Print(name.ToString());
            }

            
            var systems = await api.GetSystems();
            foreach (var name in systems.Select(sys => sys.Lookup))
            {
                Debug.Print(name.ToString());
            }
            var test = await api.GetBulkMediaUrl(new List<string> {"Legend of Zelda, The (USA)"}, "Nintendo_NES", MediaType.Artwork);
            var test2 = await api.GetBulkMediaUrl(new List<string> { "Legend of Zelda, The (USA)" }, "Nintendo_NES", MediaType.Background);
            var boxUrl = await api.GetBulkMediaUrl(new List<string> { "Legend of Zelda, The (USA)" }, "Nintendo_NES", MediaType.Box);
            var boxUrl2 = await api.GetBulkMediaUrl(new List<string> { "BattleToads" ,"Abadox", "Super Mario Bros. + Duck Hunt (USA)" }, "Nintendo_NES", MediaType.Box);
        }
    }
}
