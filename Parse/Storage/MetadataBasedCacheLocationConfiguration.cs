using System;
using System.IO;
using System.Reflection;
using Parse.Abstractions.Library;
using Parse.Abstractions.Storage;

namespace Parse.Storage
{
    /// <summary>
    /// A configuration of the Parse SDK persistent storage location based on product metadata such as company name and product name.
    /// </summary>
    public struct MetadataBasedCacheLocationConfiguration : ICacheLocationConfiguration
    {
        /// <summary>
        /// An instance of <see cref="MetadataBasedCacheLocationConfiguration"/> with inferred values based on the entry assembly. Should be used with <see cref="VersionInformation.Inferred"/>.
        /// </summary>
        /// <remarks>Should not be used with Unity.</remarks>
        public static MetadataBasedCacheLocationConfiguration NoCompanyInferred { get; } = new MetadataBasedCacheLocationConfiguration
        {
            CompanyName = Assembly.GetEntryAssembly().GetName().Name,
            ProductName = String.Empty
        };

        /// <summary>
        /// The name of the company that owns the product specified by <see cref="ProductName"/>.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The name of the product that is using the Parse .NET SDK.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The corresponding relative path generated by this <see cref="ICacheLocationConfiguration"/>.
        /// </summary>
        public string GetRelativeStorageFilePath(IParseCorePlugins plugins) => Path.Combine(CompanyName ?? "Parse", ProductName ?? "_global", $"{plugins.MetadataController.HostVersioningData.DisplayVersion ?? "1.0.0.0"}.cachefile");
    }
}
