﻿using Umbraco.Cms.Core.Manifest;

namespace uSync.Migrations.Core.Composing;
//todo: convert to v15 way of handling ui
internal class SyncMigrationsManifestFilter 
{
    public void Filter(List<PackageManifest> manifests)
    {
        // manifests.Add(new()
        // {
        //     PackageName = uSyncMigrations.AppName,
        //     BundleOptions = BundleOptions.Independent,
        //     Version = uSyncMigrations.AppVersion,
        //     Scripts = new[]
        //     {
        //         uSyncMigrations.PluginFolder + "/migration.service.js",
        //         uSyncMigrations.PluginFolder + "/migration.helpers.js",
        //         uSyncMigrations.PluginFolder + "/backoffice/uSyncMigrations/dashboard.controller.js",
        //         uSyncMigrations.PluginFolder + "/migrate.controller.js",
        //         uSyncMigrations.PluginFolder + "/dialogs/handlerPicker.controller.js",
        //         uSyncMigrations.PluginFolder + "/components/migrationResults.component.js",
        //         uSyncMigrations.PluginFolder + "/components/migrationList.component.js",
        //         uSyncMigrations.PluginFolder + "/components/migrationStatus.component.js",
        //         uSyncMigrations.PluginFolder + "/components/migrationMessages.component.js",
        //         uSyncMigrations.PluginFolder + "/components/migrationImport.component.js",
        //         uSyncMigrations.PluginFolder + "/dialogs/filePicker.controller.js",
        //         uSyncMigrations.PluginFolder + "/dialogs/upload.controller.js",
        //         uSyncMigrations.PluginFolder + "/dialogs/download.controller.js"
        //     },
        //     Stylesheets = new[]
        //     {
        //         uSyncMigrations.PluginFolder + "/migrations.css"
        //     }
        // });
    }
}
