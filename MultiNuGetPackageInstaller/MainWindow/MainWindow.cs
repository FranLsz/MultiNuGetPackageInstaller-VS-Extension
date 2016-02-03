using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.ComponentModelHost;
using MultiNuGetPackageInstaller.Helpers;
using MultiNuGetPackageInstaller.Model;
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

            // Se cargan los templates guardados
            var i2 = 0;
            foreach (Template tt in ExtensionOptions.Templates)
            {
                i2++;
                Button box = new Button();
                box.Text = tt.Name;
                box.Parent = TemplatesPanel;
                box.AutoSize = true;
                box.Location = new Point(20, i2 * 25); //vertical
                box.Click += CargarPaquetes;
            }

            // Se comprueba si hay una solucion cargada
            if (solution == null || !solution.IsOpen)
            {
                ErrorLbl.Text = "Solution not found";
                ProjectsPanel.Visible = false;
                InstallBtn.Enabled = false;
                return;
            }

            // Se cargan los proyectos de la solucion actual
            Projects projects = solution.Projects;
            var i = 0;
            foreach (Project p in projects)
            {
                i++;
                CheckBox btn = new CheckBox();
                btn.Text = p.Name;
                btn.Parent = ProjectsPanel;
                btn.AutoSize = true;
                btn.Location = new Point(20, i * 20); //vertical
            }
        }

        private void CargarPaquetes(object sender, EventArgs e)
        {
            PackagesBox.Lines = ExtensionOptions.Templates.First(o => o.Name == ((Button)sender).Text).Packages;
        }


        void ReportarProgreso(Tuple<int, string, Color> t)
        {
            //ProgressBar.Value = t.Item1;
            PackagesBox.AppendLine(GetHora() + t.Item2, t.Item3);
        }

        // Enviar progreso con el texto en color
        private static void Enviar(IProgress<Tuple<int, string, Color>> p, int progreso, string texto, Color color)
        {
            p.Report(new Tuple<int, string, Color>(progreso, texto, color));
        }

        // Enviar progreso con el texto sin color
        private static void Enviar(IProgress<Tuple<int, string, Color>> p, int progreso, string texto)
        {
            p.Report(new Tuple<int, string, Color>(progreso, texto, Color.Black));
        }

        private async void InstallBtn_Click(object sender, EventArgs e)
        {
            // Valida si el formato de la caja de texto es el correcto
            if (PackagesBox.Lines.IsEmpty())
            {
                ErrorLbl.Text = "Package box must be populated";
                return;
            }

            var expresionRegular = "^[a-zA-Z0-9.]{1,}[ ]{0,1}[0-9.]*$";
            Regex rgx = new Regex(expresionRegular);
            foreach (var line in PackagesBox.Lines)
            {
                if (!rgx.IsMatch(line))
                {
                    ErrorLbl.Text = "Incorrect format, the expected is [PACKAGE.NAME] or [PACKAGE.NAME VERSION]";
                    return;
                }


            }


            // Si no se ha seleccionado un projecto como minimo, lanza mensaje de error
            var projectsTarget = new List<string>();
            foreach (var c in ProjectsPanel.Controls)
            {
                if (c.GetType() != typeof(CheckBox)) continue;
                var box = c as CheckBox;
                if (box.Checked)
                    projectsTarget.Add(box.Text);
            }

            if (projectsTarget.IsEmpty())
            {
                ErrorLbl.Text = "You must selected at least one project";
                return;
            }

            // Se bloquean algunos controles del UI
            ErrorLbl.Text = string.Empty;
            InstallBtn.Enabled = false;
            PackagesBox.ReadOnly = true;
            ProjectsPanel.Enabled = false;
            TemplatesPanel.Enabled = false;


            // Cargamos los paquetes del cuadro de texto y los projectos seleccionados
            var opciones = new Dictionary<string, object>
                {
                    {"PackagesBox", PackagesBox.Lines },
                    {"ProjectNameTarget", projectsTarget },
                };

            // Se vacia el contenido del TextBox
            PackagesBox.Lines = new[] { string.Empty };

            var indicadorDeProgreso = new Progress<Tuple<int, string, Color>>(ReportarProgreso);
            await IniciarProceso(opciones, indicadorDeProgreso);

            // Se vuelven a habilitar los controles bloqueados previamente
            InstallBtn.Enabled = false;
            PackagesBox.ReadOnly = false;
            ProjectsPanel.Enabled = true;
            TemplatesPanel.Enabled = true;
            InstallBtn.Enabled = true;
        }

        private async Task IniciarProceso(IReadOnlyDictionary<string, object> opciones, IProgress<Tuple<int, string, Color>> p)
        {
            await Task.Run(() =>
            {
                Enviar(p, 0, " ------");

                // Extraccion de opciones
                var packagesBoxLines = opciones["PackagesBox"] as string[];
                var projectNameTarget = opciones["ProjectNameTarget"] as List<string>;

                // Obtencion del DTE
                DTE dte = (DTE)ServiceProvider.GetService(typeof(DTE));
                Solution2 solution = dte.Solution as Solution2;
                Projects projects = solution.Projects;

                var packagesToInstall = new Dictionary<string, string>();
                var projectsTarget = new List<Project>();

                foreach (var c in projectNameTarget)
                {
                    projectsTarget.AddRange(from Project pk in projects where pk.Name == c select pk);
                }


                foreach (var line in packagesBoxLines)
                {
                    var trimmedLine = line.Trim();
                    if (trimmedLine.Contains(" "))
                    {
                        var tuple = trimmedLine.Split(' ');
                        var pkgName = tuple[0];
                        var pkgVersion = tuple[1];
                        packagesToInstall.Add(pkgName, pkgVersion);
                    }
                    else
                    {
                        var pkgName = trimmedLine;
                        packagesToInstall.Add(pkgName, string.Empty);
                    }
                }

                Enviar(p, 0, " Connecting to NuGet API...");
                var repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                var componentModel = (IComponentModel)ServiceProvider.GetService(typeof(SComponentModel));
                var pckInstaller = componentModel.GetService<IVsPackageInstaller>();
                Enviar(p, 25, " Connected successfully", Color.Green);

                foreach (var pkg in packagesToInstall)
                {
                    Enviar(p, 25, " Installing " + pkg.Key + " " + pkg.Value);
                    try
                    {
                        var pk = repo.FindPackagesById(pkg.Key).ToList();

                        if (pk.Any())
                        {
                            var pkgName = pkg.Key;
                            var pkgVersion = pkg.Value;

                            // Si no se ha especificado la version del paquete,
                            // se selecciona la ultima disponible
                            if (pkgVersion.IsEmpty())
                                pkgVersion =
                                    pk.Where(o => o.IsLatestVersion)
                                        .Select(o => o.Version)
                                        .FirstOrDefault()
                                        .Version.ToString();

                            // se verifica que existe un paquete con esa version
                            if (pk.All(o => o.Version.ToString() != pkgVersion))
                            {
                                Enviar(p, 25, "Couldn't obtain the version " + pkgVersion + " of " + pkgName, Color.Red);
                                break;
                            }

                            foreach (var project in projectsTarget)
                            {
                                pckInstaller.InstallPackage(OnlineNuGetApi, project, pkgName, pkgVersion, false);
                                Enviar(p, 25, " Package " + pkg.Key + " installed on " + project.Name, Color.Green);
                            }
                        }
                        else
                        {
                            Enviar(p, 25, " Package " + pkg.Key + " not found", Color.Red);
                        }
                    }
                    catch (Exception ex)
                    {
                        Enviar(p, 25, "Couldn't install " + pkg.Key + " on your current solution: " + ex.Message, Color.Red);
                    }
                }

                Enviar(p, 100, " ------");
            });
        }

        private string GetHora()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
