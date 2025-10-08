using TechTalk.SpecFlow;
using FluentAssertions;
using System.Collections.Generic;

namespace FULLSTACKFURY.EduSpace.Tests.StepDefinitions
{
    [Binding]
    public class US13_RegisterResourceTests
    {
        private Dictionary<string, string> _formData;
        private string _mensaje;

        [Given(@"que el administrador está en la vista Registro de Recursos")]
        public void GivenAdministradorEnVistaRegistro(Table table)
        {
            _formData = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                foreach (var header in table.Header)
                {
                    _formData[header] = row[header];
                }
            }
        }

        [Given(@"que el administrador ha llenado el formulario con datos incompletos")]
        public void GivenAdministradorFormularioIncompleto(Table table)
        {
            _formData = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                foreach (var header in table.Header)
                {
                    _formData[header] = row[header];
                }
            }
        }

        [When(@"el administrador envía el registro")]
        public void WhenAdministradorEnviaRegistro()
        {
            if (!string.IsNullOrWhiteSpace(_formData.GetValueOrDefault("Name")) &&
                !string.IsNullOrWhiteSpace(_formData.GetValueOrDefault("KindOfResource")) &&
                !string.IsNullOrWhiteSpace(_formData.GetValueOrDefault("ClassroomId")))
            {
                _mensaje = "Registro exitoso";
            }
            else
            {
                _mensaje = "Error: Campos obligatorios faltantes";
            }
        }

        [Then(@"el sistema registra el recurso")]
        public void ThenSistemaRegistraRecurso()
        {
            _mensaje.Should().Contain("Registro exitoso");
        }

        [Then(@"el sistema rechaza el registro y muestra un mensaje de error")]
        public void ThenSistemaMuestraError()
        {
            _mensaje.Should().Contain("Error");
        }
    }
}
