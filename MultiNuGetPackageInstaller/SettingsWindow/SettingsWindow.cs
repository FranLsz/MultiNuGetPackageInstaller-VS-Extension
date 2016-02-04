using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MultiNuGetPackageInstaller.MainCommand;
using MultiNuGetPackageInstaller.Model;
using Newtonsoft.Json;
using NuGet;

namespace MultiNuGetPackageInstaller.SettingsWindow
{
    public partial class SettingsWindow : UserControl
    {
        private List<Template> _templates;
        private Template _templateSeleccionado;

        internal OptionPage OptionsPage;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            if (!OptionsPage.TemplatesJson.IsEmpty())
            {
                _templates = JsonConvert.DeserializeObject<List<Template>>(OptionsPage.TemplatesJson);
                CargarBotonesDeTemplates(_templates);
            }
            else
                _templates = new List<Template>();
        }

        private void ExtensionUserControl_Load(object sender, EventArgs e)
        {
        }

        // Cargar los paquetes del template actual
        private void Cargarpaquetes(object sender, EventArgs e)
        {
            SaveBtn.Enabled = true;
            DeleteBtn.Enabled = true;

            _templateSeleccionado = _templates.First(o => o.Nombre == ((Button) sender).Text);
            TemplateNameTxt.Text = _templateSeleccionado.Nombre;
            PackageBoxTxt.Lines = _templateSeleccionado.Paquetes;
        }

        // Nueva template
        private void NewBtn_Click(object sender, EventArgs e)
        {
            _templateSeleccionado = null;
            DeleteBtn.Enabled = false;

            TemplateNameTxt.Text = string.Empty;
            PackageBoxTxt.Lines = new[] {string.Empty};
        }

        // Guardar template actual
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            var template = new Template {Nombre = TemplateNameTxt.Text, Paquetes = PackageBoxTxt.Lines};

            // Editando
            if (_templateSeleccionado != null)
            {
                if (!ValidarTemplate(template, true))
                    return;
                var index = _templates.IndexOf(_templateSeleccionado);
                _templates[index] = template;
                _templateSeleccionado = template;
            }
            // Creando
            else
            {
                if (!ValidarTemplate(template))
                    return;
                _templates.Add(template);
            }

            _templateSeleccionado = template;
            DeleteBtn.Enabled = true;
            CargarBotonesDeTemplates(_templates);
            OptionsPage.TemplatesJson = JsonConvert.SerializeObject(_templates);
        }

        // Eliminar template actual
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            _templates.Remove(_templateSeleccionado);
            CargarBotonesDeTemplates(_templates);
            OptionsPage.TemplatesJson = JsonConvert.SerializeObject(_templates);
            NewBtn_Click(sender, e);
        }

        // Cargar los botones de los templates
        private void CargarBotonesDeTemplates(List<Template> templates)
        {
            ButtonsPanel.Controls.Clear();

            var i = 0;
            foreach (var tt in _templates)
            {
                i++;

                var btn = new Button();
                btn.Text = tt.Nombre;
                btn.Parent = ButtonsPanel;
                btn.Click += Cargarpaquetes;
                btn.AutoSize = true;
                btn.Location = new Point(20, i*30);
            }
        }

        // Validar el template que se pretende añadir
        private bool ValidarTemplate(Template template, bool editing = false)
        {
            // Si no estan vacios los campos
            if (template.Nombre.IsEmpty() || !template.Paquetes.Any())
            {
                ErrorLbl.Text = "Both fields are required";
                return false;
            }

            // Si el nombre el template no es muy largo
            if (template.Nombre.Length > 20)
            {
                ErrorLbl.Text = "The name is too long";
                return false;
            }

            // Si no se esta editando
            if (!editing)
            {
                // Comprueba que no se intenta añadir un template 
                // con el mismo nombre que otro existente
                if (_templates.Any(o => o.Nombre == template.Nombre))
                {
                    ErrorLbl.Text = "A template with that name is already entered";
                    return false;
                }
            }


            ErrorLbl.Text = string.Empty;
            return true;
        }
    }
}