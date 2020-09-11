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
    public class EmuMoviesMetadataProvider : OnDemandMetadataProvider
    {
        private readonly MetadataRequestOptions _options;
        private readonly EmuMoviesMetadataPlugin _plugin;
        private readonly string _platform;
        private readonly List<string> _gameNames = new List<string>();

        public EmuMoviesMetadataProvider(MetadataRequestOptions options, EmuMoviesMetadataPlugin plugin)
        {
            _options = options;
            _plugin = plugin;

            if (!string.IsNullOrWhiteSpace(_options?.GameData?.Platform?.Name))
            {
                _platform = _plugin.PlatformTranslationTable.ContainsKey(_options.GameData.Platform.Name)
                    ? _plugin.PlatformTranslationTable[_options.GameData.Platform.Name]
                    : Regex.Replace(_options.GameData.Platform.Name, @"[^A-Za-z0-9]", "_");
            }

            if (_options?.GameData != null)
            {
                if (!string.IsNullOrWhiteSpace(_options.GameData.GameImagePath))
                {
                    _gameNames.Add(_options.GameData.GameImagePath);
                }

                if (!string.IsNullOrWhiteSpace(_options.GameData.Name))
                {
                    _gameNames.Add(_options.GameData.Name);
                }
            }

        }

        public override MetadataFile GetCoverImage()
        {
            var boxUrl = _plugin.EmuMoviesApi.GetBulkMediaUrl(_gameNames, _platform, MediaType.Box).Result;
            if (!string.IsNullOrWhiteSpace(boxUrl))
            {
                return new MetadataFile(boxUrl);
            }

            return base.GetCoverImage();
        }

        public override MetadataFile GetBackgroundImage()
        {
            var backgroundUrl = _plugin.EmuMoviesApi.GetBulkMediaUrl(_gameNames, _platform, MediaType.Background).Result;
            if (string.IsNullOrWhiteSpace(backgroundUrl))
            {
                backgroundUrl = _plugin.EmuMoviesApi.GetBulkMediaUrl(_gameNames, _platform, MediaType.Artwork).Result;
            }

            if (!string.IsNullOrWhiteSpace(backgroundUrl))
            {
                return new MetadataFile(backgroundUrl);
            }

            return base.GetBackgroundImage();
        }

        public override MetadataFile GetIcon()
        {
            var iconUrl = _plugin.EmuMoviesApi.GetBulkMediaUrl(_gameNames, _platform, MediaType.Icon).Result;
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
