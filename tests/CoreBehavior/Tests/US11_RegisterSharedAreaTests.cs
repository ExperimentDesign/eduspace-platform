using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;

namespace FULLSTACKFURY.EduSpace.Tests
{
    public class US11_RegisterSharedAreaTests
    {
        private Dictionary<string, string> _formData;
        private string _mensaje;

        private void CompletarFormulario(Dictionary<string, string> datos)
        {
            _formData = datos;
        }

        private void EnviarRegistro()
        {
            if (!string.IsNullOrWhiteSpace(_formData["Name"]) &&
                !string.IsNullOrWhiteSpace(_formData["Capacity"]) &&
                !string.IsNullOrWhiteSpace(_formData["Description"]))
            {
                _mensaje = "Registro exitoso";
            }
            else
            {
                _mensaje = "Error: Campos obligatorios faltantes";
            }
        }

        [Test]
        public void RegistroExitoso()
        {
            CompletarFormulario(new Dictionary<string, string>
            {
                { "Name", "Sala de estudio" },
                { "Capacity", "20" },
                { "Description", "Espacio para trabajo grupal" }
            });

            EnviarRegistro();

            _mensaje.Should().Be("Registro exitoso");
        }

        [Test]
        public void RegistroConCamposIncompletos()
        {
            CompletarFormulario(new Dictionary<string, string>
            {
                { "Name", "" },
                { "Capacity", "" },
                { "Description", "" }
            });

            EnviarRegistro();

            _mensaje.Should().Contain("Error");
        }
    }
}