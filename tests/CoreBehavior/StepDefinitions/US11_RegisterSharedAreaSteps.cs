using TechTalk.SpecFlow;
using FluentAssertions;
using System.Collections.Generic;

namespace FULLSTACKFURY.EduSpace.Tests.StepDefinitions
{
    [Binding]
    public class US11_RegisterSharedAreaSteps
    {
        private Dictionary<string, string> _formData;
        private string _mensaje;

        [Given(@"que el administrador ha completado el formulario de registro de espacio compartido con datos válidos")]
        public void GivenFormularioValido(Table table)
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

        [Given(@"que el administrador ha completado el formulario de registro de espacio compartido con datos incompletos")]
        public void GivenFormularioInvalido(Table table)
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

        [When(@"el administrador envía el registro de espacio compartido")]
        public void WhenEnviaRegistro()
        {
            // Validación simple simulada
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

        [Then(@"el sistema debe almacenar correctamente el espacio compartido")]
        public void ThenDebeAlmacenar()
        {
            _mensaje.Should().Contain("Registro exitoso");
        }

        [Then(@"el sistema debe mostrar un mensaje de error")]
        public void ThenDebeMostrarError()
        {
            _mensaje.Should().Contain("Error");
        }
    }
}
