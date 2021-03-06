﻿using System.Drawing;
using System.Windows.Forms;

namespace MultiNuGetPackageInstaller.Helpers
{
    public static class WinFormsExtensions
    {
        public static void AgregarLinea(this RichTextBox source, string value, Color color)
        {
            source.SelectionStart = source.TextLength;
            source.SelectionLength = 0;

            source.SelectionColor = color;
            source.AgregarLinea(value);
            source.SelectionColor = source.ForeColor;
        }

        public static void AgregarLinea(this RichTextBox source, string value)
        {
            if (source.Text.Length == 0)
                source.Text = value;
            else
                source.AppendText("\r\n" + value);

            source.ScrollToCaret();
        }
    }
}