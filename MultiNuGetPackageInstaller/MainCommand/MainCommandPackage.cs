//------------------------------------------------------------------------------
// <copyright file="MainCommandPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using MultiNuGetPackageInstaller.Helpers;
using Newtonsoft.Json;

namespace MultiNuGetPackageInstaller.MainCommand
{
    /// <summary>
    ///     This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The minimum requirement for a class to be considered a valid package for Visual Studio
    ///         is to implement the IVsPackage interface and register itself with the shell.
    ///         This package uses the helper classes defined inside the Managed Package Framework (MPF)
    ///         to do it: it derives from the Package class that provides the implementation of the
    ///         IVsPackage interface and uses the registration attributes defined in the framework to
    ///         register itself and its components with the shell. These attributes tell the pkgdef creation
    ///         utility what data to put into .pkgdef file.
    ///     </para>
    ///     <para>
    ///         To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...
    ///         &gt; in .vsixmanifest file.
    ///     </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuidString)]
    [ProvideOptionPage(typeof(OptionPage),
        "Multi NuGet package installer", "Templates", 106, 104, true)]
    [ProvideProfile(typeof(OptionPage),
        "MultiNuGetPackageInstaller", "Settings", 101, 105, true, DescriptionResourceID = 103)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class MainCommandPackage : Package
    {
        /// <summary>
        ///     MainCommandPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "25ffb3a1-3f31-4587-8a4b-9b7c4f02806d";

        public List<Template> Templates
        {
            get
            {
                var pagina = (OptionPage)GetDialogPage(typeof(OptionPage));
                return JsonConvert.DeserializeObject<List<Template>>(pagina.TemplatesJson);
            }

            set
            {
                var pagina = (OptionPage)GetDialogPage(typeof(OptionPage));
                pagina.TemplatesJson = JsonConvert.SerializeObject(value);
            }
        }

        #region Package Members

        /// <summary>
        ///     Initialization of the package; this method is called right after the package is sited, so this is the place
        ///     where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            MainCommand.Initialize(this);
            base.Initialize();
        }

        #endregion
    }

    [Guid("E3111480-9EA2-4739-A089-5C3C4283FEC3")]
    public class OptionPage : DialogPage
    {
        public string TemplatesJson { get; set; } = string.Empty;

        protected override IWin32Window Window
        {
            get
            {
                var page = new SettingsWindow.SettingsWindow { OptionsPage = this };
                page.Initialize();
                return page;
            }
        }
    }
}