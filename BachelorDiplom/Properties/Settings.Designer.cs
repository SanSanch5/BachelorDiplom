﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34209
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BachelorLibAPI.Properties {
    
    
    [CompilerGenerated()]
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [ApplicationScopedSetting()]
        [DebuggerNonUserCode()]
        [SpecialSetting(SpecialSetting.ConnectionString)]
        [DefaultSettingValue("Data Source=alex-pc\\alexsql;Initial Catalog=Course_DB;Integrated Security=True")]
        public string Course_DBConnectionString {
            get {
                return ((string)(this["Course_DBConnectionString"]));
            }
        }
        
        [ApplicationScopedSetting()]
        [DebuggerNonUserCode()]
        [SpecialSetting(SpecialSetting.ConnectionString)]
        [DefaultSettingValue("Data Source=alex-pc\\sqlexpress;Initial Catalog=Course_DB;Integrated Security=True" +
            "")]
        public string Course_DBConnectionString1 {
            get {
                return ((string)(this["Course_DBConnectionString1"]));
            }
        }
        
        [ApplicationScopedSetting()]
        [DebuggerNonUserCode()]
        [SpecialSetting(SpecialSetting.ConnectionString)]
        [DefaultSettingValue("Data Source=ALEX-PC\\sqlexpress;Initial Catalog=Course_DB;Integrated Security=True" +
            "")]
        public string Course_DBConnectionString2 {
            get {
                return ((string)(this["Course_DBConnectionString2"]));
            }
        }
        
        [ApplicationScopedSetting()]
        [DebuggerNonUserCode()]
        [DefaultSettingValue("60")]
        public double AvegareVelocity {
            get {
                return ((double)(this["AvegareVelocity"]));
            }
        }
        
        [ApplicationScopedSetting()]
        [DebuggerNonUserCode()]
        [SpecialSetting(SpecialSetting.ConnectionString)]
        [DefaultSettingValue("Server=localhost;Port=5432;User=Alex;Database=cars_tracking;")]
        public string CarsTrackingDBConnectionString {
            get {
                return ((string)(this["CarsTrackingDBConnectionString"]));
            }
        }
    }
}
