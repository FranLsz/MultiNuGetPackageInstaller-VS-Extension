//------------------------------------------------------------------------------
// <copyright file="ExtensionOptions.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using MultiNuGetPackageInstaller.Model;
using Newtonsoft.Json;

namespace MultiNuGetPackageInstaller.Options
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(ExtensionOptions.PackageGuidString)]
    [ProvideOptionPage(typeof(OptionPage),
    "Multi NuGet package installer", "Settings", 0, 0, true)]
    [ProvideProfile(typeof(OptionPage),
    "Multi NuGet package installer", "Settings", 0, 0, true)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ExtensionOptions : Package
    {
        /// <summary>
        /// ExtensionOptions GUID string.
        /// </summary>
        public const string PackageGuidString = "b5dc16c4-c991-4a9d-a941-6b73c2a35a15";

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionOptions"/> class.
        /// </summary>
        public ExtensionOptions()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        /*public List<Template> Templates
        {
            get
            {
                OptionPage page = (OptionPage)GetDialogPage(typeof(OptionPage));
                return page.Templates;
            }
        }*/

        public List<Template> Templates
        {
            get
            {
                OptionPage page = (OptionPage)GetDialogPage(typeof(OptionPage));
                return JsonConvert.DeserializeObject<List<Template>>(page.TemplatesJson); ;
            }

            set
            {
                OptionPage page = (OptionPage)GetDialogPage(typeof(OptionPage));
                page.TemplatesJson = JsonConvert.SerializeObject(value);
            }
        }

        public string Aaa
        {
            get
            {
                OptionPage page = (OptionPage)GetDialogPage(typeof(OptionPage));
                return page.Aaa; ;
            }

            set
            {
                OptionPage page = (OptionPage)GetDialogPage(typeof(OptionPage));
                page.Aaa = value;
            }
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            MainCommand.MainCommand.Initialize(this);
        }

        #endregion
    }

    [Guid("E3111480-9EA2-4739-A089-5C3C4283FEC3")]
    public class OptionPage : DialogPage
    {
        private string _templatesJson = string.Empty;
        public string TemplatesJson { get { return _templatesJson; } set { _templatesJson = value; } }

        private string _aaa = "AAA";
        public string Aaa { get { return _aaa; } set { _aaa = value; } }

        protected override IWin32Window Window
        {
            get
            {
                ExtensionUserControl page = new ExtensionUserControl { OptionsPage = this };
                page.Initialize();
                return page;
            }
        }
    }
}
