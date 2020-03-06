using Playnite.SDK;
using System;
using System.Collections.Generic;

namespace EmuMoviesMetadata
{
    public class EmuMoviesMetadataSettings : ObservableObject, ISettings
    {
        private EmuMoviesMetadataSettings _editingClone;
        private readonly EmuMoviesMetadataPlugin _plugin;

        private string username = "";
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string password = "";
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public EmuMoviesMetadataSettings()
        {
        }

        public EmuMoviesMetadataSettings(EmuMoviesMetadataPlugin plugin)
        {
            this._plugin = plugin;

            var settings = plugin.LoadPluginSettings<EmuMoviesMetadataSettings>();
            if (settings != null)
            {
                LoadValues(settings);
            }

        }

        public void BeginEdit()
        {
            _editingClone = this.GetClone();
        }

        public void EndEdit()
        {
            _plugin.SavePluginSettings(this);
            _plugin.EmuMoviesApi.Password = Password;
            _plugin.EmuMoviesApi.Username = Username;
        }

        public void CancelEdit()
        {
            LoadValues(_editingClone);
        }

        public bool VerifySettings(out List<string> errors)
        {
            errors = null;
            return true;
        }

        private void LoadValues(EmuMoviesMetadataSettings source)
        {
            source.CopyProperties(this, false, null, true);
        }
    }
}
