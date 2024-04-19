using UnityEngine;

namespace TestTask.Save
{
    public class SaveSystem
    {
        public LevelData LevelData = new LevelData();
        public SettingsData SettingsData = new SettingsData();

        private SaveDataService _saveDataService = new SaveDataService();

        private string _levelDataPath = Application.streamingAssetsPath + "/LevelData.json";
        private string _settingDataPath = Application.streamingAssetsPath + "/SettingsData.json";

        public SaveSystem()
        {
            var levelData = _saveDataService.LoadData(_levelDataPath, LevelData);
            var settingsData = _saveDataService.LoadData(_settingDataPath, SettingsData);
            LevelData = levelData;
            SettingsData = settingsData;
        }

        public void SaveLevelData()
        {
            _saveDataService.SaveData(_levelDataPath, LevelData);
        }

        public void SaveSettingsData()
        {
            _saveDataService.SaveData(_settingDataPath, SettingsData);
        }
    }
}

