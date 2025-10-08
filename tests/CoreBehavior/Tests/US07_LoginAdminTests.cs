using NUnit.Framework;
using FULLSTACKFURY.EduSpace.Tests.StepDefinitions;

namespace FULLSTACKFURY.EduSpace.Tests.Tests
{
    [TestFixture]
    public class US07_LoginAdminTests
    {
        [Test]
        public void InicioSesionExitoso()
        {
            var steps = new US07_LoginAdminSteps();

            steps.GivenAdministradorIngresaCredenciales("admin@upc.edu.pe", "admin123");
            steps.WhenAdministradorEnviaSolicitud();
            steps.ThenSistemaAutenticaAdministrador();
            steps.ThenSistemaPermiteAcceso();
        }

        [Test]
        public void InicioSesionErrorCredenciales()
        {
            var steps = new US07_LoginAdminSteps();

            steps.GivenAdministradorIngresaCredencialesIncorrectas("admin@upc.edu.pe", "wrongpass");
            steps.WhenAdministradorIntentaIniciarSesion();
            steps.ThenSistemaMuestraError();
        }
    }
}