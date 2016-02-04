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
using MultiNuGetPackageInstaller.MainCommand;
using NuGet;
using NuGet.VisualStudio;

namespace MultiNuGetPackageInstaller.MainWindow
{
    public partial class MainWindow : Form
    {
        public MainWindow(IServiceProvider serviceProvider, MainCommandPackage mainCommandPackage)
        {
            ServiceProvider = serviceProvider;
            MainCommandPackage = mainCommandPackage;
            InitializeComponent();
        }

        private IServiceProvider ServiceProvider { get; }
        private MainCommandPackage MainCommandPackage { get; }

        private static string OnlineNuGetApi => "https://packages.nuget.org/api/v2";

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var dte = (DTE) ServiceProvider.GetService(typeof (DTE));
            var solucion = dte.Solution as Solution2;

            // Se cargan los templates guardados en caso de que existan
            if (MainCommandPackage.Templates != null)
            {
                var i2 = 0;
                foreach (var tt in MainCommandPackage.Templates)
                {
                    var box = new Button();
                    box.Text = tt.Nombre;
                    box.Parent = TemplatesPanel;
                    box.AutoSize = true;
                    box.Location = new Point(20, i2*25); //vertical
                    box.Width = 125;
                    box.FlatStyle = FlatStyle.Flat;
                    box.ForeColor = Color.White;
                    box.BackColor = Color.FromArgb(47, 47, 47);
                    ;
                    box.Click += CargarPaquetes;
                    i2++;
                }
            }

            // Se comprueba si hay una solucion cargada
            if (solucion == null || !solucion.IsOpen)
            {
                ErrorLbl.Text = "Solution not found";
                ProjectsPanel.Visible = false;
                InstallBtn.Enabled = false;
                return;
            }

            // Se cargan los proyectos de la solucion actual
            var projects = solucion.Projects;
            var i = 0;
            foreach (Project p in projects)
            {
                var btn = new CheckBox();
                btn.Text = p.Name;
                btn.Parent = ProjectsPanel;
                btn.AutoSize = true;
                btn.Location = new Point(20, i*20); //vertical
                i++;
            }
        }

        private void CargarPaquetes(object sender, EventArgs e)
        {
            PackagesBox.Lines = MainCommandPackage.Templates.First(o => o.Nombre == ((Button) sender).Text).Paquetes;
        }


        private void ReportarProgreso(Tuple<int, string, Color> t)
        {
            //ProgressBar.Value = t.Item1;
            PackagesBox.AgregarLinea(GetHora() + t.Item2, t.Item3);
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
            var rgx = new Regex(expresionRegular);
            foreach (var line in PackagesBox.Lines)
            {
                if (!rgx.IsMatch(line))
                {
                    ErrorLbl.Text = "Incorrect format, the expected is [PACKAGE.NAME] or [PACKAGE.NAME VERSION]";
                    return;
                }
            }

            // Si no se ha seleccionado un projecto como minimo, lanza mensaje de error
            var proyectosSeleccionados = new List<string>();
            foreach (var c in ProjectsPanel.Controls)
            {
                if (c.GetType() != typeof (CheckBox)) continue;
                var box = c as CheckBox;
                if (box.Checked)
                    proyectosSeleccionados.Add(box.Text);
            }

            if (proyectosSeleccionados.IsEmpty())
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
                {"PackagesBox", PackagesBox.Lines},
                {"ProjectNameTarget", proyectosSeleccionados}
            };

            // Se vacia el contenido del TextBox
            PackagesBox.Lines = new[] {string.Empty};

            var indicadorDeProgreso = new Progress<Tuple<int, string, Color>>(ReportarProgreso);
            await IniciarProceso(opciones, indicadorDeProgreso);

