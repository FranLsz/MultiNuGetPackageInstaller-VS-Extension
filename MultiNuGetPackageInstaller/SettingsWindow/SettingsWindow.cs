using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MultiNuGetPackageInstaller.Model;
using Newtonsoft.Json;
using NuGet;

namespace MultiNuGetPackageInstaller.SettingsWindow
{
    public partial class SettingsWindow : UserControl
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        internal OptionPage OptionsPage;

        private List<Template> _templates;
        private Template _selectedTemplate;

        public void Initialize()
        {

            if (!OptionsPage.TemplatesJson.IsEmpty())
            {
                _templates = JsonConvert.DeserializeObject<List<Template>>(OptionsPage.TemplatesJson);
                LoadTemplateButtons(_templates);
            }
            else
                _templates = new List<Template>();

        }

        private void ExtensionUserControl_Load(object sender, EventArgs e)
        {

        }

        // Cargar los paquetes del template actual
        private void LoadPackages(object sender, EventArgs e)
        {
            SaveBtn.Enabled = true;
            DeleteBtn.Enabled = true;

            _selectedTemplate = _templates.First(o => o.Name == ((Button)sender).Text);
            TemplateNameTxt.Text = _selectedTemplate.Name;
            PackageBoxTxt.Lines = _selectedTemplate.Packages;
        }

        // Nueva template
        private void NewBtn_Click(object sender, EventArgs e)
        {
            _selectedTemplate = null;
            DeleteBtn.Enabled = false;

            TemplateNameTxt.Text = string.Empty;
            PackageBoxTxt.Lines = new[] { string.Empty };
        }

        // Guardar template actual
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            var template = new Template() { Name = TemplateNameTxt.Text, Packages = PackageBoxTxt.Lines };

            // Editando
            if (_selectedTemplate != null)
            {
                if (!ValidateTemplate(template, true))
                    return;
                var index = _templates.IndexOf(_selectedTemplate);
                _templates[index] = template;
                _selectedTemplate = template;
            }
            // Creando
            else
            {
                if (!ValidateTemplate(template))
                    return;
                _templates.Add(template);
            }

            _selectedTemplate = template;
            DeleteBtn.Enabled = true;
            LoadTemplateButtons(_templates);
            OptionsPage.TemplatesJson = JsonConvert.SerializeObject(_templates);
        }

        // Eliminar template actual
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            _templates.Remove(_selectedTemplate);
            LoadTemplateButtons(_templates);
            OptionsPage.TemplatesJson = JsonConvert.SerializeObject(_templates);
            NewBtn_Click(sender, e);
        }

        // Cargar los botones de los templates
        private void LoadTemplateButtons(List<Template> templates)
        {
            ButtonsPanel.Controls.Clear();

            var i = 0;
            foreach (var tt in _templates)
            {
                i++;

                var btn = new Button();
                btn.Text = tt.Name;
                btn.Parent = ButtonsPanel;
                btn.Click += LoadPackages;
                btn.AutoSize = true;
                btn.Location = new Point(20, i * 30);
            }
        }

        // Validar el template que se pretende añadir
        private bool ValidateTemplate(Template template, bool editing = false)
        {
            // Si no estan vacios los campos
            if (template.Name.IsEmpty() || !template.Packages.Any())
            {
                ErrorLbl.Text = "Both fields are required";
                return false;
            }

            // Si el nombre el template no es muy largo
            if (template.Name.Length > 20)
            {
                ErrorLbl.Text = "The name is too long";
                return false;
            }

            // Si no se esta editando
            if (!editing)
            {
                // Comprueba que no se intenta añadir un template 
                // con el mismo nombre que otro existente
                if (_templates.Any(o => o.Name == template.Name))
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
