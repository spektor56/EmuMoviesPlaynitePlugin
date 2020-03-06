using Playnite.SDK.Metadata;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using EmuMoviesApi.Models;

namespace EmuMoviesMetadata
{
    public class EmuMoviesLazyMetadataProvider : OnDemandMetadataProvider
    {
        private readonly MetadataRequestOptions _options;
        private readonly EmuMoviesMetadataPlugin _plugin;
        private string _platform;

        public EmuMoviesLazyMetadataProvider(MetadataRequestOptions options, EmuMoviesMetadataPlugin plugin)
        {
            this._options = options;
            this._plugin = plugin;
        }

        private void GetPlatform()
        {
            if (_platform is null)
            {
                _platform = Regex.Replace(_options.GameData.Platform.Name,@"[^A-Za-z0-9]","_");
                if (_plugin.PlatformTranslationTable.ContainsKey(_options.GameData.Platform.Name))
                {
                    _platform = _plugin.PlatformTranslationTable[_options.GameData.Platform.Name];
                }
            }
        }

        public override MetadataFile GetCoverImage()
        {
            GetPlatform();

            var boxUrl = _plugin.EmuMoviesApi.GetBulkMediaUrl(new List<string> { Path.GetFileNameWithoutExtension(_options.GameData.GameImagePath), _options.GameData.Name }, _platform, MediaType.Box).Result;
            if (!string.IsNullOrWhiteSpace(boxUrl))
            {
                return new MetadataFile(boxUrl);
            }

            return base.GetCoverImage();
        }

        public override MetadataFile GetBackgroundImage()
        {
            GetPlatform();

            var backgroundUrl = _plugin.EmuMoviesApi.GetBulkMediaUrl(new List<string> { Path.GetFileNameWithoutExtension(_options.GameData.GameImagePath), _options.GameData.Name }, _platform, MediaType.Background).Result;
            if (string.IsNullOrWhiteSpace(backgroundUrl))
            {
                backgroundUrl = _plugin.EmuMoviesApi
                    .GetBulkMediaUrl(
                        new List<string>
                            {Path.GetFileNameWithoutExtension(_options.GameData.GameImagePath), _options.GameData.Name},
                        "Nintendo_NES", MediaType.Artwork).Result;
            }

            if (!string.IsNullOrWhiteSpace(backgroundUrl))
            {
                return new MetadataFile(backgroundUrl);
            }

            return base.GetBackgroundImage();
        }

        public override MetadataFile GetIcon()
        {
            GetPlatform();

            var iconUrl = _plugin.EmuMoviesApi.GetBulkMediaUrl(new List<string> { Path.GetFileNameWithoutExtension(_options.GameData.GameImagePath), _options.GameData.Name }, _platform, MediaType.Icon).Result;
            if (!string.IsNullOrWhiteSpace(iconUrl))
            {
                return new MetadataFile(iconUrl);
            }

            return base.GetIcon();
        }

        public override List<MetadataField> AvailableFields
        {
            get
            {
                return _plugin.SupportedFields;
            }
        }
    }
}
