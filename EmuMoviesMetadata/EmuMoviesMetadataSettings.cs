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

            var savedSettings = plugin.LoadPluginSettings<EmuMoviesMetadataSettings>();
            if (savedSettings != null)
            {
                RestoreSettings(savedSettings);
            }

        }

        public void BeginEdit()
        {
            _editingClone = new EmuMoviesMetadataSettings(_plugin);
        }

        public void EndEdit()
        {
            _plugin.SavePluginSettings(this);
            _plugin.EmuMoviesApi.Password = Password;
            _plugin.EmuMoviesApi.Username = Username;
        }

        public void CancelEdit()
        {
            RestoreSettings(_editingClone);
        }

        public bool VerifySettings(out List<string> errors)
        {
            errors = null;
            return true;
        }

        private void RestoreSettings(EmuMoviesMetadataSettings source)
        {
            Username = source.Username;
            Password = source.Password;
        }
    }
}
