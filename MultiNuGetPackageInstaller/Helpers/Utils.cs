﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace MultiNuGetPackageInstaller.Helpers
{
    public class Utils
    {
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string GetManifestAttribute(string attribute)
        {
            var doc = new XmlDocument();
            var path = Path.Combine(AssemblyDirectory, "extension.vsixmanifest");
            doc.Load(path);
            var metaData = doc.DocumentElement.ChildNodes.Cast<XmlElement>().First(x => x.Name == "Metadata");
            var identity = metaData.ChildNodes.Cast<XmlElement>().First(x => x.Name == "Identity");
            return identity.GetAttribute(attribute);
        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            var place = Source.LastIndexOf(Find, StringComparison.Ordinal);

            if (place == -1)
                return Source;

            var result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
    }
}