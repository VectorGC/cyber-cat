using CommandLine;

namespace ApiGateway.Configurations;

internal class Args
{
    [Option("ssl", Required = false, HelpText = "Configure a PEM format SSL certificate and launch the application on the HTTPS port")]
    public string CertificatePemPath { get; set; }

    [Option("key", Required = false, HelpText = "Set the path to the private key and password for using the SSL certificate")]
    public string CertificateKeyPath { get; set; }

    public bool UseHttps => !string.IsNullOrEmpty(CertificatePemPath) && File.Exists(CertificatePemPath);
}