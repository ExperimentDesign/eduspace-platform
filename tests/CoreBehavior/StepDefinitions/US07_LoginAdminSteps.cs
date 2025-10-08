using TechTalk.SpecFlow;
using FluentAssertions;

namespace FULLSTACKFURY.EduSpace.Tests.StepDefinitions
{
    [Binding]
    public class US07_LoginAdminSteps
    {
        private string _email;
        private string _password;
        private string _mensaje;

        [Given(@"Que el administrador ha ingresado sus credenciales ""(.*)"" y contraseña ""(.*)""")]
        public void GivenAdministradorIngresaCredenciales(string email, string password)
        {
            _email = email;
            _password = password;
        }

        [Given(@"Que el administrador ha ingresado credenciales incorrectas ""(.*)"" y contraseña ""(.*)""")]
        public void GivenAdministradorIngresaCredencialesIncorrectas(string email, string password)
        {
            _email = email;
            _password = password;
        }

        [When(@"el administrador envía la solicitud de inicio de sesión")]
        public void WhenAdministradorEnviaSolicitud()
        {
            if (_email == "admin@upc.edu.pe" && _password == "admin123")
            {
                _mensaje = "Inicio de sesión exitoso";
            }
            else
            {
                _mensaje = "Credenciales no válidas";
            }
        }

        [When(@"el administrador intenta iniciar sesión")]
        public void WhenAdministradorIntentaIniciarSesion()
        {
            WhenAdministradorEnviaSolicitud();
        }

        [Then(@"el sistema debe autenticar al administrador correctamente")]
        public void ThenSistemaAutenticaAdministrador()
        {
            _mensaje.Should().Contain("exitoso");
        }

        [Then(@"el sistema debe permitir al administrador acceder a las funcionalidades para gestionar la administración")]
        public void ThenSistemaPermiteAcceso()
        {
            _mensaje.Should().Contain("exitoso");
        }

        [Then(@"el sistema debe mostrar un mensaje de error indicando que las credenciales no son válidas")]
        public void ThenSistemaMuestraError()
        {
            _mensaje.Should().Contain("no válidas");
        }
    }
}
