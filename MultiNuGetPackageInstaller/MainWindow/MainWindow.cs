using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using MultiNuGetPackageInstaller.Options;
using NuGet;
using NuGet.VisualStudio;

namespace MultiNuGetPackageInstaller.MainWindow
{
    public partial class MainWindow : Form
    {
        private static string OnlineNuGetApi => "https://packages.nuget.org/api/v2";

        private IServiceProvider ServiceProvider { get; }
        private ExtensionOptions ExtensionOptions { get; }

        public MainWindow(IServiceProvider service, ExtensionOptions extensionOptions)
        {
            ServiceProvider = service;
            ExtensionOptions = extensionOptions;
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            DTE dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            Solution2 solution = dte.Solution as Solution2;


            // Se comprueba si hay una solucion cargada
            if (solution == null || !solution.IsOpen)
            {
                SolutionNotFoundLbl.Visible = true;
                ProjectsPanel.Visible = false;
                InstallBtn.Enabled = false;
                return;
            }

            Projects projects = solution.Projects;
            var i = 0;
            foreach (Project p in projects)
            {
                i++;
                CheckBox box = new CheckBox();
                box.Text = p.Name;
                box.Parent = ProjectsPanel;
                box.AutoSize = true;
                box.Location = new Point(20, i * 20); //vertical
            }
        }

        private void InstallBtn_Click(object sender, EventArgs e)
        {
            InstallBtn.Enabled = false;
            PackagesBox.Enabled = false;
            ProjectsPanel.Enabled = false;

            DTE dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            Solution2 solution = dte.Solution as Solution2;

            Projects projects = solution.Projects;

            var packagesToInstall = new Dictionary<string, string>();
            var projectsTarget = new List<Project>();

            foreach (var c in ProjectsPanel.Controls)
            {
                if (c.GetType() != typeof(CheckBox)) continue;
                var box = c as CheckBox;
                if (box.Checked)
                    projectsTarget.AddRange(from Project p in projects where p.Name == box.Text select p);
            }


            foreach (var line in PackagesBox.Lines)
            {
                var trimmedLine = line.Replace(" ", "");

                if (trimmedLine.Contains("-"))
                {
                    var tuple = trimmedLine.Split('-');
                    var pkgName = tuple[0];
                    var pkgVersion = tuple[1];
                    packagesToInstall.Add(pkgName, pkgVersion);
                }
                else
                {
                    var pkgName = trimmedLine;
                    packagesToInstall.Add(pkgName, "");
                }
            }




            var repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            var componentModel = (IComponentModel)ServiceProvider.GetService(typeof(SComponentModel));
            var pckInstaller = componentModel.GetService<IVsPackageInstaller>();


            foreach (var pkg in packagesToInstall)
            {
                try
                {
                    var p = repo.FindPackagesById(pkg.Key).ToList();

                    if (p.Any())
                    {
                        var pkgName = pkg.Key;
                        var pkgVersion = pkg.Key;

                        if (pkg.Value.IsEmpty())
                            pkgVersion = p.Where(o => o.IsLatestVersion).Select(o => o.Version).FirstOrDefault().Version.ToString();

                        foreach (var project in projectsTarget)
                        {
                            pckInstaller.InstallPackage(OnlineNuGetApi, project, pkgName, pkgVersion, false);
                        }
                    }
                    else
                    {
                        // PKG not found
                    }
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }

    }
}
