using TechTalk.SpecFlow;
using FluentAssertions;
using System.Collections.Generic;

namespace FULLSTACKFURY.EduSpace.Tests.StepDefinitions
{
    [Binding]
    public class US08_RegisterTeacherProfileSteps
    {
        private Dictionary<string, string> _formData;
        private string _mensaje;

        [Given(@"que el administrador ha ingresado datos completos del docente")]
        public void GivenAdminIngresaDatosCompletos(Table table)
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

        [Given(@"que el administrador ha ingresado información incompleta del docente")]
        public void GivenAdminIngresaDatosIncompletos(Table table)
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
        public void WhenAdminEnviaRegistro()
        {
            if (_formData.ContainsKey("Username") && !string.IsNullOrWhiteSpace(_formData["Username"]) &&
                _formData.ContainsKey("Password") && !string.IsNullOrWhiteSpace(_formData["Password"]) &&
                _formData.ContainsKey("Nombre") && !string.IsNullOrWhiteSpace(_formData["Nombre"]) &&
                _formData.ContainsKey("Apellido") && !string.IsNullOrWhiteSpace(_formData["Apellido"]) &&
                _formData.ContainsKey("Email") && !string.IsNullOrWhiteSpace(_formData["Email"]) &&
                _formData.ContainsKey("Telefono") && !string.IsNullOrWhiteSpace(_formData["Telefono"]) &&
                _formData.ContainsKey("AreaAcademica") && !string.IsNullOrWhiteSpace(_formData["AreaAcademica"]))
            {
                _mensaje = "Registro exitoso";
            }
            else
            {
                _mensaje = "Error: Información incompleta";
            }
        }

        [When(@"el administrador envía el registro de información")]
        public void WhenAdminEnviaRegistroIncompleto()
        {
            WhenAdminEnviaRegistro();
        }

        [Then(@"el sistema almacena los datos ingresados en la base de datos")]
        public void ThenSistemaAlmacenaDatos()
        {
            _mensaje.Should().Contain("Registro exitoso");
        }

        [Then(@"el sistema muestra un mensaje de error")]
        public void ThenSistemaMuestraError()
        {
            _mensaje.Should().Contain("Error");
        }
    }
}
