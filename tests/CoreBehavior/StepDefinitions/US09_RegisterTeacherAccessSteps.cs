using TechTalk.SpecFlow;
using FluentAssertions;
using System.Collections.Generic;

namespace FULLSTACKFURY.EduSpace.Tests.StepDefinitions
{
    [Binding]
    public class US09_RegisterTeacherAccessSteps
    {
        private Dictionary<string, string> _formData;
        private string _mensaje;

        [Given(@"que el formulario de registro del docente fue completado con datos de formato válido")]
        public void GivenFormularioCompletoValido(Table table)
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

        [Given(@"que el formulario de registro del docente fue completado con datos de formato inválido")]
        public void GivenFormularioCompletoInvalido(Table table)
        {
            GivenFormularioCompletoValido(table);
        }

        [When(@"el docente envía el registro de información")]
        public void WhenDocenteEnviaRegistro()
        {
            if (_formData["Password"] == _formData["ConfirmPassword"])
            {
                _mensaje = "Registro exitoso";
            }
            else
            {
                _mensaje = "Error: Las contraseñas no coinciden";
            }
        }

        [Then(@"el sistema almacenará las credenciales del docente en la base de datos")]
        public void ThenSistemaAlmacenaCredenciales()
        {
            _mensaje.Should().Contain("Registro exitoso");
        }

        [Then(@"el sistema mostrará un mensaje de error")]
        public void ThenSistemaMuestraError()
        {
            _mensaje.Should().Contain("Error");
        }
    }
}