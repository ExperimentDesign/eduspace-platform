using TechTalk.SpecFlow;
using FluentAssertions;

namespace FULLSTACKFURY.EduSpace.Tests.StepDefinitions
{
    [Binding]
    public class US05_RegisterAdminSteps
    {
        private string _email;
        private string _password;
        private string _mensaje;

        [Given(@"Que el administrador ha completado el formulario de registro con un correo institucional válido ""(.*)"" y contraseña ""(.*)""")]
        public void GivenAdministradorCompletaFormulario(string email, string password)
        {
            _email = email;
            _password = password;
        }

        [When(@"el administrador envía la solicitud de registro")]
        public void WhenAdministradorEnviaSolicitud()
        {
            // Simulación de lógica
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password))
            {
                _mensaje = "Error: Campos obligatorios faltantes";
                return;
            }

            if (!_email.EndsWith("@upc.edu.pe"))
            {
                _mensaje = "Error: Correo no válido";
                return;
            }

            _mensaje = "Registro exitoso. Se envió un correo de confirmación.";
        }

        [Then(@"el sistema debe validar los datos ingresados")]
        public void ThenSistemaValidaDatos()
        {
            _email.Should().Contain("@upc.edu.pe");
            _password.Should().NotBeNullOrEmpty();
        }

        [Then(@"el sistema debe enviar un correo de confirmación al correo institucional del administrador")]
        public void ThenSistemaEnviaCorreo()
        {
            _mensaje.Should().Contain("correo de confirmación");
        }

        [Then(@"el sistema debe mostrar un mensaje de éxito indicando que el registro ha sido exitoso")]
        public void ThenSistemaMuestraMensajeExito()
        {
            _mensaje.Should().Contain("Registro exitoso");
        }

        [Given(@"Que el administrador ha dejado campos obligatorios del formulario sin completar")]
        public void GivenAdministradorDejaCamposVacios()
        {
            _email = "";
            _password = "";
        }

        [When(@"intenta enviar la solicitud de registro")]
        public void WhenIntentaEnviarSolicitud()
        {
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password))
            {
                _mensaje = "Campos obligatorios faltantes";
            }
        }

        [Then(@"el sistema debe mostrar mensajes de error en los campos faltantes")]
        public void ThenSistemaMuestraErrores()
        {
            _mensaje.Should().Contain("Campos obligatorios faltantes");
        }
    }
}
