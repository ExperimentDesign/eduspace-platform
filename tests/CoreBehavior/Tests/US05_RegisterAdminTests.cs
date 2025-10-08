using NUnit.Framework;
using FULLSTACKFURY.EduSpace.Tests.StepDefinitions;

namespace FULLSTACKFURY.EduSpace.Tests
{
    [TestFixture]
    public class US05_RegisterAdminTests
    {
        [Test]
        public void RegistroExitosoAdministrador()
        {
            var steps = new US05_RegisterAdminSteps();

            steps.GivenAdministradorCompletaFormulario("admin@upc.edu.pe", "123456");
            steps.WhenAdministradorEnviaSolicitud();
            steps.ThenSistemaValidaDatos();
            steps.ThenSistemaEnviaCorreo();
            steps.ThenSistemaMuestraMensajeExito();
        }

        [Test]
        public void RegistroCamposVacios()
        {
            var steps = new US05_RegisterAdminSteps();

            steps.GivenAdministradorDejaCamposVacios();
            steps.WhenIntentaEnviarSolicitud();
            steps.ThenSistemaMuestraErrores();
        }
    }
}