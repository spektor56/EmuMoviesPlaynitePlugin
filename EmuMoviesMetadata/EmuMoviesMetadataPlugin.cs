using EmuMoviesApi;
using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace EmuMoviesMetadata
{
    public class EmuMoviesMetadataPlugin : MetadataPlugin
    {
        internal readonly EmuMoviesMetadataSettings Settings;
        public bool Initializing { get; private set; } = true;
        public Api EmuMoviesApi { get; private set; }
        
        public Dictionary<string, string> PlatformTranslationTable = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
            {"Apple II","Apple_II"},
            {"Apple IIgs","Apple_IIGS"},
            {"Atari 2600","Atari_2600"},
            {"Atari 5200","Atari_5200"},
            {"Atari 7800","Atari_7800"},
            {"Atari Jaguar","Atari_Jaguar"},
            {"Atari Lynx","Atari_Lynx"},
            {"Bandai WonderSwan","Bandai_WonderSwan"},
            {"Bandai WonderSwan Color","Bandai_WonderSwan_Color"},
            {"Capcom CP System I","Capcom_Play_System"},
            {"Capcom CP System II","Capcom_Play_System_II"},
            {"Capcom CP System III","Capcom_Play_System_III"},
            {"CAVE CV1000","Cave"},
            {"Coleco ColecoVision","Coleco_Vision"},
            {"ColecoVision","Coleco_Vision"},
            {"Commodore 64","Commodore_64"},
            {"Commodore Amiga","Commodore_Amiga"},
            {"Commodore Amiga CD32","Commodore_Amiga_CD32"},
            {"Commodore C128","Commodore_128"},
            {"Commodore PET","Commodore_PET"},
            {"Commodore PLUS4","Commodore_Plus_4"},
            {"Commodore VIC20","Commodore_VIC_20"},
            {"Daphne","Daphne"},
            {"DOS","Microsoft_DOS"},
            {"GCE Vectrex","GCE_Vectrex"},
            {"MAME 2003 Plus","ArcadePC"},
            {"Mattel Intellivision","Mattel_Intellivision"},
            {"Microsoft MSX","MSX"},
            {"Microsoft MSX2","MSX_2"},
            {"Microsoft Xbox","Microsoft_Xbox"},
            {"Microsoft Xbox 360","Microsoft_Xbox_360"},
            {"NEC PC Engine SuperGrafx","NEC_PC_Engine"},
            {"NEC PC-9801","NEC_PC_9801"},
            {"NEC PC-FX","NEC_PC_FX"},
            {"NEC SuperGrafx","NEC_SuperGrafx"},
            {"NEC TurboGrafx 16","NEC_TurboGrafx_16"},
            {"NEC TurboGrafx-CDa","NEC_TurboGrafx_CD"},
            {"Nintendo 3DS","Nintendo_3DS"},
            {"Nintendo 64","Nintendo_N64"},
            {"Nintendo 64DD","Nintendo_64DD"},
            {"Nintendo DS","Nintendo_DS"},
            {"Nintendo Entertainment System","Nintendo_NES"},
            {"Nintendo Family Computer Disk System","Nintendo_Famicom_Disk_System"},
            {"Nintendo Game Boy","Nintendo_Game_Boy"},
            {"Nintendo Game Boy Advance","Nintendo_Game_Boy_Advance"},
            {"Nintendo Game Boy Color","Nintendo_Game_Boy_Color"},
            {"Nintendo GameCube","Nintendo_GameCube"},
            {"Nintendo Satellaview","Nintendo_Satellaview"},
            {"Nintendo Virtual Boy","Nintendo_Virtual_Boy"},
            {"Nintendo Wii","Nintendo_Wii"},
            {"Nintendo Wii U","Nintendo_Wii_U"},
            {"PC","Microsoft_Windows"},
            {"Philips CD-i","Philips_CD_i"},
            {"Phillips Videopac+","Philips_Videopac_"},
            {"Sammy Atomiswave","Sammy_Atomiswave"},
            {"Sega 32X","Sega_32X"},
            {"Sega CD","Sega_CD"},
            {"Sega Dreamcast","Sega_Dreamcast"},
            {"Sega Game Gear","Sega_Game_Gear"},
            {"Sega Genesis","Sega_Genesis"},
            {"Sega Hikaru","Sega_Hikaru"},
            {"Sega Master System","Sega_Master_System"},
            {"Sega Model 2","Sega_Model_2"},
            {"Sega NAOMI","Sega_Naomi"},
            {"Sega PICO","Sega_Pico"},
            {"Sega Saturn","Sega_Saturn"},
            {"Sega SG 1000","Sega_SG_1000"},
            {"Sega ST V","Sega_ST_V"},
            {"Sharp X68000","Sharp_X68000"},
            {"Sinclair ZX 81","Sinclair_ZX_81"},
            {"Sinclair ZX Spectrum","Sinclair_ZX_Spectrum"},
            {"SNK Neo Geo CD","SNK_Neo_Geo_CD"},
            {"SNK Neo Geo Pocket","SNK_Neo_Geo_Pocket"},
            {"SNK Neo Geo Pocket Color","SNK_Neo_Geo_Pocket_Color"},
            {"Sony PlayStation","Sony_Playstation"},
            {"Sony PlayStation 2","Sony_Playstation_2"},
            {"Sony PlayStation 3","Sony_PlayStation_3"},
            {"Sony PSP","Sony_PSP"},
            {"Super Nintendo Entertainment System","Nintendo_SNES"},
            {"WonderSwan","Bandai_WonderSwan"},
            {"WonderSwan Color","Bandai_WonderSwan_Color"}
        };

        public EmuMoviesMetadataPlugin(IPlayniteAPI playniteAPI) : base(playniteAPI)
        {
            Settings = new EmuMoviesMetadataSettings(this);
            EmuMoviesApi = new Api(Settings.Username, Settings.Password);
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return Settings;
        }

        public override UserControl GetSettingsView(bool firstRunView)
        {
            return new EmuMoviesMetadataSettingsView(this);
        }

        public override IEnumerable<ExtensionFunction> GetFunctions()
        {
            return base.GetFunctions();
        }

        public override void OnGameStarting(Game game)
        {
            base.OnGameStarting(game);
        }

        public override void OnGameStarted(Game game)
        {
            base.OnGameStarted(game);
        }

        public override void OnGameStopped(Game game, long ellapsedSeconds)
        {
            base.OnGameStopped(game, ellapsedSeconds);
        }

        public override void OnGameInstalled(Game game)
        {
            base.OnGameInstalled(game);
        }

        public override void OnGameUninstalled(Game game)
        {
            base.OnGameUninstalled(game);
        }

        public override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
        }

        public override Guid Id => Guid.Parse("000001D9-DBD1-46C6-B5D0-B1BD557D10E4");
        public override OnDemandMetadataProvider GetMetadataProvider(MetadataRequestOptions options)
        {
            return new EmuMoviesMetadataProvider(options, this);
        }

        public override string Name { get; } = "EmuMovies";

        public override List<MetadataField> SupportedFields { get; } = new List<MetadataField>
        {
            //MetadataField.Name,
            //MetadataField.Genres,
            //MetadataField.ReleaseDate,
            //MetadataField.Developers,
            //MetadataField.Publishers,
            //MetadataField.Tags,
            //MetadataField.Description,
            //MetadataField.Links,
            //MetadataField.CriticScore,
            //MetadataField.CommunityScore,
            MetadataField.Icon,
            MetadataField.CoverImage,
            MetadataField.BackgroundImage

        };

    }
}