            // Se vuelven a habilitar los controles bloqueados previamente
            InstallBtn.Enabled = false;
            PackagesBox.ReadOnly = false;
            ProjectsPanel.Enabled = true;
            TemplatesPanel.Enabled = true;
            InstallBtn.Enabled = true;
            ClearBtn.Visible = true;
        }

        private async Task IniciarProceso(IReadOnlyDictionary<string, object> opciones,
            IProgress<Tuple<int, string, Color>> p)
        {
            await Task.Run(() =>
            {
                Enviar(p, 0, " ------");

                // Extraccion de opciones
                var listaDePaquetes = opciones["PackagesBox"] as string[];
                var proyectosSeleccionados = opciones["ProjectNameTarget"] as List<string>;

                // Obtencion del DTE
                var dte = (DTE) ServiceProvider.GetService(typeof (DTE));
                var solucion = dte.Solution as Solution2;
                var proyectos = solucion.Projects;

                var paquetesParaInstalar = new Dictionary<string, string>();
                var proyectosDondeInstalar = new List<Project>();

                foreach (var c in proyectosSeleccionados)
                {
                    proyectosDondeInstalar.AddRange(from Project pk in proyectos where pk.Name == c select pk);
                }


                foreach (var linea in listaDePaquetes)
                {
                    var trimmedLine = linea.Trim();
                    var pkgNombre = trimmedLine;
                    var pkgVersion = string.Empty;

                    if (trimmedLine.Contains(" "))
                    {
                        var tupla = trimmedLine.Split(' ');
                        pkgNombre = tupla[0];
                        pkgVersion = tupla[1];
                        paquetesParaInstalar.Add(pkgNombre, pkgVersion);
                    }

                    // si el packete esta duplicado en el textbox, lanza error
                    if (paquetesParaInstalar.Any(o => o.Key == pkgNombre))
                    {
                        Enviar(p, 100, "Package " + pkgNombre + " is duplicated", Color.Red);
                        return;
                    }

                    // se agrega a la lista de paquetes para instalar
                    paquetesParaInstalar.Add(pkgNombre, pkgVersion);
                }

                Enviar(p, 0, " Connecting to NuGet API...");
                var repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                var componentModel = (IComponentModel) ServiceProvider.GetService(typeof (SComponentModel));
                var pkgInstalador = componentModel.GetService<IVsPackageInstaller>();
                Enviar(p, 25, " Connected successfully", Color.Green);

                foreach (var pkg in paquetesParaInstalar)
                {
                    Enviar(p, 25, " Installing " + pkg.Key + " " + pkg.Value);
                    try
                    {
                        var pk = repo.FindPackagesById(pkg.Key).ToList();

                        if (pk.Any())
                        {
                            var pkgNombre = pkg.Key;
                            var pkgVersion = pkg.Value;

                            // Si no se ha especificado la version del paquete,
                            // se selecciona la ultima disponible
                            if (pkgVersion.IsEmpty())
                                pkgVersion =
                                    pk.Where(o => o.IsLatestVersion)
                                        .Select(o => o.Version)
                                        .FirstOrDefault()
                                        .Version.ToString();
                            else
                            {
                                // se verifica que existe un paquete con esa version
                                if (pk.All(o => o.Version.ToString() != pkgVersion))
                                {
                                    Enviar(p, 25, "Couldn't obtain the version " + pkgVersion + " of " + pkgNombre,
                                        Color.Red);
                                    break;
                                }
                            }


                            foreach (var proyecto in proyectosDondeInstalar)
                            {
                                pkgInstalador.InstallPackage(OnlineNuGetApi, proyecto, pkgNombre, pkgVersion, false);
                                Enviar(p, 25, " Package " + pkgNombre + " installed on " + proyecto.Name, Color.Green);
                            }
                        }
                        else
                        {
                            Enviar(p, 25, " Package " + pkg.Key + " not found", Color.Red);
                        }
                    }
                    catch (Exception ex)
                    {
                        Enviar(p, 25, "Couldn't install " + pkg.Key + " on your current solution: " + ex.Message,
                            Color.Red);
                    }
                }

                Enviar(p, 100, " ------");
            });
        }

        private string GetHora()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            PackagesBox.Lines = new[] {string.Empty};
            PackagesBox.ReadOnly = false;
            ClearBtn.Visible = false;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
        }
    }
}