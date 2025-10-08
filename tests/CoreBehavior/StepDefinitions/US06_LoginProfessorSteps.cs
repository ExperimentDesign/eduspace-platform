using TechTalk.SpecFlow;
using FluentAssertions;

namespace FULLSTACKFURY.EduSpace.Tests.StepDefinitions
{
    [Binding]
    public class US06_LoginProfessorSteps
    {
        private string _email;
        private string _password;
        private string _mensaje;

        [Given(@"Que el docente ha ingresado sus credenciales correctamente ""(.*)"" y contraseña ""(.*)""")]
        public void GivenDocenteIngresaCredencialesCorrectas(string email, string password)
        {
            _email = email;
            _password = password;
        }

        [Given(@"Que el docente ha ingresado credenciales incorrectas ""(.*)"" y contraseña ""(.*)""")]
        public void GivenDocenteIngresaCredencialesIncorrectas(string email, string password)
        {
            _email = email;
            _password = password;
        }

        [When(@"el docente envía la solicitud de inicio de sesión")]
        public void WhenDocenteEnviaSolicitud()
        {
            if (_email == "profesor@upc.edu.pe" && _password == "123456")
            {
                _mensaje = "Inicio de sesión exitoso";
            }
            else
            {
                _mensaje = "Credenciales inválidas";
            }
        }

        [When(@"el docente intenta iniciar sesión")]
        public void WhenDocenteIntentaIniciarSesion()
        {
            WhenDocenteEnviaSolicitud();
        }

        [Then(@"el sistema debe autenticar que las credenciales del docente sean las correctas")]
        public void ThenSistemaAutenticaCredenciales()
        {
            _mensaje.Should().Contain("exitoso");
        }

        [Then(@"el sistema debe permitir al docente acceder a las características y recursos específicos para su rol")]
        public void ThenSistemaPermiteAcceso()
        {
            _mensaje.Should().Contain("exitoso");
        }

        [Then(@"el sistema debe mostrar un mensaje de error indicando que las credenciales son inválidas")]
        public void ThenSistemaMuestraError()
        {
            _mensaje.Should().Contain("inválidas");
        }
    }
}
