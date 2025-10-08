using NUnit.Framework;
using FULLSTACKFURY.EduSpace.Tests.StepDefinitions;

namespace FULLSTACKFURY.EduSpace.Tests.Tests
{
    [TestFixture]
    public class US06_LoginProfessorTests
    {
        [Test]
        public void InicioSesionExitoso()
        {
            var steps = new US06_LoginProfessorSteps();

            steps.GivenDocenteIngresaCredencialesCorrectas("profesor@upc.edu.pe", "123456");
            steps.WhenDocenteEnviaSolicitud();
            steps.ThenSistemaAutenticaCredenciales();
            steps.ThenSistemaPermiteAcceso();
        }

        [Test]
        public void InicioSesionErrorCredenciales()
        {
            var steps = new US06_LoginProfessorSteps();

            steps.GivenDocenteIngresaCredencialesIncorrectas("profesor@upc.edu.pe", "wrongpassword");
            steps.WhenDocenteIntentaIniciarSesion();
            steps.ThenSistemaMuestraError();
        }
    }
}